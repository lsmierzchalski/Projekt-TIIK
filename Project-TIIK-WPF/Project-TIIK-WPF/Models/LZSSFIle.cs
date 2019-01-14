using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TIIK_WPF.Models
{
    class LZSSFIle
    {

        char first_character;

        List<Lzss_step> steps;

    }

    class Lzss_step
    {
        bool bit;

        int p;
        int c;

        char character;

        Lzss_step(int p, int c)
        {
            this.p = p;
            this.c = c;
        }

        Lzss_step(char character)
        {
            this.character = character;
        }
    }
}
