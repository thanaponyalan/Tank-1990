using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldOfTanks
{
    public class GameModel
    {
        public bool gameOver = false;
        public Tank player = new PlayerTank();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Tank> bot;
        private Point startPosition;
        public Map currentMap;

        public GameModel()
        {
            currentMap = new Stage1(player);
            startPosition = currentMap.startPosition;

        }


        public void Move(Tank tank, Direction direction) // перемещение танка пользователя
        {
            int dX = tank.shift[direction].Key; // смещение по x
            int dY = tank.shift[direction].Value; // смещение по y

            if (tank.direction == direction)
            {
                Point newPoint = new Point((tank.point.X + dX), (tank.point.Y + dY));
                Rectangle rect = new Rectangle(newPoint, new Size(tank.size, tank.size));

                if (TankCanMove(rect))
                {
                    tank.point = newPoint;
                }
            }

            else
            {
                RotateImage(tank, direction); // поворачиваем картинку в нужном направлении
                tank.direction = direction;
            }
        }

        public void Shoot(Tank tank)
        {
            if (!tank.isShooting)
            {
                Bullet bullet = new Bullet(tank);
                bullets.Add(new Bullet(tank));
                tank.isShooting = true;
            }
        }

        public void MoveBullet()
        {
            List<int> index = new List<int>();
            int i = 0;
            foreach (var bullet in bullets)
            {
                int dX = bullet.shift[bullet.direction].Key; // смещение по x
                int dY = bullet.shift[bullet.direction].Value; // смещение по y
                bullet.middle = new Point(bullet.middle.X + dX, bullet.middle.Y + dY);
                bullet.point = new Point(bullet.point.X + dX, bullet.point.Y + dY);

                if (!BulletCanMove(bullet.middle))
                {
                    bullet.tank.isShooting = false;
                    index.Add(i);
                }
                i++;
            }

            foreach (var j in index)
            {
                bullets.RemoveAt(j);
            }
        }

        private bool TankCanMove(Rectangle tank) // проверяет, может ли танк двигаться в данном направлении
        {
            if (tank.Left < currentMap.mainFrame.Left || tank.Right > currentMap.mainFrame.Right
                || tank.Top < currentMap.mainFrame.Top || tank.Bottom > currentMap.mainFrame.Bottom)
            {
                return false;
            }

            foreach (var s in currentMap.stone)
            {
                Rectangle rect = new Rectangle(s, new Size(currentMap.size, currentMap.size));
                if (rect.IntersectsWith(tank)) return false;
            }

            foreach (var b in currentMap.brick)
            {
                Rectangle rect = new Rectangle(b, new Size(currentMap.size, currentMap.size));
                if (rect.IntersectsWith(tank)) return false;
            }

            Rectangle eagle = new Rectangle(currentMap.pointEagle, new Size(currentMap.size, currentMap.size));
            if (eagle.IntersectsWith(tank)) return false;

            return true;
        }

        private bool BulletCanMove(Point point)
        {
            Rectangle rect = currentMap.mainFrame;
            if (point.X <= rect.Left || point.X >= rect.Right
                || point.Y <= rect.Top || point.Y >= rect.Bottom) return false;

            foreach (var s in currentMap.stone)
            {
                Rectangle stone = new Rectangle(s, new Size(currentMap.size, currentMap.size));
                if (stone.Contains(point)) return false;
            }

            int index = 0;
            foreach (var b in currentMap.brick)
            {
                Rectangle brick = new Rectangle(b, new Size(currentMap.size, currentMap.size));
                if (brick.Contains(point))
                {
                    currentMap.brick.RemoveAt(index);
                    return false;
                }
                index++;
            }

            Rectangle eagle = new Rectangle(currentMap.pointEagle, new Size(currentMap.size, currentMap.size));
            if (eagle.Contains(point)) return false;

            return true;
        }


        private static void RotateImage(Tank tank, Direction newDirection) // поворачиваем картинку на нужный угол
        {
            int angle = tank.windRose[newDirection] - tank.windRose[tank.direction];
            angle = angle < 0 ? angle + 360 : angle;

            switch (angle)
            {
                case 270:
                    tank.img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case 180:
                    tank.img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 90:
                    tank.img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
            }
        }
    }
}
