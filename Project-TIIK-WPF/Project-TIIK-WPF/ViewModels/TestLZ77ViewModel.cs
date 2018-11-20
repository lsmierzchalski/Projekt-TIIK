using Project_TIIK_WPF.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Project_TIIK_WPF.ViewModels
{
    class TestLZ77ViewModel : ViewModelBase
    {
        private uint _k = 4;
        public uint K { get => _k; set => _k = value; }
        private uint _n = 4;
        public uint N { get => _n; set => _n = value; }

        private string _text = "aabbcabbcdddc";// string.Empty;
        public string Text { get => _text; set => _text = value; }

        private List<LZ77StepOfAlgorithm> _outputList = new List<LZ77StepOfAlgorithm>();
        public List<LZ77StepOfAlgorithm> OutputList
        {
            get => _outputList;
            set
            {
                _outputList = value;
                OnPropertyChanged();
            }
        }

        private List<LZ77DecompressionStepOfAlgorithm> _outputDecompressionList = new List<LZ77DecompressionStepOfAlgorithm>();
        public List<LZ77DecompressionStepOfAlgorithm> OutputDecompressionList
        {
            get => _outputDecompressionList;
            set
            {
                _outputDecompressionList = value;
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
                        OutputList = LZ77HelperFunctions.Compression(Text, int.Parse(K.ToString()), int.Parse(N.ToString()));
                    });
            }
        }

        public ICommand Decompression_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        OutputDecompressionList = LZ77HelperFunctions.Decompression(OutputList);
                    });
            }
        }
        

    }
}
