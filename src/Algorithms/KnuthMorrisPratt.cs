using System.Collections.Generic;
using System.Linq;

namespace Tubes3_ResmiTamatStima.Algorithms
{
    public class KMP
    {
        public List<int> KMPSearch(string pat, string txt)
        {
            int M = pat.Length;
            int N = txt.Length;
            int[] lps = new int[M];
            int j = 0; // index for pat[]
            List<int> result = new List<int>();

            ComputeLPSArray(pat, M, lps);

            int i = 0; // index for txt[]
            while (i < N)
            {
                if (pat[j] == txt[i])
                {
                    j++;
                    i++;
                }
                if (j == M)
                {
                    result.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < N && pat[j] != txt[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
            return result;
        }

        void ComputeLPSArray(string pat, int M, int[] lps)
        {
            int len = 0;
            int i = 1;
            lps[0] = 0;

            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
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