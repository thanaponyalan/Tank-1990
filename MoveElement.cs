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

    public abstract class MoveElement
    {
        public int Width { get; set; } // ширина объекта
        public int Height { get; set; } // длина объекта
        public int Speed { get; set; } // скорость передвижения объекта в пикселях
        public Direction direction { get; set; } // направление движения объекта
        public Point point { get; set; } // координаты левого верхнего угла объекта
        public Bitmap img;

        public abstract void Move(Direction newDirection, IMap map); // перемещение объекта 
    }
}
