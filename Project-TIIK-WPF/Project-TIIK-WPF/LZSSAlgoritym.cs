using Project_TIIK_WPF.Models;
using System;
using System.Collections.Generic;

namespace Project_TIIK_WPF
{
    class LZSSAlgoritym
    {

        public bool[] Compresion(byte[] data, uint charSize, uint k, uint n)
        {
            bool[] result2 = { false };
            byte[] buffor = { 0 };
            List<bool> result = new List<bool>();

            if (data.Length > 0)
            {
                byte[] firstChar = GetChar(data, charSize);
                result = ByteArrayAddToList(result, firstChar);
                if (data.Length >= n)
                {
                    buffor = GetChar(data, charSize * n);
                }
                else
                {
                    buffor = data;
                }
                
                byte[] dictionary = new byte[charSize * k];
                int x = 0;
                for (int i=0; i<k; i++)
                {
                    for(int j=0; j<charSize; j++)
                    {
                        dictionary[x] = firstChar[j];
                        x++;
                    }
                }

                while (buffor.Length != 0)
                {
                    
                }
                
            }

            return result2;
        }

        private byte[] GetChar(byte[] data, uint charSize)
        {
            List<Byte> character = new List<Byte>();

            for(int i=0; i<charSize; i++)
            {
                character.Add(data[i]);
            }

            return character.ToArray();
        }

        private static List<bool> ByteArrayAddToList(List<bool> list, byte[] array)
        {
            for(int i=0; i<array.Length; i++)
            {
                bool[] boolarray = ConvertByteToBoolArray(array[i]);
                for (int j = 0; j < boolarray.Length; j++)
                {
                    list.Add(boolarray[j]);
                }
            }

            return list;
        }

        private static byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 8 - source.Length;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }

        private static bool[] ConvertByteToBoolArray(byte b)
        {
            // prepare the return result
            bool[] result = new bool[8];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < 8; i++)
                result[i] = (b & (1 << i)) == 0 ? false : true;

            // reverse the array
            Array.Reverse(result);

            return result;
        }

        private static uint GetSizeTwo(uint charSize, uint k, uint n)
        {

            return 0;
        }

        private static uint GetSizeThree(uint charSize, uint k, uint n)
        {

            return 0;
        }

        private static List<PrefixCompres> GetListPrefixes(byte[] dictionary, byte[] buffer, uint charSize)
        {
            List<PrefixCompres> result = new List<PrefixCompres>();
            byte[] windows = addArrayToArrayByte(dictionary, buffer);

            for (uint i=0; i < buffer.Length / charSize; i++)
            {
                bool exitst = false;
                for (uint j = 0; j < dictionary.Length / charSize - i - 1; j++)
                {
                    //if (EqualByteArrayStringsXD(GetByteArrayString(windows,j,i,charSize), GetByteArrayString(buffer, j, i, charSize))
                }
            }

            return result;
        }

        private bool EqualByteArrayStringsXD(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            else
            {
                for (int i = 0; i < a.Length; i++) {
                    if (a[i] != b[i]) return false;
                }
            }
            return true;
        }

        private byte[] GetByteArrayString(byte[] data, uint p, uint s, uint charSize)
        {
            List<byte> result = new List<byte>();
            for(int i=0; i<s*charSize; i++)
            {
                result.Add(data[i + p*charSize]);
            }
            return result.ToArray();
        }

        public static byte[] addArrayToArrayByte(byte[] a, byte[] b)
        {
            List<byte> result = new List<byte>();
            result.AddRange(a);
            result.AddRange(b);
            return result.ToArray();
        }

        public static byte[] addByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 1);
            newArray[0] = newByte;
            return newArray;
        }

    }
}
