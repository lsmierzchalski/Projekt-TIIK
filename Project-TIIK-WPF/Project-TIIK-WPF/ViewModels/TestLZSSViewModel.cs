using Project_TIIK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_TIIK_WPF.ViewModels
{
    class TestLZSSViewModel : ViewModelBase
    {
        private uint _k = 4;
        public uint K { get => _k; set => _k = value; }
        private uint _n = 4;
        public uint N { get => _n; set => _n = value; }

        private string _text = "aabbcabbcdddc";// string.Empty;
        public string Text { get => _text; set => _text = value; }

        private List<LZSSCompressionStep> _outputList = new List<LZSSCompressionStep>();
        public List<LZSSCompressionStep> OutputList
        {
            get => _outputList;
            set
            {
                _outputList = value;
                OnPropertyChanged();
            }
        }

        public ICommand Compression_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        OutputList = LZSSHelperFunctions.Compression(Text, int.Parse(K.ToString()), int.Parse(N.ToString()));
                    });
            }
        }
    }
}
