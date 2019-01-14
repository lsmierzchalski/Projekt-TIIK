using Project_TIIK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Project_TIIK_WPF
{
    public static class LZSSHelperFunctions
    {
        public  const int CHAR_BIT_SIZE = 8;

        public static List<LZSSCompressionStep> Compression(string text, int k, int n)
        {
            List<LZSSCompressionStep> outputList = new List<LZSSCompressionStep>();
            string result = string.Empty;
            string input = string.Empty;

            int size0PC = GetBitSize0PC(k, n);

            int lp = 1;
            if (text.Length > 0)
            {

                result = text[0] + "\n";
                if (text.Length >= n)
                {
                    input = text.Substring(0, n);
                }
                else
                {
                    input = text;
                }
                string dictionary = string.Empty;
                for (int i = 0; i < k; i++)
                {
                    dictionary += input[0];
                }
                int s = 0;
                outputList.Add(new LZSSCompressionStep(lp, dictionary, input, text[0]));
                while (input.Length != 0)
                {
                    lp++;
                    int i = 0;
                    int l = 0;
                    char c = input[0];

                    bool bit = true;
                    for (int j = input.Length; j > 0; j--)
                    {
                        string prefix = input.Substring(0, j);
                        int tmp = PrefixExists(dictionary, prefix);
                        if (tmp != -1 && size0PC < prefix.Length * CHAR_BIT_SIZE)
                        {
                            i = tmp;
                            l = prefix.Length;
                            bit = false;
                            break;
                        }
                    }

                    if (bit)
                    {
                        outputList.Add(new LZSSCompressionStep(lp, dictionary, input, input[0]));
                        s += 1;
                        dictionary = (dictionary + input).Substring(1, k);
                    }
                    else
                    {
                        outputList.Add(new LZSSCompressionStep(lp, dictionary, input, i, l));
                        s += l;
                        dictionary = (dictionary + input).Substring(l, k);
                    }

                    result += i + ", " + l + ", " + c + "\n";
;
                    if (s + n <= text.Length)
                    {
                        input = text.Substring(s, n);
                    }
                    else
                    {
                        input = text.Substring(s, text.Length - s);
                    }
                }

            }

            return outputList;
        }

        public static int PrefixExists(string dictionary, string prefix)
        {
            int result = -1;

            string window = dictionary + prefix.Substring(0, prefix.Length - 1);

            for (int i = 0; i < window.Length - prefix.Length + 1; i++)
            {
                if (prefix == window.Substring(i, prefix.Length))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public static int GetBitSize0PC(int k, int n)
        {
            string k_2 = Convert.ToString(k-1, 2).ToString();
            string n_2 = Convert.ToString(n-1, 2).ToString();

            int size = 1 + k_2.Length + n_2.Length;

            return size;
        }

    }
}
