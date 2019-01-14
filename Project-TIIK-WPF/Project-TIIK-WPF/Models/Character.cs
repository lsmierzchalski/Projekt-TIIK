using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF.Models
{
    public class Character
    {
        public byte[] sign; 

        public Character(byte[] s)
        {
            sign = s;
        }
    }
}
