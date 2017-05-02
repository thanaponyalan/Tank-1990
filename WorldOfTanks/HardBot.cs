using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    class HardBot : Tank
    {
        public HardBot(Point position)
        {
            point = position;
            size = 40; // размеры танка
            direction = Direction.South;
            hitpoints = 4;
            img = new Bitmap(Image.FromFile("../../HardBot.png"), new Size(size, size));
        }       
    }
}
