using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_TIIK_WPF.ViewModels
{
    class TestLZ77ViewModel : ViewModelBase
    {
        private uint _k = 4;
        public uint K { get => _k; set => _k = value; }
        private uint _n = 4;
        public uint N { get => _n; set => _n = value; }

        private string _text = string.Empty;
        public string Text { get => _text; set => _text = value; }

        private string _outputText = string.Empty;
        public string OutputText {
            get => _outputText;
            set
            {
                _outputText = value;
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
                        OutputText = LZ77HelperFunctions.Compression(Text, int.Parse(K.ToString()), int.Parse(N.ToString()));
                    });
            }
        }

    }
}
