using System.Collections.Generic;
using System.Linq;

namespace Tubes3_ResmiTamatStima.Algorithms
{
    public class KMP
    {
        public List<int> KMPSearch(string pat, string txt)
        {
            int M = pat.Length; // Panjang pola
            int N = txt.Length; // Panjang teks
            int[] lps = new int[M]; // Array untuk menyimpan nilai prefix suffix terpanjang untuk pola
            int j = 0; // Indeks untuk pola
            List<int> result = new List<int>(); // Daftar untuk menyimpan kemunculan

            ComputeLPSArray(pat, M, lps); // Hitung array LPS

            int i = 0; // Indeks untuk teks
            while (i < N)
            {
                if (pat[j] == txt[i]) // Jika karakter cocok
                {
                    j++;
                    i++;
                }
                if (j == M) // Jika seluruh pola ditemukan
                {
                    result.Add(i - j); // Tambahkan indeks ke daftar hasil
                    j = lps[j - 1]; // Gunakan array LPS untuk memperbarui indeks pola
                }
                else if (i < N && pat[j] != txt[i]) // Jika ditemukan ketidakcocokan
                {
                    if (j != 0)
                        j = lps[j - 1]; // Gunakan array LPS untuk melewati karakter dalam pola
                    else
                        i = i + 1; // Pindah ke karakter berikutnya dalam teks
                }
            }
            return result; // Kembalikan daftar kemunculan
        }

        void ComputeLPSArray(string pat, int M, int[] lps)
        {
            int len = 0; // Panjang prefix suffix terpanjang sebelumnya
            int i = 1;
            lps[0] = 0; // Nilai LPS untuk karakter pertama selalu 0

            while (i < M)
            {
                if (pat[i] == pat[len]) // Jika ada kecocokan
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else // Jika tidak ada kecocokan
                {
                    if (len != 0)
                    {
                        len = lps[len - 1]; // Gunakan array LPS untuk melewati karakter
                    }
                    else
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }


        public int CalculateLevenshteinDistance(string text, string pattern)
        {
            int textLength = text.Length;
            int patternLength = pattern.Length;
            int[,] distance = new int[textLength + 1, patternLength + 1];

            for (int i = 0; i <= textLength; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= patternLength; j++)
            {
                distance[0, j] = j;
            }

            for (int i = 1; i <= textLength; i++)
            {
                for (int j = 1; j <= patternLength; j++)
                {
                    int cost = (pattern[j - 1] == text[i - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[textLength, patternLength];
        }
    }
}