using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Project_TIIK_WPF.ViewModels
{
    public class LZSSViewModel : ViewModelBase
    {
        //parametry
        private uint _dictionarySize = 8;
        private uint _bufferSize = 4;
        private uint _charSize = 1;

        public uint DictionarySize { get => _dictionarySize; set => _dictionarySize = value; }
        public uint BufferSize { get => _bufferSize; set => _bufferSize = value; }
        public uint CharSize { get => _charSize; set => _charSize = value; }


        private string _pathFileCompresion = "C:\\Users\\lukis\\Downloads\\puste\\abracadabra.txt"; // string.Empty;
        private string _pathFileDecompresion = "C:\\Users\\lukis\\Downloads\\puste\\abracadabra.lzss";
        private string _timeCompresion = string.Empty;
        private string _timeDecompresion = string.Empty;
        private string _sizeCompresionInputFile = string.Empty;
        private string _sizeDecompresionInputFile = string.Empty;
        private string _sizeCompresionOutputFile = string.Empty;
        private string _sizeDecompresionOutputFile = string.Empty;

        private ObservableCollection<string> _codePageCollection = new ObservableCollection<string>() { "utf-8", "utf-16", "windows-1250" };

        private string _selectCodePageToCompresion = string.Empty;

        public string PathFileCompresion { get => _pathFileCompresion; set { _pathFileCompresion = value; OnPropertyChanged(); } }
        public string PathFileDecompresion { get => _pathFileDecompresion; set { _pathFileDecompresion = value; OnPropertyChanged(); } }
        public string TimeCompresion { get => _timeCompresion; set { _timeCompresion = value; OnPropertyChanged(); } }
        public string TimeDecompresion { get => _timeDecompresion; set { _timeDecompresion = value; OnPropertyChanged(); } }
        public string SizeCompresionInputFile { get => _sizeCompresionInputFile; set { _sizeCompresionInputFile = value; OnPropertyChanged(); } }
        public string SizeDecompresionInputFile { get => _sizeDecompresionInputFile; set { _sizeDecompresionInputFile = value; OnPropertyChanged(); } }
        public string SizeCompresionOutputFile { get => _sizeCompresionOutputFile; set { _sizeCompresionOutputFile = value; OnPropertyChanged(); } }
        public string SizeDecompresionOutputFile { get => _sizeDecompresionOutputFile; set { _sizeDecompresionOutputFile = value; OnPropertyChanged(); } }

        public ObservableCollection<string> CodePageCollection { get => _codePageCollection; set => _codePageCollection = value; }

        public string SelectCodePageToCompresion { get => _selectCodePageToCompresion; set => _selectCodePageToCompresion = value; }

        public ICommand SelectTXTFile_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        try
                        {
                            PathFileCompresion = FileHelpfulFunctions.SelectFile("txt files (*.txt)|*.txt");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    });
            }
        }

        public ICommand SelectLZSSFile_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        try
                        {
                            PathFileDecompresion = FileHelpfulFunctions.SelectFile("lzss files (*.lzss)|*.lzss");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    });
            }
        }


        public ICommand CompresionAndSaveToFile_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        try
                        {
                            //string pathToSave = FileHelpfulFunctions.SelectPath("*.lzss");

                            string path = FileHelpfulFunctions.GetPathToSaveFileLZSS();
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            byte[] byteArray = LZSSFileHelperFuncions.TextFileToByteArray(PathFileCompresion);
                            SizeCompresionInputFile = byteArray.Length.ToString();

                            byte[] result = LZSS.Compresion(byteArray, CharSize, DictionarySize, BufferSize);
                            SizeCompresionOutputFile = result.Length.ToString();
                            //LZSSFileHelperFuncions.CompresDataToFile(pathToSave, result);

                            watch.Stop();
                            File.WriteAllBytes(path, result);
                            var elapsedMs = watch.ElapsedMilliseconds;
                            TimeCompresion = elapsedMs.ToString();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    });
            }
        }

        public ICommand DecompresionAndSaveToFile_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        try
                        {
                            string path = FileHelpfulFunctions.GetPathToSaveFileTXT();
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            byte[] byteArray = LZSSFileHelperFuncions.TextFileToByteArray(PathFileDecompresion);
                            SizeDecompresionInputFile = byteArray.Length.ToString();

                            byte[] result = LZSS.Decompresion(byteArray, CharSize, DictionarySize, BufferSize);
                            SizeDecompresionOutputFile= result.Length.ToString();

                            watch.Stop();
                            File.WriteAllBytes(path, result);
                            var elapsedMs = watch.ElapsedMilliseconds;
                            TimeDecompresion = elapsedMs.ToString();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    });
            }
        }

        public LZSSViewModel() {

            SelectCodePageToCompresion = CodePageCollection[0];


        }
    }
}
