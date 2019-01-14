using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static string SelectFile(string filter)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = filter;
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }

            return filePath;
        }

        public static string SelectPath(string type)
        {
            string dummyFileName = "Save Here";

            SaveFileDialog sf = new SaveFileDialog();
            // Feed the dummy name to the save dialog
            sf.FileName = dummyFileName;
            
            string savePath = string.Empty;
            if (sf.ShowDialog() == true)
            {
                // Now here's our save folder
                savePath = Path.GetDirectoryName(sf.FileName);
                // Do whatever
            }

            return savePath;
        }

        public static void SaveFileLZSS(byte[] data)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "LZSS File|*.lzss";
            saveFileDialog1.Title = "Save an Compres File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  

                string path = fs.Name;
                fs.Close();
                File.WriteAllBytes(path, data);
            }
        }

        public static string GetPathToSaveFileLZSS()
        {
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "LZSS File|*.lzss";
            saveFileDialog1.Title = "Save an Compres File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            string path = string.Empty;
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  

                path = fs.Name;
                fs.Close();
            }
            return path;
        }
        
        public static string GetPathToSaveFileTXT()
        {
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.txt";
            saveFileDialog1.Title = "Save an Compres File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            string path = string.Empty;
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  

                path = fs.Name;
                fs.Close();
            }
            return path;
        }
    }
}
