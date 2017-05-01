using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WorldOfTanks
{
    public class Bullet
    {
        public int size = 10; // размер прямоугольника в который будет вписана пуля
        public Direction direction; // направление 
        public int Speed = 8; // скорость в пикселях за одну милисекунду
        public Point point; // левая верхняя точка прямоугольника
        public Point middle; // центр окружности/пули
        public Tank tank; // танк, который выпустил пулю
        public  Dictionary<Direction, KeyValuePair<int, int>> shift;

        public Bullet(Tank tank)
        {
            this.tank = tank;
            direction = tank.direction;

            switch (direction)
            {
                case Direction.North:
                    middle = new Point(tank.point.X + tank.size / 2, tank.point.Y);
                    point = new Point(tank.point.X + tank.size / 2 - size/2, tank.point.Y - size/2);
                    break;
                case Direction.South:
                    middle = new Point(tank.point.X + tank.size / 2, tank.point.Y + tank.size);
                    point = new Point(tank.point.X + tank.size / 2 - size / 2, tank.point.Y + tank.size - size/2);
                    break;
                case Direction.East:
                    middle = new Point(tank.point.X + tank.size, tank.point.Y + tank.size / 2);
                    point = new Point(tank.point.X + tank.size - size/2, tank.point.Y + tank.size/2 - size/2);
                    break;
                case Direction.West:
                    middle = new Point(tank.point.X, tank.point.Y + tank.size/2);
                    point = new Point(tank.point.X - size / 2, tank.point.Y + tank.size / 2 - size / 2);
                    break;
            }

            
            shift = new Dictionary<Direction, KeyValuePair<int, int>>();
            shift.Add(Direction.North, new KeyValuePair<int, int>(0, -Speed));
            shift.Add(Direction.South, new KeyValuePair<int, int>(0, Speed));
            shift.Add(Direction.West, new KeyValuePair<int, int>(-Speed, 0));
            shift.Add(Direction.East, new KeyValuePair<int, int>(Speed, 0));
        }
    }
}
