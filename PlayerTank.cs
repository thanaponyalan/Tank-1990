using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank1990
{
    public class PlayerTank : Tank
    {
        public PlayerTank()
        {
            Speed = 10;
            HitPoints = 3;
            direction = Direction.North;
            Height = Width = 40;
            point = new Point(50, 50);
            Gun = new Point(point.X + Width / 2, point.Y);
            img = new Bitmap(Image.FromFile("../../TT34.png"), new Size(Width, Height));
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
                            point = new Point(point.X, point.Y - 1);
                            break;
                        case Direction.South:
                            point = new Point(point.X, point.Y + 1);
                            break;
                        case Direction.West:
                            point = new Point(point.X - 1, point.Y);
                            break;
                        case Direction.East:
                            point = new Point(point.X + 1, point.Y);
                            break;
                    }

                    
                    UpdateView(null, null);
                }
            }

            else
            {
                RotateImage(newDirection);
                direction = newDirection;
                UpdateView(null, null);
            }

            SetGunCoordinates();
        }

        private void RotateImage(Direction newDirection) // поворачиваем картинку на нужный угол
        {
            int angle = windRose[newDirection] - windRose[direction];
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

        private void SetGunCoordinates() // устанавливаем координаты пушки
        {
            switch (direction)
            {
                case Direction.North:
                    Gun = new Point(point.X + Width / 2, point.Y);
                    break;
                case Direction.South:
                    Gun = new Point(point.X + Width / 2, point.Y + Height);
                    break;
                case Direction.West:
                    Gun = new Point(point.X, point.Y + Height / 2);
                    break;
                case Direction.East:
                    Gun = new Point(point.X + Width, point.Y + Height / 2);
                    break;
            }
        }
    }
}
