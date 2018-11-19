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

        public static string Compression(string text, int k, int n)
        {
            List<string> outputList = new List<string>();
            string result = string.Empty;

            string input = text.Substring(0,n);
            string dictionary = string.Empty;
            for(int i=0; i<k; i++)
            {
                dictionary += input[0];
            }
            int s = 0;
            while(input.Length != 0)
            {
                int i = 0;
                int l = 0;
                char c = input[0];

                for(int j=input.Length-1; j>0; j--)
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

                result+= i + ", " + l + ", " + c + "\n";

                s += l + 1;
                dictionary = (dictionary + input).Substring(l+1,k);
                if (s+n<=text.Length)
                {
                    input = text.Substring(s, n);
                }
                else
                {
                    input = text.Substring(s, text.Length - s);
                }
            }

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
