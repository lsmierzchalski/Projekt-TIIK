﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF.Models
{
    public class LZ77StepOfAlgorithm
    {
        private int _lp = 0;
        public int Lp { get => _lp; set => _lp = value; }

        private string _dictionaryBuffer = string.Empty;
        public string DictionaryBuffer { get => _dictionaryBuffer; set => _dictionaryBuffer = value; }

        private string _inputBuffer = string.Empty;
        public string InputBuffer { get => _inputBuffer; set => _inputBuffer = value; }


        private int _p;
        public int P { get => _p; set => _p = value; }
        private int _c;
        public int C { get => _c; set => _c = value; }
        private char _s;
        public char S { get => _s; set => _s = value; }
        
        public string OutputEncoder {
            get
            {
                if (Lp == 1) return S.ToString();
                return P + ", " + C + ", " + S;
            }
        }

        public LZ77StepOfAlgorithm(int lp, string dictionary, string input, int p, int c, char s)
        {
            Lp = lp;
            DictionaryBuffer = dictionary;
            InputBuffer = input;
            P = p;
            C = c;
            S = s;
        }

        public LZ77StepOfAlgorithm(int lp, string dictionary, string input, char s)
        {
            Lp = lp;
            DictionaryBuffer = dictionary;
            InputBuffer = input;
            S = s;
        }
    }
}
