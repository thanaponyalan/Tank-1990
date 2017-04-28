using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WorldOfTanks
{
    public class PlayerTank : Tank
    {
        public PlayerTank()
        {
            size = 40; // размеры танка
            direction = Direction.North;
            hitpoints = 3;
            point = new Point(10, 10);
            img = new Bitmap(Image.FromFile("../../TT34.png"), new Size(size, size));
        }
    }
}
