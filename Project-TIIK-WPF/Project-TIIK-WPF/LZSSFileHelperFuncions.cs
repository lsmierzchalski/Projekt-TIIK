using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF
{
    public static class LZSSFileHelperFuncions
    {

        public static bool[] LZSSFileObjectToBoolArray(string filePath, string codePage)
        {
            List<bool> result = new List<bool>();

            var text = File.ReadAllText(filePath, Encoding.GetEncoding(codePage));

            return result.ToArray();
        }

        public static byte[] TextFileToByteArray(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static void CompresDataToFile(string filePath, byte[] data)
        {
            File.WriteAllBytes(filePath, data);
        }
    }
}
