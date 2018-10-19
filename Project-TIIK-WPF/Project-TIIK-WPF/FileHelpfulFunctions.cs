using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF
{
    public static class FileHelpfulFunctions
    {
        public static string SelectFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Title = "Wybierz plik tekstowy";
            if (dialog.ShowDialog() == true)
            {
                string fname = dialog.FileName;
                return System.IO.File.ReadAllText(fname);
            }
            return string.Empty;
        }
    }
}
