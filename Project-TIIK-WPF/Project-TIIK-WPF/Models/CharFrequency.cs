using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF.Models
{
    public class CharFrequency : ObservableObject
    {
        private char _character;
        public char Character {
            get { return _character; }
            set
            {
                _character = value;
                OnPropertyChanged();
            }
        }

        public int DecimalValue
        {
            get { return (int)Character; }
        }

        private int _frequency;
        public int Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                OnPropertyChanged();
            }
        }

        public CharFrequency(char character)
        {
            Character = character;
            Frequency = 1;
        }

    }
}
