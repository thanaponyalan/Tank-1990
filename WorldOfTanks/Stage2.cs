using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    class Stage2 : Map
    {
        public Stage2()
            : base()
        {
            int x = mainFrame.X;
            int y = mainFrame.Y;
            startPositionForBots = new List<Point>();
            startPositionForBots.Add(new Point(x + size / 2, y));
            startPositionForBots.Add(new Point(x + size * 2, y + size));
            startPositionForBots.Add(new Point(x + size * 2, y + size * 5));
            startPositionForBots.Add(new Point(x + size * 4, y + size * 4));
            startPositionForBots.Add(new Point(x + size * 6, y + size));
            startPositionForBots.Add(new Point(x + size * 10, y + size));
            startPositionForBots.Add(new Point(x + size * 12, y + size * 2));
            startPositionForBots.Add(new Point(x + size * 12, y + size * 5));
            InitializeStone(x, y);
            InitializeBrick(x, y);
            InitializeForest(x, y);
        }

        private void InitializeForest(int x, int y)
        {
            forest.Add(new Point(x, y + size));
            forest.Add(new Point(x, y + size * 2));
            forest.Add(new Point(x, y + size * 3));
            forest.Add(new Point(x, y + size * 4));
            forest.Add(new Point(x, y + size * 5));
            forest.Add(new Point(x + size, y + size * 5));
            forest.Add(new Point(x, y + size * 10));
            forest.Add(new Point(x + 4 * size, y + size * 7));
            forest.Add(new Point(x + 5 * size, y + size * 6));
            forest.Add(new Point(x + 6 * size, y + size * 6));
            forest.Add(new Point(x + 10 * size, y + size * 6));
            forest.Add(new Point(x + 10 * size, y + size * 5));
            forest.Add(new Point(x + 10 * size, y + size * 4));
            forest.Add(new Point(x + 3 * size, y + size * 2));
            forest.Add(new Point(x + 4 * size, y + size * 2));
            forest.Add(new Point(x + 5 * size, y + size * 2));

        }

        private void InitializeBrick(int x, int y)
        {
            AddSquareOfBricks(new Point(x + size, y + size));
            AddSquareOfBricks(new Point(x + size, y + 2 * size));
            AddSquareOfBricks(new Point(x + size * 6, y + 2 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + 2 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + size));
            AddSquareOfBricks(new Point(x + size * 9, y + size));
            AddSquareOfBricks(new Point(x + size * 9, y + 2 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 2 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + size));
            AddSquareOfBricks(new Point(x + size * 3, y + 3 * size));
            AddSquareOfBricks(new Point(x + size * 6, y + 3 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + 3 * size));
            AddSquareOfBricks(new Point(x + size * 3, y + 4 * size));
            AddSquareOfBricks(new Point(x + size * 5, y + 4 * size));
            AddSquareOfBricks(new Point(x + size * 9, y + 4 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 4 * size));
            AddSquareOfBricks(new Point(x + size * 3, y + 5 * size));
            AddSquareOfBricks(new Point(x + size * 4, y + 5 * size));
            AddSquareOfBricks(new Point(x + size * 5, y + 5 * size));
            AddSquareOfBricks(new Point(x + size * 9, y + 5 * size));
            AddSquareOfBricks(new Point(x, y + 6 * size));
            AddSquareOfBricks(new Point(x, y + 6 * size));
            AddSquareOfBricks(new Point(x + size, y + 6 * size));
            AddSquareOfBricks(new Point(x + size * 2, y + 6 * size));
            AddSquareOfBricks(new Point(x + size * 3, y + 6 * size));
            AddSquareOfBricks(new Point(x + size * 4, y + 6 * size));
            AddSquareOfBricks(new Point(x + size * 8, y + 6 * size));
            AddSquareOfBricks(new Point(x + size * 9, y + 6 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 6 * size));
            AddSquareOfBricks(new Point(x + size * 5, y + 7 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + 7 * size));
            AddSquareOfBricks(new Point(x + size * 9, y + 7 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 7 * size));
            AddSquareOfBricks(new Point(x + size, y + 8 * size));
            AddSquareOfBricks(new Point(x + size * 5, y + 8 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + 8 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 8 * size));
            AddSquareOfBricks(new Point(x + size, y + 9 * size));
            AddSquareOfBricks(new Point(x + size * 5, y + 9 * size));
            AddSquareOfBricks(new Point(x + size * 6, y + 9 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + 9 * size));
            AddSquareOfBricks(new Point(x + size * 9, y + 9 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 9 * size));
            AddSquareOfBricks(new Point(x + size, y + 10 * size));
            AddSquareOfBricks(new Point(x + size * 3, y + 10 * size));
            AddSquareOfBricks(new Point(x + size, y + 11 * size));
            AddSquareOfBricks(new Point(x + size * 9, y + 11 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 11 * size));
            AddSquareOfBricks(new Point(x + size, y + 12 * size));
            AddSquareOfBricks(new Point(x + size * 3, y + 12 * size));
            AddSquareOfBricks(new Point(x + size * 9, y + 12 * size));
            AddSquareOfBricks(new Point(x + size * 10, y + 12 * size));
            AddSquareOfBricks(new Point(x + size * 11, y + 12 * size));
            AddSquareOfBricks(new Point(x + size * 5, y + 11 * size));
            AddSquareOfBricks(new Point(x + size * 6, y + 11 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + 11 * size));
            AddSquareOfBricks(new Point(x + size * 5, y + 12 * size));
            AddSquareOfBricks(new Point(x + size * 7, y + 12 * size));
        }

        private void AddSquareOfBricks(Point point)
        {
            brick.Add(point);
            brick.Add(new Point(point.X + size / 3, point.Y));
            brick.Add(new Point(point.X + 2 * (size / 3), point.Y));
            brick.Add(new Point(point.X, point.Y + size / 3));
            brick.Add(new Point(point.X + size / 3, point.Y + size / 3));
            brick.Add(new Point(point.X + 2 * (size / 3), point.Y + size / 3));

            brick.Add(new Point(point.X, point.Y + 2 * (size / 3)));
            brick.Add(new Point(point.X + size / 3, point.Y + 2 * (size / 3)));
            brick.Add(new Point(point.X + 2 * (size / 3), point.Y + 2 * (size / 3)));
        }

        private void InitializeStone(int x, int y)
        {
            stone.Add(new Point(x + size * 3, y));
            stone.Add(new Point(x + size * 3, y + size));
            stone.Add(new Point(x + size * 7, y));
            stone.Add(new Point(x + size * 10, y + 2 * size));
            stone.Add(new Point(x + size * 9, y + 3 * size));
            stone.Add(new Point(x + size * 12, y + 4 * size));
            stone.Add(new Point(x + size * 6, y + 4 * size));
            stone.Add(new Point(x + size * 7, y + 6 * size));
            stone.Add(new Point(x + size * 8, y + 5 * size));
            stone.Add(new Point(x, y + 9 * size));
            stone.Add(new Point(x + 3 * size, y + 9 * size));
            stone.Add(new Point(x + 3 * size, y + 8 * size));
            stone.Add(new Point(x + 10 * size, y + 10 * size));
        }
    }
}
