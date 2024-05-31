using System.Diagnostics;
using Tubes3_ResmiTamatStima.Algorithms;

namespace Tubes3_ResmiTamatStima
{
    public partial class Form1 : Form
    {
        private BoyerMoore boyerMoore;
        private KMP kmp;
        public Form1()
        {
            InitializeComponent();
            boyerMoore = new BoyerMoore();
            kmp = new KMP();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnPilihCitra_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Image image = Image.FromFile(ofd.FileName);
                    picBoxInput.Image = image;
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (picBoxInput.Image == null)
            //{
            //    MessageBox.Show("Harap pilih citra sidik jari terlebih dahulu.");
            //    return;
            //}

            SearchFingerprint();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void SidikJariMasukan_Click(object sender, EventArgs e)
        {

        }

        private void SearchFingerprint()
        {
            string text = "FARHAN";
            string pattern = "FARHANU";
            Stopwatch stopwatch = new Stopwatch();
            // Search for pattern in text
            stopwatch.Start();
            List<int> occurrences = boyerMoore.Search(text, pattern);


            if (occurrences.Count > 0)
            {
                Console.WriteLine($"Found {occurrences.Count} occurrences of '{pattern}' in text. Similarity: 100%");
                lblPersentaseKecocokan.Text = "Persentase Kecocokan: 100%";
            }
            else
            {
                Console.WriteLine($"No exact match found for '{pattern}' in text. Calculating similarity...");

                // Calculate Levenshtein Distance
                int distance = boyerMoore.CalculateLevenshteinDistance(text, pattern);
                Console.WriteLine($"Levenshtein Distance between text and pattern: {distance}");

                // Calculate similarity percentage
                int maxLength = Math.Max(text.Length, pattern.Length);
                double similarity = (maxLength - distance) / (double)maxLength;
                Console.WriteLine($"Similarity: {similarity * 100}%");
                lblPersentaseKecocokan.Text = $"Persentase Kecocokan: {similarity * 100}%";
            }
            stopwatch.Stop();
            long waktuEks = stopwatch.ElapsedMilliseconds;
            lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
            Console.ReadLine();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
