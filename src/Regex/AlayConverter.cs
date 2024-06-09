using System;
using System.Text.RegularExpressions;

public class AlayConverter
{
    // kamus alfabet alay
    private static readonly Dictionary<char, char> digitToLetterMap = new Dictionary<char, char>
    {
        {'1', 'i'},
        {'4', 'a'},
        {'6', 'g'},
        {'0', 'o'},
        {'3', 'e'},
        {'5', 's'},
        {'7', 't'},
        {'8', 'b'},
        {'9', 'p'}
    };

    // jika angka semua, tidak diganti jadi huruf
    private static string ConvertDigitsToLetters(string input)
    {
        return Regex.Replace(input, @"\b\w+\b", m => // \b\w+\b is a word boundary followed by one or more word characters followed by another word boundary
        {
            string word = m.Value;
            if (Regex.IsMatch(word, "[a-zA-Z]"))
            {
                foreach (var pair in digitToLetterMap)
                {
                    word = word.Replace(pair.Key.ToString(), pair.Value.ToString());
                }
            }
            return word;
        });
    }

    private static string NormalizeCase(string input)
    {
        return Regex.Replace(input, @"\b\w+\b", m =>
        {
            string word = m.Value;
            if (word.Length > 1)
            {
                if (word.Equals(word.ToUpper()))
                {
                    return word;
                }
                else
                {
                    // huruf pertama pada kata bukan kapital, ubah semua huruf jadi kecil
                    if (!char.IsUpper(word[0]))
                    {
                        return word.ToLower();
                    }
                    else
                    {
                        // jangan ubah huruf awal menjadi lowercase apabila sebelumnya kapital
                        return char.ToUpper(word[0]) + word.Substring(1).ToLower();
                    }
                }
            }
            return word;
        });
    }

    public static string ConvertAlayToOriginal(string alayText)
    {
        alayText = NormalizeCase(alayText);
        alayText = ConvertDigitsToLetters(alayText);
        return alayText;
    }
      
}
