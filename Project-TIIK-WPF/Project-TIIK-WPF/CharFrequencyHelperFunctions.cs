using Project_TIIK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF
{
    public static class CharFrequencyHelperFunctions
    {
        public static ObservableCollection<CharFrequency> GetListCharFrequency(string text)
        {
            ObservableCollection<CharFrequency> list = new ObservableCollection<CharFrequency>();

            foreach(char character in text)
            {
                CharFrequency item = GetItemIfCharIsOnList(character, list);
                if (item != null) {
                    item.Frequency++;
                }
                else
                {
                    list.Add(new CharFrequency(character));
                }
            }

            return list;
        }

        private static CharFrequency GetItemIfCharIsOnList(char character, ObservableCollection<CharFrequency> list)
        {
            foreach(var item in list)
            {
                if(item.Character == character)
                {
                    return item;
                }
            }
            return null;
        }
        
    }
}
