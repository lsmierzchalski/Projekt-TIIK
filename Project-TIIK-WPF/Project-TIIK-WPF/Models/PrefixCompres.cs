using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF.Models
{
    public class PrefixCompres
    {
        public uint positon;
        public uint lenght;

        public PrefixCompres(uint p, uint l)
        {
            positon = p;
            lenght = l;
        }
    }
}
