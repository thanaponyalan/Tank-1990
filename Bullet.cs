using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank1990
{
    // пуля
    public class Bullet : MoveElement
    {
        public Bullet(Tank tank)
        {
            Speed = 10;
            direction = tank.direction;
            SetWidthAndHeight(tank);
        }

        

        public event UpdateViewDelegate UpdateView; // отрисовка

        private void SetWidthAndHeight(Tank tank)
        {
            switch (direction)
            {
                case Direction.North:
                    point = new Point(tank.point.X + 14, tank.point.Y - 14);
                    Width = tank.Width;
                    Height = 14;
                    img = new Bitmap(Image.FromFile("../../Bullet_Top.png"));
                    break;
                case Direction.South:
                    point = new Point(tank.point.X + 14, tank.point.Y + tank.Height);
                    Width = tank.Width;
                    Height = 14;
                    img = new Bitmap(Image.FromFile("../../Bullet_Bottom.png"));
                    break;
                case Direction.West:
                    point = new Point(tank.point.X - 14, tank.point.Y + 14);
                    Width = 14;
                    Height = tank.Height;
                    img = new Bitmap(Image.FromFile("../../Bullet_Left.png"));
                    break;
                case Direction.East:
                    point = new Point(tank.point.X + tank.Width, tank.point.Y + 14);
                    Width = 14;
                    Height = tank.Height;
                    img = new Bitmap(Image.FromFile("../../Bullet_Right.png"));
                    break;
            }
        }

        public override void Move(Direction newDirection, IMap map)
        {
            switch (direction)
            {
                case Direction.North:
                    point = new Point(point.X, point.Y - Speed);
                    break;
                case Direction.South:
                    point = new Point(point.X, point.Y + Speed);
                    break;
                case Direction.West:
                    point = new Point(point.X - Speed, point.Y);
                    break;
                case Direction.East:
                    point = new Point(point.X + Speed, point.Y);
                    break;
            }

            UpdateView(null, null);
        }


    }
}
