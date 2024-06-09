using System;
using System.Collections.Generic;

namespace Tubes3_ResmiTamatStima.Algorithms
{
    public class BoyerMoore
    {
        private int[] GenerateBadMatchTable(string pattern)
        {
            int[] badMatchTable = new int[256]; // Inisialisasi tabel untuk heuristik karakter buruk dengan 256 entri (untuk semua karakter ASCII)
            for (int i = 0; i < badMatchTable.Length; i++)
            {
                badMatchTable[i] = pattern.Length; // Atur semua entri ke panjang pola
            }
            for (int i = 0; i < pattern.Length - 1; i++)
            {
                badMatchTable[pattern[i]] = pattern.Length - 1 - i; // Atur nilai untuk setiap karakter dalam pola
            }
            return badMatchTable; // Kembalikan tabel karakter buruk yang telah terisi
        }

        public List<int> Search(string text, string pattern)
        {
            List<int> occurrences = new List<int>(); // Daftar untuk menyimpan indeks awal dari kemunculan pola dalam teks
            int[] badMatchTable = GenerateBadMatchTable(pattern); // Buat tabel karakter buruk untuk pola
            int offset = 0; // Mulai dari awal teks

            while (offset <= text.Length - pattern.Length) // Iterasi melalui teks
            {
                int scan = 0;
                for (scan = pattern.Length - 1; scan >= 0 && pattern[scan] == text[offset + scan]; scan--) ; // Pindai pola dari kanan ke kiri

                if (scan < 0) // Jika pola ditemukan
                {
                    occurrences.Add(offset); // Tambahkan offset ke daftar kemunculan
                    offset += (offset + pattern.Length < text.Length) ? pattern.Length - badMatchTable[text[offset + pattern.Length]] : 1; // Gunakan tabel karakter buruk untuk menggeser pola
                }
                else // Jika ditemukan ketidakcocokan
                {
                    offset += Math.Max(1, scan - badMatchTable[text[offset + scan]]); // Gunakan tabel karakter buruk untuk menggeser pola
                }
            }

            return occurrences; // Kembalikan daftar kemunculan
        }

        public int CalculateLevenshteinDistance(string text, string pattern)
        {
            int textLength = text.Length; // Panjang teks
            int patternLength = pattern.Length; // Panjang pola
            int[,] distance = new int[textLength + 1, patternLength + 1]; // Buat array 2D untuk menyimpan jarak

            for (int i = 0; i <= textLength; i++)
            {
                distance[i, 0] = i; // Inisialisasi kolom pertama dari matriks jarak
            }

            for (int j = 0; j <= patternLength; j++)
            {
                distance[0, j] = j; // Inisialisasi baris pertama dari matriks jarak
            }

            for (int i = 1; i <= textLength; i++)
            {
                for (int j = 1; j <= patternLength; j++)
                {
                    int cost = (pattern[j - 1] == text[i - 1]) ? 0 : 1; // Hitung biaya substitusi

                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), // Minimum dari penghapusan dan penyisipan
                        distance[i - 1, j - 1] + cost); // Minimum dari substitusi
                }
            }

            return distance[textLength, patternLength]; // Kembalikan jarak Levenshtein
        }
    }
}
