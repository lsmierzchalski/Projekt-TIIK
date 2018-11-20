using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF.Models
{
    public class LZ77DecompressionStepOfAlgorithm
    {
        private int _lp = 0;
        public int Lp { get => _lp; set => _lp = value; }

        private string _dictionaryBuffer = string.Empty;
        public string DictionaryBuffer { get => _dictionaryBuffer; set => _dictionaryBuffer = value; }

        private string _inputBuffer = string.Empty;
        public string InputBuffer { get => _inputBuffer; set => _inputBuffer = value; }

        private string _decompressionText = string.Empty;
        public string DecompressionText { get => _decompressionText; set => _decompressionText = value; }

        public LZ77DecompressionStepOfAlgorithm(int lp, string dictionary, string input,string output)
        {
            Lp = lp;
            DictionaryBuffer = dictionary;
            InputBuffer = input;
            DecompressionText = output;
        }

    }
}
