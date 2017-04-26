using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank1990
{
    public abstract class Tank : MoveElement
    {
        public Point Gun { get; set; } // координаты пушки танка
        public int HitPoints { get; set; } // количество жизней
        public Dictionary<Direction, int> windRose = new Dictionary<Direction, int>(); // роза ветров

        public Tank()
        {
            windRose.Add(Direction.North, 0);
            windRose.Add(Direction.East, 90);
            windRose.Add(Direction.West, 270);
            windRose.Add(Direction.South, 180);
        } 
    }
}
