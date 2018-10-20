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
using LiveCharts;
using LiveCharts.Wpf;

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

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection {
            get { return _seriesCollection; }
            set {
                _seriesCollection = value;
                OnPropertyChanged(); 
                } 
        }

        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged();
            }
        }

        public Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
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

                //Chart
                ChartValues<double> chartValues = new ChartValues<double>();
                foreach(var item in ListCharFrequency)
                {
                    chartValues.Add(item.Frequency);
                }

                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Liczba wystąpień",
                        Values = chartValues
                    }
                };

                List<string> labels = new List<string>();
                foreach (var item in ListCharFrequency)
                {
                    labels.Add(item.Character.ToString());
                }

                Labels = labels.ToArray();
                Formatter = value => value + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
