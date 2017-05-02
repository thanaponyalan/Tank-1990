using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    public class Stage1 : Map
    {


        public Stage1(Tank player)
            : base(player)
        {
            int x = mainFrame.X;
            int y = mainFrame.Y;
            InitializeStone(x, y);
            InitializeBrick(x, y);
            InitializeForest(x, y);
        }

        private void InitializeForest(int x, int y)
        {
            forest.Add(new Point(x, y + size * 12));
            forest.Add(new Point(x + size, y + size * 12));
            forest.Add(new Point(x + size * 2, y + size * 12));
            forest.Add(new Point(x, y + size * 11));
            forest.Add(new Point(x, y + size * 10));
            forest.Add(new Point(x, y + size * 9));
            forest.Add(new Point(x, y + size * 8));
            forest.Add(new Point(x + size * 12, y + size * 12));
            forest.Add(new Point(x + size * 11, y + size * 12));
            forest.Add(new Point(x + size * 10, y + size * 12));
            forest.Add(new Point(x + size * 12, y + size * 11));
            forest.Add(new Point(x + size * 12, y + size * 10));
            forest.Add(new Point(x + size * 12, y + size * 9));
            forest.Add(new Point(x + size * 12, y + size * 8));
        }

        private void InitializeBrick(int x, int y)
        {
            AddSquareOfBricks(new Point(x + size * 3, y + size));
            AddSquareOfBricks(new Point(x + size * 5, y + size));
            AddSquareOfBricks(new Point(x + size * 4, y + size));
            AddSquareOfBricks(new Point(x + size * 3, y + size * 2));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 2));
            AddSquareOfBricks(new Point(x + size * 3, y + size * 3));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 3));
            AddSquareOfBricks(new Point(x + size * 4, y + size * 3));
            AddSquareOfBricks(new Point(x + size * 3, y + size * 5));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 5));
            AddSquareOfBricks(new Point(x + size * 4, y + size * 5));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 4));
            AddSquareOfBricks(new Point(x + size * 7, y + size));
            AddSquareOfBricks(new Point(x + size * 8, y + size));
            AddSquareOfBricks(new Point(x + size * 9, y + size));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 2));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 2));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 3));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 3));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 4));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 4));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 5));
            AddSquareOfBricks(new Point(x + size * 8, y + size * 5));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 5));
            AddSquareOfBricks(new Point(x + size, y + size * 8));
            AddSquareOfBricks(new Point(x + size, y + size * 9));
            AddSquareOfBricks(new Point(x + size, y + size * 10));
            AddSquareOfBricks(new Point(x + size, y + size * 11));
            AddSquareOfBricks(new Point(x + size * 3, y + size * 8));
            AddSquareOfBricks(new Point(x + size * 3, y + size * 9));
            AddSquareOfBricks(new Point(x + size * 3, y + size * 10));
            AddSquareOfBricks(new Point(x + size * 3, y + size * 11));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 8));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 9));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 10));
            AddSquareOfBricks(new Point(x + size * 9, y + size * 11));
            AddSquareOfBricks(new Point(x + size * 11, y + size * 8));
            AddSquareOfBricks(new Point(x + size * 11, y + size * 9));
            AddSquareOfBricks(new Point(x + size * 11, y + size * 10));
            AddSquareOfBricks(new Point(x + size * 11, y + size * 11));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 12));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 11));
            AddSquareOfBricks(new Point(x + size * 6, y + size * 11));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 11));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 12));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 7));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 7));
            AddSquareOfBricks(new Point(x + size * 7, y + size * 9));
            AddSquareOfBricks(new Point(x + size * 6, y + size * 9));
            AddSquareOfBricks(new Point(x + size * 5, y + size * 9));
        }

        private void AddSquareOfBricks(Point point)
        {
            brick.Add(point);
            brick.Add(new Point(point.X + size / 2, point.Y));
            brick.Add(new Point(point.X, point.Y + size / 2));
            brick.Add(new Point(point.X + size / 2, point.Y + size / 2));
        }

        private void InitializeStone(int x, int y)
        {
            stone.Add(new Point(x, y + size * 7));
            stone.Add(new Point(x + 12 * size, y + size * 7));
            stone.Add(new Point(x + 6 * size, y + size * 7));
            stone.Add(new Point(x + 2 * size, y + size * 9));
            stone.Add(new Point(x + 4 * size, y + size * 9));
            stone.Add(new Point(x + 8 * size, y + size * 9));
            stone.Add(new Point(x + 10 * size, y + size * 9));
        }
    }
}
