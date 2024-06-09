using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Tubes3_ResmiTamatStima.Algorithms;
using Tubes3_ResmiTamatStima.Data;
using System.Drawing.Text;

namespace Tubes3_ResmiTamatStima
{
    public partial class Form1 : Form
    {
        private BoyerMoore boyerMoore;
        private KMP kmp;
        private byte[] fingerprintData;
        private string inputData;
        private List<string> data = new List<string>();
        private List<string> files;
        private List<string> names_alay;
        private List<string> names_ori = new List<string>();
        private IConfiguration configuration;
        private Font customFont;
        private PrivateFontCollection pfc;

        public Form1(IConfiguration configuration)
        {
            InitializeComponent();
            LoadCustomFont();

            this.configuration = configuration;
            boyerMoore = new BoyerMoore();
            kmp = new KMP();

            // Set custom font for labels if it was loaded successfully
            if (customFont != null)
            {
                btnPilihCitra.Font = customFont;
                radioBM.Font = customFont;
                radioKMP.Font = customFont;
                groupBox1.Font = customFont;
                btnSearch.Font = customFont;
                lblPersentaseKecocokan.Font = customFont;
                lblWaktuPencarian.Font = customFont;
                BiodataText.Font = customFont;
            }
        }

        // Load custom font from file
        private void LoadCustomFont()
        {
            try
            {
                pfc = new PrivateFontCollection();
                pfc.AddFontFile("font.otf");
                customFont = new Font(pfc.Families[0], 11, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading custom font: {ex.Message}");
                customFont = this.Font; // Fall back to default font if loading fails
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load data from database when form loads
            LoadDataFromDB();
        }

        // Load data from database asynchronously
        private async void LoadDataFromDB()
        {
            // Get image file paths from database
            files = await DBUtilities.GetDataFromDB(configuration);
            foreach (string file in files)
            {
                string fileAwal = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../"));
                string fileAkhir = Path.GetFullPath(Path.Combine(fileAwal, file));
                System.Diagnostics.Debug.WriteLine($"File: {fileAkhir}");
                using Bitmap image = new Bitmap(fileAkhir);

                int portionWidth = Math.Min(image.Width, 30);
                int portionHeight = Math.Min(image.Height, 30);

                // Calculate the coordinates for the middle of the image
                int x = (image.Width - portionWidth) / 2;
                int y = (image.Height - portionHeight) / 2;

                // Extract a portion from the middle of the image
                using Bitmap portionF = image.Clone(new Rectangle(x, y, portionWidth, portionHeight), image.PixelFormat);

                // Convert the portion to binary data
                byte[] binaryData = DBUtilities.ConvertImageToBinary(portionF);

                // Convert binary data to Base64 string representation
                string base64Data = Convert.ToBase64String(binaryData);

                // Add base64 string to data list
                data.Add(base64Data);
            }

            // Get alay names from database
            names_alay = await DBUtilities.GetNamesFromBiodata(configuration);
            foreach (string name in names_alay)
            {
                // Convert alay names to original names
                names_ori.Add(AlayConverter.ConvertAlayToOriginal(name));
            }

            // Update specific entry in sidik_jari table
            await DBUtilities.UpdateNameInSidikJariAsync(configuration, "Jokowi2", "dewi");
        }

        // Handle the click event for the "Pilih Citra" button
        private void btnPilihCitra_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => btnPilihCitra_Click(sender, e)));
            }
            else
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Ambil Gambar");
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            using (Bitmap image = new Bitmap(ofd.FileName))
                            {
                                int portionWidth = Math.Min(image.Width, 30);
                                int portionHeight = Math.Min(image.Height, 30);

                                // Calculate the coordinates for the middle of the image
                                int x = (image.Width - portionWidth) / 2;
                                int y = (image.Height - portionHeight) / 2;

                                // Extract a portion from the middle of the image
                                using (Bitmap portion = image.Clone(new Rectangle(x, y, portionWidth, portionHeight), image.PixelFormat))
                                {
                                    // Display the portion in the PictureBox
                                    picBoxInput.SizeMode = PictureBoxSizeMode.Zoom;
                                    picBoxInput.Image = (System.Drawing.Image)portion.Clone();
                                    // Convert the portion to binary data and base64 string
                                    fingerprintData = DBUtilities.ConvertImageToBinary(portion);
                                    inputData = Convert.ToBase64String(fingerprintData);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error selecting image: {ex.Message}");
                }
            }
        }

        // Handle the click event for the "Search" button
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

            // Perform fingerprint search asynchronously
            await SearchFingerprint();
        }

        // Perform fingerprint search and display the results
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

            // Parallel search in data list using the selected algorithm
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
                System.Diagnostics.Debug.WriteLine($"Index: {i}, Similarity: {similarity}");
                return new { Index = i, Similarity = similarity, Data = d };
            })).ToArray();

            var results = await Task.WhenAll(tasks);

            // Find the best match
            var bestResult = results.OrderByDescending(r => r.Similarity).First();
            bestSimilarity = bestResult.Similarity;
            bestMatch = bestResult.Data;
            bestMatchIndex = bestResult.Index;

            stopwatch.Stop();
            long waktuEks = stopwatch.ElapsedMilliseconds;

            // Display the best match result if similarity is above 60%
            if (bestMatch != null && bestSimilarity * 100 > 60)
            {
                System.Diagnostics.Debug.WriteLine($"Index: {bestMatchIndex}, Similarity: {bestSimilarity}");
                lblPersentaseKecocokan.Text = $"Persentase Kecocokan: {bestSimilarity * 100}%";
                lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
                byte[] imageBytes = Convert.FromBase64String(bestMatch);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    picBoxMatched.SizeMode = PictureBoxSizeMode.Zoom;
                    picBoxMatched.Image = System.Drawing.Image.FromStream(ms);
                }
                int idxData = data.IndexOf(bestMatch);
                string file = files[idxData];
                string name = await DBUtilities.GetNamesByCitraFromDB(configuration, file);

                double bestSimilarity_name = 0;
                string bestMatch_name = null;

                // Search for the best name match
                foreach (string nama in names_ori)
                {
                    List<int> occurrences_name;
                    if (nama.Length > name.Length)
                    {
                        occurrences_name = boyerMoore.Search(name, nama);
                    }
                    else
                    {
                        occurrences_name = boyerMoore.Search(nama, name);
                    }
                    double similarity_name;
                    if (occurrences_name.Count > 0)
                    {
                        bestSimilarity_name = 1.0;
                        bestMatch_name = nama;
                        System.Diagnostics.Debug.WriteLine($"Nama: {nama}, Similarity: 1");
                        break;
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
                    System.Diagnostics.Debug.WriteLine($"Nama: {nama}, Similarity: {similarity_name}");
                }

                int idx = names_ori.IndexOf(bestMatch_name);
                System.Diagnostics.Debug.WriteLine($"Index Nama: {idx}, Similarity: {bestSimilarity_name}");

                // Display the biodata if name similarity is above 60%
                if (idx != -1 && bestSimilarity_name * 100 > 60)
                {
                    string alay = names_alay[idx];
                    Biodata biodata = await DBUtilities.GetBiodataByNameFromDB(configuration, alay);

                    // Create and display labels for biodata
                    var biodataText = $"NIK: {biodata.NIK}\r\n\r\n" +
                      $"Nama: {name}\r\n\r\n" +
                      $"Tempat Lahir: {biodata.tempat_lahir}\r\n\r\n" +
                      $"Tanggal Lahir: {biodata.tanggal_lahir}\r\n\r\n" +
                      $"Jenis Kelamin: {biodata.jenis_kelamin}\r\n\r\n" +
                      $"Golongan Darah: {biodata.golongan_darah}\r\n\r\n" +
                      $"Alamat: {biodata.alamat}\r\n\r\n" +
                      $"Agama: {biodata.agama}\r\n\r\n" +
                      $"Status Perkawinan: {biodata.status_perkawinan}\r\n\r\n" +
                      $"Pekerjaan: {biodata.pekerjaan}\r\n\r\n" +
                      $"Kewarganegaraan: {biodata.kewarganegaraan}\r\n\r\n";

                    BiodataText.TextAlign = HorizontalAlignment.Left;
                    BiodataText.ReadOnly = true;
                    BiodataText.Multiline = true;
                    BiodataText.Text = biodataText;
                }
                else
                {
                    MessageBox.Show("Data Biodata Tidak Ditemukan yang tingkat kemiripan namanya di atas 60%");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Index: {bestMatchIndex}, Similarity: {bestSimilarity}");
                lblPersentaseKecocokan.Text = "Persentase Kecocokan: -";
                lblWaktuPencarian.Text = $"Waktu Pencarian: {waktuEks} ms";
                MessageBox.Show("Tidak Ditemukan Sidik Jari yang Tingkat Kemiripannya diatas 60%");
            }
        }
    }
}

// Define the Biodata class to represent biodata records
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
