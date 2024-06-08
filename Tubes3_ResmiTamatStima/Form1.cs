using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Tubes3_ResmiTamatStima.Algorithms;
using Tubes3_ResmiTamatStima.Data;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tubes3_ResmiTamatStima
{
    public partial class Form1 : Form
    {
        private BoyerMoore boyerMoore;
        private KMP kmp;
        private byte[] fingerprintData;
        private string inputData;
        private List<string> data;
        private List<string> names_alay;
        private List<string> names_ori = new List<string>();
        private IConfiguration configuration;
        private Font customFont;

        public Form1(IConfiguration configuration)
        {
            InitializeComponent();

            // Load Custom Font
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("font.otf");
            customFont = new Font(pfc.Families[0], 11, FontStyle.Bold);

            this.configuration = configuration;
            boyerMoore = new BoyerMoore();
            kmp = new KMP();

            // Set custom font for labels
            btnPilihCitra.Font = customFont;
            radioBM.Font = customFont;
            radioKMP.Font = customFont;
            groupBox1.Font = customFont;
            btnSearch.Font = customFont;
            lblPersentaseKecocokan.Font = customFont;
            lblWaktuPencarian.Font = customFont;
            BiodataText.Font = customFont;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Memuat data dari database ketika form di-load
            LoadDataFromDB();

        }

        private async void LoadDataFromDB()
        {
            await DBUtilities.InsertDummyDataAsync(configuration);
            data = await DBUtilities.GetDataFromDB(configuration);
            names_alay = await DBUtilities.GetNamesFromBiodata(configuration);
            foreach (string name in names_alay)
            {
                names_ori.Add(AlayConverter.ConvertAlayToOriginal(name));
            }

            await DBUtilities.UpdateNameInSidikJariAsync(configuration, "Jokowi2", "dewi");
        }

        private void btnPilihCitra_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => btnPilihCitra_Click(sender, e)));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ambil Gambar");
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        using Bitmap image = new Bitmap(ofd.FileName);
                        // Ensure the image is large enough for the specified portion
                        if (image.Width < 30 || image.Height < 30)
                        {
                            MessageBox.Show("Image is too small for the specified portion size.");
                            return;
                        }

                        // Calculate the coordinates for the middle of the image
                        int x = (image.Width - 30) / 2;
                        int y = (image.Height - 30) / 2;

                        // Extract a 30x30 portion from the middle of the image
                        using Bitmap portion = image.Clone(new Rectangle(x, y, 30, 30), image.PixelFormat);
                        picBoxInput.SizeMode = PictureBoxSizeMode.Zoom; // Ensure the PictureBox displays the image properly
                        picBoxInput.Image = (System.Drawing.Image)portion.Clone();
                        fingerprintData = DBUtilities.ConvertImageToBinary(portion);
                        inputData = Convert.ToBase64String(fingerprintData);
                    }
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (picBoxInput.Image == null)
            {
                MessageBox.Show("Harap pilih citra sidik jari terlebih dahulu.");
                return;
            }

            if (!radioBM.Checked && !radioKMP.Checked)
            {
                MessageBox.Show("Mohon pilih Algoritma terlebih dahulu");
                return;
            }

            await SearchFingerprint();
        }

        private async Task SearchFingerprint()
        {
            if (fingerprintData == null)
            {
                MessageBox.Show("Harap pilih citra sidik jari terlebih dahulu.");
                return;
            }

            if (data == null || !data.Any())
            {
                MessageBox.Show("Data dari database belum dimuat atau tidak tersedia.");
                return;
            }

            double bestSimilarity = 0;
            string bestMatch = null;
            int bestMatchIndex = -1;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            System.Diagnostics.Debug.WriteLine($"Number of data to search: {data.Count}");

            var tasks = data.Select((d, i) => Task.Run(() =>
            {
                List<int> occurrences;
                if (radioBM.Checked)
                {
                    occurrences = boyerMoore.Search(d, inputData);
                }
                else
                {
                    occurrences = kmp.KMPSearch(d, inputData);
                }

                double similarity;
                if (occurrences.Count > 0)
                {
                    similarity = 1.0;
                }
                else
                {
                    int distance = boyerMoore.CalculateLevenshteinDistance(d, inputData);
                    int maxLength = Math.Max(d.Length, inputData.Length);
                    similarity = (maxLength - distance) / (double)maxLength;
                }

                return new { Index = i, Similarity = similarity, Data = d };
            })).ToArray();

            var results = await Task.WhenAll(tasks);

            var bestResult = results.OrderByDescending(r => r.Similarity).First();
            bestSimilarity = bestResult.Similarity;
            bestMatch = bestResult.Data;
            bestMatchIndex = bestResult.Index;

            stopwatch.Stop();
            long waktuEks = stopwatch.ElapsedMilliseconds;

            if (bestMatch != null && bestSimilarity * 100 > 70)
            {
                lblPersentaseKecocokan.Text = $"Persentase Kecocokan: {bestSimilarity * 100}%";
                lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
                byte[] imageBytes = Convert.FromBase64String(bestMatch);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    picBoxMatched.SizeMode = PictureBoxSizeMode.Zoom;
                    picBoxMatched.Image = System.Drawing.Image.FromStream(ms);
                }
                string name = await DBUtilities.GetNamesByCitraFromDB(configuration, bestMatch);


                double bestSimilarity_name = 0;
                string bestMatch_name = null;

                foreach (string nama in names_ori)
                {
                    List<int> occurrences_name;
                    occurrences_name = boyerMoore.Search(nama, name);
                    double similarity_name;
                    if (occurrences_name.Count > 0)
                    {
                        similarity_name = 1.0;
                    }
                    else
                    {
                        int distance = boyerMoore.CalculateLevenshteinDistance(nama, name);
                        int maxLength = Math.Max(nama.Length, name.Length);
                        similarity_name = (maxLength - distance) / (double)maxLength;
                    }

                    if (similarity_name > bestSimilarity_name)
                    {
                        bestSimilarity_name = similarity_name;
                        bestMatch_name = nama;
                    }
                }

                int idx = names_ori.IndexOf(bestMatch_name);
                string alay = names_alay[idx];
                Biodata biodata = await DBUtilities.GetBiodataByNameFromDB(configuration, alay);

                // Create and display labels for biodata
                var biodataText = $@"
                NIK: {biodata.NIK}
                Nama: {name}
                Tempat Lahir: {biodata.tempat_lahir}
                Tanggal Lahir: {biodata.tanggal_lahir}
                Jenis Kelamin: {biodata.jenis_kelamin}
                Golongan Darah: {biodata.golongan_darah}
                Alamat: {biodata.alamat}
                Agama: {biodata.agama}
                Status Perkawinan: {biodata.status_perkawinan}
                Pekerjaan: {biodata.pekerjaan}
                Kewarganegaraan: {biodata.kewarganegaraan}";

                BiodataText.Text = biodataText;
            }
            else
            {
                lblPersentaseKecocokan.Text = "Persentase Kecocokan: -";
                lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
                MessageBox.Show("Tidak Ditemukan Sidik Jari yang Tingkat Kemiripannya diatas 70%");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void JudulProgram_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblWaktuPencarian_Click(object sender, EventArgs e)
        {

        }

        private void picBoxInput_Click(object sender, EventArgs e)
        {

        }

        private void radioBM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblWaktuPencarian_Click_1(object sender, EventArgs e)
        {

        }

        private void lblPersentaseKecocokan_Click(object sender, EventArgs e)
        {

        }
    }
}

public class Biodata 
{
    public string NIK { get; set; }
    public string nama { get; set; }
    public string tempat_lahir { get; set; }
    public string tanggal_lahir { get; set; }
    public string jenis_kelamin { get; set; }
    public string golongan_darah { get; set; }
    public string alamat { get; set; }
    public string agama { get; set; }
    public string status_perkawinan { get; set; }
    public string pekerjaan { get; set; }
    public string kewarganegaraan { get; set; }
}