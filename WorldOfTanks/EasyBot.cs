using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    class EasyBot : Bot
    {
        public bool canMove = true;

        public EasyBot(Point position)
        {
            point = position;
            size = 40; // размеры танка
            direction = Direction.South;
            hitpoints = 1;
            img = new Bitmap(Image.FromFile("../../EasyBot.png"), new Size(size, size));
        }
    }
}
