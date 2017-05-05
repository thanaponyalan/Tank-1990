using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldOfTanks
{
    public enum BotDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class GameModel
    {
        private delegate void CreateBotDelegate();
        private int currentBotAmount; // суммарное кол-во ботов на карте
        private int simultaneousBotAmount; // одновременное кол-во ботов на карте
        private int maxBotAmount; // максимальное суммарное кол-во ботов на карте
        private int maxStepsInOneDirection = 80; // максимально кол-во шагов в одном направлении для бота
        public bool gameOver = false;
        public bool playerWin = false;
        public Tank player = new PlayerTank();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Tank> bots = new List<Tank>();
        private Point playerStartPosition; // стартовая позиция игрока
        public Map currentMap;
        private Random random = new Random();
        public Dictionary<int, Direction> directionForBot = new Dictionary<int, Direction>();
        private CreateBotDelegate[] createBot = new CreateBotDelegate[3];

        public GameModel(Map map, GameDifficulty difficulty)
        {
            SetDifficulty(difficulty);
            directionForBot.Add(0, Direction.East);
            directionForBot.Add(1, Direction.North);
            directionForBot.Add(2, Direction.South);
            directionForBot.Add(3, Direction.West);
            createBot[0] = new CreateBotDelegate(CreateEasyBot);
            createBot[1] = new CreateBotDelegate(CreateMediumBot);
            createBot[2] = new CreateBotDelegate(CreateHardBot);
            if (map is Stage1) currentMap = new Stage1();
            else if (map is Stage2) currentMap = new Stage2();
            player.point = playerStartPosition = currentMap.startPosition;
        }

        public void ChangeMap(Map newMap)
        {
            currentBotAmount = 0;
            if (newMap is Stage1) currentMap = new Stage1();
            else if (newMap is Stage2) currentMap = new Stage2();
            gameOver = false;
            playerWin = false;
            player = new PlayerTank();
            player.point = playerStartPosition = currentMap.startPosition;
            for (int i = 0; i < currentBotAmount; ++i) { AddBot(); }
        }

        private void CreateEasyBot()
        {
            int num = 0;
            Point position;
            do
            {
                num = random.Next(0, currentMap.startPositionForBots.Count);
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
                num = random.Next(0, currentMap.startPositionForBots.Count);
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
                num = random.Next(0, currentMap.startPositionForBots.Count);
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
                Point previous = tank.point;
                tank.point = new Point((tank.point.X + dX), (tank.point.Y + dY));


                if (!TankCanMove(tank))
                {
                    if (!(tank is PlayerTank)) { ((Bot)(tank)).canMove = false; ((Bot)(tank)).countStep = 0; }
                    tank.point = previous;

                }

                else
                {
                    if (!(tank is PlayerTank)) { ((Bot)(tank)).canMove = true; ((Bot)(tank)).countStep++; }
                }
            }

            else
            {
                if (!(tank is PlayerTank)) { ((Bot)(tank)).canMove = true; ((Bot)(tank)).countStep = 0; }
                RotateImage(tank, direction); // поворачиваем картинку в нужном направлении
                tank.direction = direction;
            }
        }

        public void MoveBots(BotDifficulty difficulty) // перемещение ботов
        {
            foreach (var bot in bots)
            {
                if (difficulty == BotDifficulty.Easy && bot is EasyBot ||
                    difficulty == BotDifficulty.Medium && bot is MediumBot ||
                    difficulty == BotDifficulty.Hard && bot is HardBot)
                {
                    if (((Bot)bot).canMove && ((Bot)bot).countStep < maxStepsInOneDirection) Move(bot, ((Bot)bot).direction);

                    else
                    {
                        int direction = random.Next(0, 4);
                        Move(bot, directionForBot[direction]);
                    }
                }
            }
        }

        public void AddBot() // добавление бота на карту
        {
            if (currentBotAmount == maxBotAmount && bots.Count == 0 && gameOver == false) playerWin = true;

            else if (bots.Count < simultaneousBotAmount && currentBotAmount < maxBotAmount)
            {
                int num = random.Next(0, 3);
                createBot[num]();
                currentBotAmount++;
            }

        }

        public void Shoot(Tank tank)
        {
            if (tank is Bot && random.Next(0, 2) == 1) return;

            if (!tank.isShooting)
            {
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

        private bool TankCanMove(Tank a_tank) // проверяет, может ли танк двигаться в данном направлении
        {
            Rectangle tank = new Rectangle(a_tank.point, new Size(a_tank.size, a_tank.size));

            if (tank.Left < Map.mainFrame.Left || tank.Right > Map.mainFrame.Right
                || tank.Top < Map.mainFrame.Top || tank.Bottom > Map.mainFrame.Bottom)
            {
                return false;
            }

            if (a_tank is Bot)
            {
                Rectangle rect = new Rectangle(player.point, new Size(player.size, player.size));
                if (rect.IntersectsWith(tank)) return false;
            }

            foreach (var bot in bots)
            {
                Rectangle rect = new Rectangle(bot.point, new Size(bot.size, bot.size));
                if (rect.IntersectsWith(tank) && bot != a_tank) return false;
            }

            foreach (var s in currentMap.stone)
            {
                Rectangle rect = new Rectangle(s, new Size(Map.size, Map.size));
                if (rect.IntersectsWith(tank)) return false;
            }

            foreach (var b in currentMap.brick)
            {
                Rectangle rect = new Rectangle(b, new Size(Map.size / 3, Map.size / 3));
                if (rect.IntersectsWith(tank)) return false;
            }

            Rectangle eagle = new Rectangle(currentMap.pointEagle, new Size(Map.size, Map.size));
            if (eagle.IntersectsWith(tank)) return false;

            return true;
        }

        private void DeleteCrossedBullets() // удаление столкнувшихся пуль
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

            foreach (var del in toDelete)
            {
                bullets.Remove(del);
            }
        }

        private bool BulletCanMove(Bullet bullet)
        {
            Rectangle rect;

            rect = Map.mainFrame;
            if (bullet.middle.X <= rect.Left || bullet.middle.X >= rect.Right
                || bullet.middle.Y <= rect.Top || bullet.middle.Y >= rect.Bottom) return false;

            foreach (var s in currentMap.stone)
            {
                rect = new Rectangle(s, new Size(Map.size, Map.size));
                if (rect.Contains(bullet.middle)) return false;
            }

            foreach (var bot in bots) // попадание в бота
            {
                rect = new Rectangle(bot.point, new Size(bot.size, bot.size));
                if ((rect.IntersectsWith(new Rectangle(bullet.point, new Size(bullet.size, bullet.size))) && bullet.tank is PlayerTank))
                {
                    bot.hitpoints--;
                    if (bot.hitpoints == 0) bots.Remove(bot);
                    return false;
                }
            }


            rect = new Rectangle(currentMap.pointEagle, new Size(Map.size, Map.size));
            if ((rect.IntersectsWith(new Rectangle(bullet.point, new Size(bullet.size, bullet.size)))))
            {
                if (!playerWin) gameOver = true;
                currentMap.eagle = new Bitmap(Image.FromFile("../../deadEagle.png"), new Size(Map.size, Map.size));
                return false;
            }

            if (bullet.tank is Bot) // попадание в игрока
            {
                rect = new Rectangle(player.point, new Size(player.size, player.size));

                if (rect.Contains(bullet.middle))
                {
                    player.point = playerStartPosition;
                    player.hitpoints--;

                    if (player.hitpoints == 0 && playerWin == false) gameOver = true;
                    return false;
                }
            }

            List<Point> brickToDelete = new List<Point>();
            bool result = true;
            // для расчистки прохода под размер танка
            rect = new Rectangle(new Point(bullet.middle.X - 10, bullet.middle.Y - 10), new Size(21, 21));
            foreach (var b in currentMap.brick)
            {
                Rectangle brick = new Rectangle(b, new Size(Map.size / 3, Map.size / 3));
                if (brick.IntersectsWith(rect))
                {
                    brickToDelete.Add(b);
                    result = false;
                }
            }

            foreach (var b in brickToDelete) { this.currentMap.brick.Remove(b); }

            rect = new Rectangle(currentMap.pointEagle, new Size(Map.size, Map.size));
            if (rect.Contains(bullet.middle)) return false;

            return result;
        }

        private void SetDifficulty(GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    simultaneousBotAmount = 4;
                    maxBotAmount = 12;
                    break;
                case GameDifficulty.Medium:
                    simultaneousBotAmount = 5;
                    maxBotAmount = 20;
                    break;
                case GameDifficulty.Hard:
                    simultaneousBotAmount = 7;
                    maxBotAmount = 28;
                    break;
            }
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
