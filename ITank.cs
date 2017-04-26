using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank1990
{
    public delegate void UpdateViewDelegate(Graphics g, IMap map);

    public interface ITank
    {
        Direction direction { get; set; } // направление движения танка
        Point TopLeftCorner { get; set; } // координаты левого верхнего угла танка
        int Width { get; set; } // ширина танка
        int Length { get; set; } // длина танка
        Point Gun { get; set; } // координаты пушки танка
        int Speed { get; set; } // скорость передвижения танка в пикселях/тик таймера 
        int HitPoints { get; set; } // количество жизней
        event UpdateViewDelegate UpdateView; // отрисовка
        void Move(Direction direction, IMap map); // перемещение танка
    }
}
