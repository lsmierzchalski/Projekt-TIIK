using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF.Models
{
    public class LZ77StepOfAlgorithm
    {
        private string _dictionaryBuffer = string.Empty;
        public string DictionaryBuffer { get => _dictionaryBuffer; set => _dictionaryBuffer = value; }

        private string _inputBuffer = string.Empty;
        public string InputBuffer { get => _inputBuffer; set => _inputBuffer = value; }

        public LZ77StepOfAlgorithm(string buf1, string buf2)
        {
            DictionaryBuffer = buf1;
            InputBuffer = buf2;
        }
    }
}
