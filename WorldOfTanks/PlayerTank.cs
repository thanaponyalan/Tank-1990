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
        private int size = 40;

        public PlayerTank()
        {
            direction = Direction.North;
            hitpoints = 3;
            tank = new Rectangle(new Point(10, 10), new Size(size, size));
            img = new Bitmap(Image.FromFile("../../TT34.png"), new Size(40, 40));
        }

        public override void Move(Direction direction)
        {
            int dX = shift[direction].Key; // смещение по x
            int dY = shift[direction].Value; // смещение по y

            if (this.direction == direction)
            {
                tank = new Rectangle(new Point(tank.X + dX, tank.Y + dY), new Size(size, size));
            }

            else
            {
                RotateImage(direction); // поворачиваем картинку в нужном направлении
                this.direction = direction;
            }
        }

        public override void Shoot()
        {
            throw new NotImplementedException();
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
    }
}
