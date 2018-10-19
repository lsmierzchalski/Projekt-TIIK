using Project_TIIK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_TIIK_WPF.ViewModels
{
    class LetterFrequencyViewModel : ViewModelBase
    {
        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set {
                _text = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CharFrequency> _listCharFrequency;
        public ObservableCollection<CharFrequency> ListCharFrequency
        {
            get
            {
                return _listCharFrequency;
            }
            set
            {
                _listCharFrequency = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectFile_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        ReadTextAndCalculateFrequency();
                    });
            }
        }

        private void ReadTextAndCalculateFrequency()
        {
            try
            {
                Text = FileHelpfulFunctions.SelectFile();
                ListCharFrequency = CharFrequencyHelperFunctions.GetListCharFrequency(Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
