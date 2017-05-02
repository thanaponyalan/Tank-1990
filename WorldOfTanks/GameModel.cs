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
        enum Bot
        {
            Easy,
            Medium,
            Hard
        }

        private delegate void CreateBotDelegate();

        public bool gameOver = false;
        public Tank player = new PlayerTank();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Tank> bots = new List<Tank>();
        private Point startPosition;
        public Map currentMap;
        private Random random = new Random();

        public GameModel()
        {
            CreateBotDelegate[] createBot = new CreateBotDelegate[3];
            createBot[0] = new CreateBotDelegate(CreateEasyBot);
            createBot[1] = new CreateBotDelegate(CreateMediumBot);
            createBot[2] = new CreateBotDelegate(CreateHardBot);
            currentMap = new Stage1(player);
            startPosition = currentMap.startPosition;

            for (int i = 0; i < 4; ++i)
            {
                int num = random.Next(0, 3);
                createBot[num]();
            }
        }

        private void CreateEasyBot()
        {
            int num = 0;
            Point position;
            do
            {
                num = random.Next(0, 6);
                position = currentMap.startPositionForBots[num];
            } while (!IsEmptyPosition(position));

            bots.Add(new EasyBot(position));
        }

        private void CreateMediumBot()
        {
            int num = 0;
            Point position;
            do
            {
                num = random.Next(0, 6);
                position = currentMap.startPositionForBots[num];
            } while (!IsEmptyPosition(position));

            bots.Add(new MediumBot(position));
        }

        private void CreateHardBot()
        {
            int num = 0;
            Point position;
            do
            {
                num = random.Next(0, 6);
                position = currentMap.startPositionForBots[num];
            } while (!IsEmptyPosition(position));

            bots.Add(new HardBot(position));
        }

        private bool IsEmptyPosition(Point point)
        {
            Rectangle newBot = new Rectangle(point, new Size(player.size, player.size));

            foreach (var b in bots)
            {
                Rectangle bot = new Rectangle(b.point, new Size(b.size, b.size));
                if (newBot.IntersectsWith(bot)) return false;
            }

            if (new Rectangle(player.point, new Size(player.size, player.size)).IntersectsWith(newBot)) return false;
            else return true;
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
            List<Bullet> b = new List<Bullet>();
            DeleteCrossedBullets();

            foreach (var bullet in bullets)
            {
                int dX = bullet.shift[bullet.direction].Key; // смещение по x
                int dY = bullet.shift[bullet.direction].Value; // смещение по y
                bullet.middle = new Point(bullet.middle.X + dX, bullet.middle.Y + dY);
                bullet.point = new Point(bullet.point.X + dX, bullet.point.Y + dY);

                if (!BulletCanMove(bullet))
                {
                    bullet.tank.isShooting = false;
                    b.Add(bullet);
                }
            }

            foreach (var toDelete in b)
            {
                bullets.Remove(toDelete);
            }
        }

        private bool TankCanMove(Rectangle tank) // проверяет, может ли танк двигаться в данном направлении
        {
            if (tank.Left < currentMap.mainFrame.Left || tank.Right > currentMap.mainFrame.Right
                || tank.Top < currentMap.mainFrame.Top || tank.Bottom > currentMap.mainFrame.Bottom)
            {
                return false;
            }

            foreach (var bot in bots)
            {
                Rectangle rect = new Rectangle(bot.point, new Size(bot.size, bot.size));
                if (rect.IntersectsWith(tank)) return false;
            }

            foreach (var s in currentMap.stone)
            {
                Rectangle rect = new Rectangle(s, new Size(currentMap.size, currentMap.size));
                if (rect.IntersectsWith(tank)) return false;
            }

            foreach (var b in currentMap.brick)
            {
                Rectangle rect = new Rectangle(b, new Size(currentMap.size / 2, currentMap.size / 2));
                if (rect.IntersectsWith(tank)) return false;
            }

            Rectangle eagle = new Rectangle(currentMap.pointEagle, new Size(currentMap.size, currentMap.size));
            if (eagle.IntersectsWith(tank)) return false;

            return true;
        }

        private void DeleteCrossedBullets()
        {
            List<Bullet> toDelete = new List<Bullet>();

            for (int i = 0; i < bullets.Count; ++i)
            {
                for (int j = 0; j < bullets.Count; ++j)
                {
                    if (i == j || toDelete.Contains(bullets[j])) continue;

                    Rectangle first = new Rectangle(new Point(bullets[i].middle.X - 10, bullets[i].middle.Y - 10), new Size(21, 21));

                    // попадание пули в другую пулю
                    Rectangle second = new Rectangle(new Point(bullets[j].middle.X - 10, bullets[j].middle.Y - 10), new Size(21, 21));
                    if (first.IntersectsWith(second))
                    {
                        bullets[i].tank.isShooting = false;
                        bullets[j].tank.isShooting = false;
                        toDelete.Add(bullets[i]);
                        toDelete.Add(bullets[j]);
                    }
                }
            }

            foreach(var del in toDelete)
            {
                bullets.Remove(del);
            }
        }

        private bool BulletCanMove(Bullet bullet)
        {
            Rectangle rect;

            rect = currentMap.mainFrame;
            if (bullet.middle.X <= rect.Left || bullet.middle.X >= rect.Right
                || bullet.middle.Y <= rect.Top || bullet.middle.Y >= rect.Bottom) return false;

            foreach (var s in currentMap.stone)
            {
                rect = new Rectangle(s, new Size(currentMap.size, currentMap.size));
                if (rect.Contains(bullet.middle)) return false;
            }

            List<int> indexes = new List<int>();
            int index = 0;
            bool result = true;
            // для расчистки прохода под размер танка
            rect = new Rectangle(new Point(bullet.middle.X - 10, bullet.middle.Y - 10), new Size(21, 21));
            foreach (var b in currentMap.brick)
            {
                Rectangle brick = new Rectangle(b, new Size(currentMap.size / 2, currentMap.size / 2));
                if (brick.IntersectsWith(rect))
                {
                    indexes.Add(index);
                    result = false;
                }
                index++;
            }

            int coef = 0;
            foreach (var i in indexes) { this.currentMap.brick.RemoveAt(i - coef); coef++; }

            rect = new Rectangle(currentMap.pointEagle, new Size(currentMap.size, currentMap.size));
            if (rect.Contains(bullet.middle)) return false;

            return result;
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
