using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank1990
{


    public delegate void UpdateViewDelegate(object sender, PaintEventArgs e);

    public abstract class Tank
    {
        public Bitmap img;
        public Direction direction { get; set; } // направление движения танка
        public Point TopLeftCorner { get; set; } // координаты левого верхнего угла танка
        public int Width { get; set; } // ширина танка
        public int Length { get; set; } // длина танка
        public Point Gun { get; set; } // координаты пушки танка
        public int Speed { get; set; } // скорость передвижения танка в пикселях
        public int HitPoints { get; set; } // количество жизней
        
        public abstract void Move(Direction newDirection, IMap map); // перемещение танка
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
