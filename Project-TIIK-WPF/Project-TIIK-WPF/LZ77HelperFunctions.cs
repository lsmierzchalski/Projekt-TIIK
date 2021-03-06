﻿using Project_TIIK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_TIIK_WPF
{
    public static class LZ77HelperFunctions
    {

        public static List<LZ77StepOfAlgorithm> Compression(string text, int k, int n)
        {
            List<LZ77StepOfAlgorithm> outputList = new List<LZ77StepOfAlgorithm>();
            string result = string.Empty;
            string input = string.Empty;

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
                outputList.Add(new LZ77StepOfAlgorithm(lp, dictionary, input, text[0]));
                while (input.Length != 0)
                {
                    lp++;
                    int i = 0;
                    int l = 0;
                    char c = input[0];

                    for (int j = input.Length - 1; j > 0; j--)
                    {
                        string prefix = input.Substring(0, j);
                        int tmp = PrefixExists(dictionary, prefix);
                        if (tmp != -1)
                        {
                            i = tmp;
                            l = prefix.Length;
                            c = input[prefix.Length];
                            break;
                        }
                    }

                    result += i + ", " + l + ", " + c + "\n";
                    outputList.Add(new LZ77StepOfAlgorithm(lp, dictionary, input, i,  l, c));

                    s += l + 1;
                    dictionary = (dictionary + input).Substring(l + 1, k);
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

        public static List<LZ77DecompressionStepOfAlgorithm> Decompression(List<LZ77StepOfAlgorithm> compressionList)
        {
            List<LZ77DecompressionStepOfAlgorithm> result = new List<LZ77DecompressionStepOfAlgorithm>();



            return result;
        }

        public static int PrefixExists(string dictionary, string prefix)
        {
            int result = -1;

            string window = dictionary + prefix.Substring(0, prefix.Length - 1);

            for(int i=0; i<window.Length-prefix.Length+1; i++)
            {
                if (prefix == window.Substring(i, prefix.Length))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}
