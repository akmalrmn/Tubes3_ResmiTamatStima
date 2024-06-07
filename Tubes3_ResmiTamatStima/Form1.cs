using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Tubes3_ResmiTamatStima.Algorithms;
using Tubes3_ResmiTamatStima.Data;
using static System.Net.Mime.MediaTypeNames;

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
                        System.Drawing.Image image = System.Drawing.Image.FromFile(ofd.FileName);
                        picBoxInput.Image = image;
                        fingerprintData = DBUtilities.ConvertImageToBinary((Bitmap)image);
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

            //for (int i = 0;i < 5;i++) 
            //{
            //    List<int> occurrences;
            //    if (radioBM.Checked)
            //    {
            //        occurrences = boyerMoore.Search(data[i], inputData);
            //    }
            //    else
            //    {
            //        occurrences = kmp.KMPSearch(data[i], inputData);
            //    }

            //    double similarity;
            //    if (occurrences.Count > 0)
            //    {
            //        similarity = 1.0;
            //    }
            //    else
            //    {
            //        int distance = boyerMoore.CalculateLevenshteinDistance(data[i], inputData);
            //        int maxLength = Math.Max(data[i].Length, inputData.Length);
            //        similarity = (maxLength - distance) / (double)maxLength;
            //    }

            //    if (similarity > bestSimilarity)
            //    {
            //        bestSimilarity = similarity;
            //        bestMatch = data[i];
            //        bestMatchIndex = data.IndexOf(data[i]);
            //    }
            //}


            foreach (string text in data)
            {
                List<int> occurrences;
                if (radioBM.Checked)
                {
                    occurrences = boyerMoore.Search(text, inputData);
                }
                else
                {
                    occurrences = kmp.KMPSearch(text, inputData);
                }

                double similarity;
                if (occurrences.Count > 0)
                {
                    similarity = 1.0;
                }
                else
                {
                    int distance = boyerMoore.CalculateLevenshteinDistance(text, inputData);
                    int maxLength = Math.Max(text.Length, inputData.Length);
                    similarity = (maxLength - distance) / (double)maxLength;
                }

                if (similarity > bestSimilarity)
                {
                    bestSimilarity = similarity;
                    bestMatch = text;
                    bestMatchIndex = data.IndexOf(text);
                }
            }

            stopwatch.Stop();
            long waktuEks = stopwatch.ElapsedMilliseconds;

            if (bestMatch != null)
            {
                lblPersentaseKecocokan.Text = $"Persentase Kecocokan: {bestSimilarity * 100}%";
                lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
                MessageBox.Show($"Match found in entry at index {bestMatchIndex} with similarity {bestSimilarity * 100}%");
                byte[] imageBytes = Convert.FromBase64String(bestMatch);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                picBoxMatched.Image = System.Drawing.Image.FromStream(ms);
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
