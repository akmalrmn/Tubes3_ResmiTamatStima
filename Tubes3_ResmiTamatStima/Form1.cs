using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Tubes3_ResmiTamatStima.Algorithms;
using Tubes3_ResmiTamatStima.Data;
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
        private IConfiguration configuration;

        public Form1(IConfiguration configuration)
        {
            InitializeComponent();
            this.configuration = configuration;
            boyerMoore = new BoyerMoore();
            kmp = new KMP();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Memuat data dari database ketika form di-load
            LoadDataFromDB();
        }

        private async void LoadDataFromDB()
        {
            data = await DBUtilities.GetDataFromDB(configuration);
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

            if (bestMatch != null)
            {
                lblPersentaseKecocokan.Text = $"Persentase Kecocokan: {bestSimilarity * 100}%";
                lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
                MessageBox.Show($"Match found in entry at index {bestMatchIndex} with similarity {bestSimilarity * 100}%");
                byte[] imageBytes = Convert.FromBase64String(bestMatch);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    picBoxMatched.SizeMode = PictureBoxSizeMode.Zoom;
                    picBoxMatched.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            else
            {
                lblPersentaseKecocokan.Text = "Persentase Kecocokan: 0%";
                lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
                MessageBox.Show("No match found.");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
