using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank1990
{
    class PlayerTank : ITank, IMoveElement
    {
        public Direction direction { get; set; }
        public Point Gun { get; set; }
        public int HitPoints { get; set; }
        public int Length { get; set; }
        public int Speed { get; set; }
        public Point TopLeftCorner { get; set; }
        public int Width { get; set; }

        public PlayerTank()
        {
            Speed = 30;
            HitPoints = 3;
            direction = Direction.North;
            Length = Width = 60;
            TopLeftCorner = new Point(50,50);
        }
        public event UpdateViewDelegate UpdateView; // отрисовка

        public void Move(IMap map) // перемещение танка
        {
            switch (direction)
            {
                case Direction.North:
                    TopLeftCorner = new Point(TopLeftCorner.X, TopLeftCorner.Y - Speed); 
                    break;
                case Direction.South:
                    TopLeftCorner = new Point(TopLeftCorner.X, TopLeftCorner.Y + Speed);
                    break;
                case Direction.West:
                    TopLeftCorner = new Point(TopLeftCorner.X - Speed, TopLeftCorner.Y);
                    break;
                case Direction.East:
                    TopLeftCorner = new Point(TopLeftCorner.X + Speed, TopLeftCorner.Y);
                    break;
            }

          
            UpdateView(null, null);

        }
    }
}
