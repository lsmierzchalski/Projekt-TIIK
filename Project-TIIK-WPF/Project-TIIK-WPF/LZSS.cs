using Project_TIIK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF
{
    class LZSS
    {
        public static byte[] Compresion(byte[] data, uint charSize, uint k, uint n)
        {
            List<Character> text = new List<Character>();
            List<Character> buffor = new List<Character>();
            List<Character> dictionary = new List<Character>();
            List<Character> window = new List<Character>();
            List<bool> result = new List<bool>();

            text = ByteArrayToListChar(data, charSize);

            //text = DeleteSubText(text, 0, n);
 
            uint positionBitSize = UInt32.Parse(Convert.ToString(k-1, 2).Length.ToString());
            uint lenghtBitSize = UInt32.Parse(Convert.ToString(n-1, 2).Length.ToString());

            uint size1S = 1 + charSize * 8;
            uint size0PC = positionBitSize + lenghtBitSize;

            if (text.Capacity > 0)
            {
                Character firstChar = GetChar(text, 0);
                result = ByteArrayAddToList(result, firstChar.sign);
                if (text.Capacity >= n)
                {
                    buffor = GetSubText(text, 0, n);
                }
                else
                {
                    buffor = text;
                }

                for (int i = 0; i < k; i++)
                {
                    dictionary.Add(buffor[0]);
                }

                uint s = 0;
                while (buffor.Count != 0)
                {
                    uint l = 0;
                    List<PrefixCompres> prefixes = GetPrefixes(dictionary, buffor);

                    bool bit = true;
                    if (prefixes.Count > 0)
                    {
                        if (size0PC < prefixes.Last().lenght * 8)
                        {
                            bit = false;
                            l = prefixes.Last().lenght;
                        }
                    }
                    List<Character> tmp = new List<Character>();
                    tmp.AddRange(dictionary);
                    tmp.AddRange(buffor);
                    result.Add(bit);
                    if (bit)
                    {
                        result = CharacterAddToList(result, buffor[0]);
                        s += 1;
                        dictionary = GetSubText(tmp, 1, k);
                    }
                    else
                    {
                        result.AddRange(UintToBoolArray(prefixes.Last().positon, positionBitSize));
                        result.AddRange(UintToBoolArray(prefixes.Last().lenght-1, lenghtBitSize));
                        s += l;
                        dictionary = GetSubText(tmp, l, k);
                    }

                    if(s + n <= text.Count)
                    {
                        buffor = GetSubText(text, s, n);
                    }
                    else
                    {
                        buffor = GetSubText(text, s, UInt32.Parse((text.Count - s).ToString()));
                    }
                }
            }

            return ConvertBoolListToByteArray(result);
        }

        public static byte[] Decompresion(byte[] data, uint charSize, uint k, uint n)
        {
            List<bool> bits = new List<bool>();
            List<Character> buffor = new List<Character>();
            List<Character> dictionary = new List<Character>();
            List<Character> window = new List<Character>();
            List<Character> result = new List<Character>();

            bits = ByteArrayToListBool(data);

            //text = DeleteSubText(text, 0, n);

            uint positionBitSize = UInt32.Parse(Convert.ToString(k - 1, 2).Length.ToString());
            uint lenghtBitSize = UInt32.Parse(Convert.ToString(n - 1, 2).Length.ToString());

            uint size1S = 1 + charSize * 8;
            uint size0PC = 1+positionBitSize + lenghtBitSize;

            if (bits.Count > 0)
            {
                Character firstChar = GetCharFromBoolList(bits, charSize);

                for (int i = 0; i < k; i++)
                {
                    dictionary.Add(firstChar);
                }

                uint j = 8 * charSize;
                while (bits.Count-j+1> size1S || bits.Count - j+1 > size0PC)
                {
                    bool bit = bits[Int32.Parse(j.ToString())];

                    List<Character> tmp = new List<Character>();
                    buffor.Clear();
                    if (bit)
                    {
                        tmp.Add(GetCharFromBoolList(bits, charSize, j + 1));
                        result.Add(tmp[0]);
                        j += size1S;
                    }
                    else
                    {
                        uint p = GetNumberFromBits(bits, j + 1, positionBitSize);
                        uint c = GetNumberFromBits(bits, j + 1 + positionBitSize, lenghtBitSize)+1;
                        tmp = GetSubText(dictionary, p, c);
                        result.AddRange(tmp);
                        j += size0PC;
                    }

                    List<Character> tmp2 = new List<Character>();
                    tmp2.AddRange(dictionary);
                    tmp2.AddRange(tmp);

                    dictionary = GetSubText(tmp2, UInt32.Parse(tmp.Count.ToString()), k);
                }
            }

            return ListCharToByteArray(result, charSize);
        }

        public static byte[] ListCharToByteArray(List<Character> list, uint charSize)
        {
            List<byte> result = new List<byte>();
            for (uint i = 0; i < list.Count; i++)
            {
                for (uint j = 0; j < charSize; j++)
                {
                    result.Add(list[Int32.Parse((i * charSize).ToString())].sign[j]);
                }
            }
            return result.ToArray();
        }

        private static uint GetNumberFromBits(List<bool> data, uint p, uint size)
        {
            string result = string.Empty;
            for(int i=0; i<size; i++)
            {
                if (data[Int32.Parse(p.ToString())]) result += '1';
                else result += '0';
                p++;
            }
            return UInt32.Parse(Convert.ToInt32(result, 2).ToString());
        }

        private static Character GetCharFromBoolList(List<bool> data, uint charSize, uint p)
        {
            return GetStringFromBoolList(data, charSize, 1, p)[0];
        }

        private static List<Character> GetStringFromBoolList(List<bool> data, uint charSize, uint l, uint p)
        {
            List<Character> result = new List<Character>();
            for (uint i = 0; i < l; i++)
            {
                List<bool> tmp = new List<bool>();
                for (uint j = 0; j < charSize * 8; j++)
                {
                    tmp.Add(data[Int32.Parse((p + i * charSize * 8 + j).ToString())]);
                }
                result.Add(new Character(ConvertBoolListToByteArray(tmp)));
            }
            return result;
        }

        private static Character GetCharFromBoolList(List<bool> data, uint charSize)
        {
            return GetStringFromBoolList(data, charSize, 1)[0];
        }

        private static List<Character> GetStringFromBoolList(List<bool> data, uint charSize, uint l)
        {
            List<Character> result = new List<Character>();
            for(uint i=0; i<l; i++)
            {
                List<bool> tmp = new List<bool>();
                for(uint j=0; j<charSize*8; j++)
                {
                    tmp.Add(data[Int32.Parse((i * charSize * 8 + j).ToString())]);
                }
                result.Add(new Character(ConvertBoolListToByteArray(tmp)));
            }
            return result;
        }

        private static List<bool> ByteArrayToListBool(byte[] array)
        {
            List<bool> result = new List<bool>();
            for (int i = 0; i < array.Length; i++)
            {
                string tmp = Convert.ToString(array[i], 2);
                int k = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (j < 8 - tmp.Length) result.Add(false);
                    else
                    {
                        if (tmp[k] == '0') result.Add(false);
                        else result.Add(true);
                        k++;
                    }
                }
            }
            return result;
        }



        // ==================================

        public static List<bool> UintToBoolArray(uint n, uint size)
        {
            string stringBoolArray = Convert.ToString(n, 2);
            List<bool> result = new List<bool>();
            for (int i=0, j=0; i<size; i++)
            {
                if(i<size - stringBoolArray.Length)
                {
                    result.Add(false);
                }
                else
                {
                    if(stringBoolArray[j]=='0') result.Add(false);
                    else result.Add(true);
                    j++;
                }
            }

            return result;
        }

        public static List<PrefixCompres> GetPrefixes(List<Character> dictionary, List<Character> buffer)
        {
            List<PrefixCompres> result = new List<PrefixCompres>();

            for (uint i = 1; i <= buffer.Count; i++)
            {
                bool stop = true;
                List<Character> prefix = GetSubText(buffer, 0, i);
                bool exitst = false;
                for (uint k = 0; k < dictionary.Count-prefix.Count; k++)
                {
                    List<Character> tmp = GetSubText(dictionary, k, i);
                    if (EqualAreToListChar(tmp, prefix))
                    {
                        result.Add(new PrefixCompres(k,i));
                        stop = false;
                        break;
                    }
                }
                if (stop) break;
            }

            return result;
        }

        public static List<Character> ByteArrayToListChar(byte[] data, uint charSize)
        {
            if (data.Length % charSize != 0) return null;
            List<Character> text = new List<Character>();
            List<byte> tmp = new List<byte>();
            for (int i = 0; i < data.Length / charSize; i++)
            {
                for (int j = 0; j < charSize; j++)
                {
                    tmp.Add(data[i + j]);
                }
                text.Add(new Character(tmp.ToArray()));
                tmp.Clear();
            }

            return text;
        }

        public static List<Character> GetSubText(List<Character> text, uint p, uint s)
        {
            List<Character> result = new List<Character>();
            for (uint i = p; i < text.Count && i < p+s; i++)
            {
                result.Add(text[Int32.Parse(i.ToString())]);
            }
            return result;
        }

        public static List<Character> GetSubText(List<Character> text, uint s)
        {
            return GetSubText(text, 0, s);
        }

        public static Character GetChar(List<Character> text, uint p)
        {
            return GetSubText(text, p, 1)[0];
        }

        public static List<Character> DeleteSubText(List<Character> text, uint p, uint s)
        {
            for (uint i = p; i < text.Count && i < p+s; i++)
            {
                text.RemoveAt(Int32.Parse(i.ToString()));
            }
            return text;
        }

        private static List<bool> CharacterAddToList(List<bool> list, Character character)
        {
            return ByteArrayAddToList(list, character.sign);
        }

        private static List<bool> ByteArrayAddToList(List<bool> list, byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                bool[] boolarray = ConvertByteToBoolArray(array[i]);
                for (int j = 0; j < boolarray.Length; j++)
                {
                    list.Add(boolarray[j]);
                }
            }

            return list;
        }

        public static byte[] ConvertBoolListToByteArray(List<bool> array)
        {
            List<byte> result = new List<byte>();
            
            List<bool> tmp = new List<bool>();
            int max = array.Count / 8;
            if (array.Count % 8 != 0) max++;
            for (int i=0; i<max; i++)
            {
                for (int j=0; j<8; j++)
                {
                    int x = i * 8 + j;
                    if (x >= array.Count) tmp.Add(false);
                    else tmp.Add(array[x]);
                }
                result.Add(ConvertBoolArrayToByte(tmp.ToArray()));
                tmp.Clear();
            }
            return result.ToArray();
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

        private static bool EqualAreToListChar(List<Character> a, List<Character> b)
        {
            if (a.Count != b.Count) return false;
            else
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (!EqualAreteArray(a[i].sign,b[i].sign)) return false;
                }
            }
            return true;
        }

        private static bool EqualAreteArray(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i]) return false;
                }
            }
            return true;
        }
    }
}
