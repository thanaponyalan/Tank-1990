using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank1990
{
    class PlayerTank : Tank, IMoveElement
    {
        public PlayerTank()
        {
            Speed = 10;
            HitPoints = 3;
            direction = Direction.North;
            Length = Width = 40;
            TopLeftCorner = new Point(50, 50);
            img = new Bitmap(Image.FromFile("../../TT34.png"), new Size(Width, Length));

        }

        public event UpdateViewDelegate UpdateView; // отрисовка

        public override void Move(Direction newDirection, IMap map) // перемещение танка
        {

            if (direction == newDirection)
            {
                for (int i = 0; i < Speed; ++i)
                {
                    switch (direction)
                    {
                        case Direction.North:
                            TopLeftCorner = new Point(TopLeftCorner.X, TopLeftCorner.Y - 1);
                            break;
                        case Direction.South:
                            TopLeftCorner = new Point(TopLeftCorner.X, TopLeftCorner.Y + 1);
                            break;
                        case Direction.West:
                            TopLeftCorner = new Point(TopLeftCorner.X - 1, TopLeftCorner.Y);
                            break;
                        case Direction.East:
                            TopLeftCorner = new Point(TopLeftCorner.X + 1, TopLeftCorner.Y);
                            break;
                    }
                    UpdateView(null, null);
                }
            }


            else
            {
                RotateImage(direction, newDirection);
                direction = newDirection;
                UpdateView(null, null);
            }
        }

        private void RotateImage(Direction previous, Direction next) // поворачиваем картинку на нужный угол
        {
            int angle = windRose[next] - windRose[previous];
            angle = angle < 0 ? angle + 360 : angle;

            switch (angle)
            {
                case 270:
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case 180:
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 90:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
            }
        }
    }
}
