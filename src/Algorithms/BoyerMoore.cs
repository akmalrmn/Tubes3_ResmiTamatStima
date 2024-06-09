using System;
using System.Collections.Generic;

namespace Tubes3_ResmiTamatStima.Algorithms
{
    public class BoyerMoore
    {
        private int[] GenerateBadMatchTable(string pattern)
        {
            int[] badMatchTable = new int[256];
            for (int i = 0; i < badMatchTable.Length; i++)
            {
                badMatchTable[i] = pattern.Length;
            }
            for (int i = 0; i < pattern.Length - 1; i++)
            {
                badMatchTable[pattern[i]] = pattern.Length - 1 - i;
            }
            return badMatchTable;
        }

        public List<int> Search(string text, string pattern)
        {
            List<int> occurrences = new List<int>();
            int[] badMatchTable = GenerateBadMatchTable(pattern);
            int offset = 0;

            while (offset <= text.Length - pattern.Length)
            {
                int scan = 0;
                for (scan = pattern.Length - 1; scan >= 0 && pattern[scan] == text[offset + scan]; scan--) ;

                if (scan < 0)
                {
                    occurrences.Add(offset);
                    offset += (offset + pattern.Length < text.Length) ? pattern.Length - badMatchTable[text[offset + pattern.Length]] : 1;
                }
                else
                {
                    offset += Math.Max(1, scan - badMatchTable[text[offset + scan]]);
                }
            }

            return occurrences;
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
