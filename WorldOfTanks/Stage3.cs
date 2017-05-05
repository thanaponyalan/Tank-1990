using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    class Stage3 : Map
    {

        public Stage3()
            : base()
        {
            int x = mainFrame.X;
            int y = mainFrame.Y;
            startPositionForBots = new List<Point>();
            startPositionForBots.Add(new Point(x, y));
            startPositionForBots.Add(new Point(x + size * 3, y));
            startPositionForBots.Add(new Point(x + size * 7, y));
            startPositionForBots.Add(new Point(x + size * 12, y));
            startPositionForBots.Add(new Point(x + size * 5, y + 3 * size));
            startPositionForBots.Add(new Point(x + size * 8, y + 3 * size));
            startPositionForBots.Add(new Point(x + size * 5, y + size * 5));
            startPositionForBots.Add(new Point(x + size * 8, y + size * 5));
            InitializeStone(x, y);
            InitializeBrick(x, y);
            InitializeForest(x, y);
        }

        private void InitializeForest(int x, int y)
        {
            forest.Add(new Point(x + 5 * size, y + 2 * size));
            forest.Add(new Point(x + 6 * size, y + 2 * size));
            forest.Add(new Point(x + 7 * size, y + 2 * size));
            forest.Add(new Point(x + 8 * size, y + 2 * size));

            forest.Add(new Point(x + 5 * size, y + 3 * size));
            forest.Add(new Point(x + 6 * size, y + 3 * size));
            forest.Add(new Point(x + 7 * size, y + 3 * size));
            forest.Add(new Point(x + 8 * size, y + 3 * size));

            forest.Add(new Point(x + 5 * size, y + 4 * size));
            forest.Add(new Point(x + 8 * size, y + 4 * size));

            forest.Add(new Point(x + size, y + 9 * size));

            forest.Add(new Point(x + 2 * size, y + 10 * size));
            forest.Add(new Point(x + 3 * size, y + 10 * size));
            forest.Add(new Point(x + 4 * size, y + 10 * size));
            forest.Add(new Point(x + 5 * size, y + 10 * size));
            forest.Add(new Point(x + 6 * size, y + 10 * size));
            forest.Add(new Point(x + 7 * size, y + 10 * size));
            forest.Add(new Point(x + 8 * size, y + 10 * size));
            forest.Add(new Point(x + 9 * size, y + 10 * size));
            forest.Add(new Point(x + 10 * size, y + 10 * size));
            forest.Add(new Point(x + 11 * size, y + 10 * size));

            forest.Add(new Point(x + 2 * size, y + 11 * size));
            forest.Add(new Point(x + 3 * size, y + 11 * size));
            forest.Add(new Point(x + 4 * size, y + 11 * size));

            forest.Add(new Point(x + 8 * size, y + 11 * size));
            forest.Add(new Point(x + 9 * size, y + 11 * size));
            forest.Add(new Point(x + 10 * size, y + 11 * size));
            forest.Add(new Point(x + 11 * size, y + 11 * size));
        }

        private void InitializeBrick(int x, int y)
        {
            AddSquareOfBricks(new Point(x + size, y + size));
            AddSquareOfBricks(new Point(x + 3 * size, y + size));
            AddSquareOfBricks(new Point(x + 4 * size, y + size));

            AddSquareOfBricks(new Point(x + 9 * size, y + size));
            AddSquareOfBricks(new Point(x + 10 * size, y + size));
            AddSquareOfBricks(new Point(x + 11 * size, y + size));

            AddSquareOfBricks(new Point(x + size, y + 2 * size));
            AddSquareOfBricks(new Point(x + 2 * size, y + 2 * size));
            AddSquareOfBricks(new Point(x + 4 * size, y + 2 * size));
            AddSquareOfBricks(new Point(x + 9 * size, y + 2 * size));
            AddSquareOfBricks(new Point(x + 12 * size, y + 2 * size));

            AddSquareOfBricks(new Point(x, y + 3 * size));
            AddSquareOfBricks(new Point(x + size, y + 3 * size));
            AddSquareOfBricks(new Point(x + 2 * size, y + 3 * size));
            AddSquareOfBricks(new Point(x + 3 * size, y + 3 * size));
            AddSquareOfBricks(new Point(x + 4 * size, y + 3 * size));

            AddSquareOfBricks(new Point(x + 9 * size, y + 3 * size));
            AddSquareOfBricks(new Point(x + 11 * size, y + 3 * size));
            AddSquareOfBricks(new Point(x + 10 * size, y + 3 * size));
            AddSquareOfBricks(new Point(x + 12 * size, y + 3 * size));


            AddSquareOfBricks(new Point(x, y + 4 * size));
            AddSquareOfBricks(new Point(x + size, y + 4 * size));
            AddSquareOfBricks(new Point(x + 3 * size, y + 4 * size));
            AddSquareOfBricks(new Point(x + 4 * size, y + 4 * size));

            AddSquareOfBricks(new Point(x + 9 * size, y + 4 * size));
            AddSquareOfBricks(new Point(x + 11 * size, y + 4 * size));
            AddSquareOfBricks(new Point(x + 10 * size, y + 4 * size));
            AddSquareOfBricks(new Point(x + 12 * size, y + 4 * size));

            AddSquareOfBricks(new Point(x, y + 5 * size));
            AddSquareOfBricks(new Point(x + size, y + 5 * size));
            AddSquareOfBricks(new Point(x + 3 * size, y + 5 * size));
            AddSquareOfBricks(new Point(x + 9 * size, y + 5 * size));
            AddSquareOfBricks(new Point(x + 12 * size, y + 5 * size));

            AddSquareOfBricks(new Point(x, y + 6 * size));
            AddSquareOfBricks(new Point(x + size, y + 6 * size));
            AddSquareOfBricks(new Point(x + 2 * size, y + 6 * size));
            AddSquareOfBricks(new Point(x + 3 * size, y + 6 * size));
            AddSquareOfBricks(new Point(x + 6 * size, y + 6 * size));
            AddSquareOfBricks(new Point(x + 9 * size, y + 6 * size));
            AddSquareOfBricks(new Point(x + 10 * size, y + 6 * size));
            AddSquareOfBricks(new Point(x + 12 * size, y + 6 * size));

            AddSquareOfBricks(new Point(x + 6 * size, y + 7 * size));

            AddSquareOfBricks(new Point(x + 2 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 3 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 4 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 5 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 6 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 7 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 8 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 9 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 10 * size, y + 8 * size));
            AddSquareOfBricks(new Point(x + 11 * size, y + 8 * size));

            AddSquareOfBricks(new Point(x, y + 9 * size));
            AddSquareOfBricks(new Point(x + 2 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 3 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 4 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 7 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 8 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 9 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 10 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 12 * size, y + 9 * size));
            AddSquareOfBricks(new Point(x + 12 * size, y + 10 * size));

            AddSquareOfBricks(new Point(x + 3 * size, y + 12 * size));
            AddSquareOfBricks(new Point(x + 5 * size, y + 12 * size));
            AddSquareOfBricks(new Point(x + 7 * size, y + 12 * size));
            AddSquareOfBricks(new Point(x + 10 * size, y + 12 * size));

            AddSquareOfBricks(new Point(x + 5 * size, y + 11 * size));
            AddSquareOfBricks(new Point(x + 6 * size, y + 11 * size));
            AddSquareOfBricks(new Point(x + 7 * size, y + 11 * size));
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
            stone.Add(new Point(x, y + size));
            stone.Add(new Point(x + 12 * size, y + size));
            stone.Add(new Point(x + 6 * size, y + 4 * size));
            stone.Add(new Point(x + 7 * size, y + 4 * size));
            stone.Add(new Point(x + 5 * size, y + 6 * size));
            stone.Add(new Point(x + 8 * size, y + 6 * size));
            stone.Add(new Point(x + 7 * size, y + 6 * size));
            stone.Add(new Point(x + 11 * size, y + 6 * size));
            stone.Add(new Point(x + 4 * size, y + 7 * size));
            stone.Add(new Point(x + 8 * size, y + 7 * size));
            stone.Add(new Point(x + 5 * size, y + 9 * size));
            stone.Add(new Point(x + 6 * size, y + 9 * size));
            stone.Add(new Point(x + 11 * size, y + 9 * size));
            stone.Add(new Point(x + size, y + 10 * size));
            stone.Add(new Point(x + 2 * size, y + 12 * size));
        }
    }
}
