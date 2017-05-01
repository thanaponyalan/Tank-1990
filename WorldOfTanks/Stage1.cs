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
            :base(player)
        {
            int size = player.size;
            int x = mainFrame.X;
            int y = mainFrame.Y;
            InitializeStone(x,y,size);
            InitializeBrick(x, y, size);
            InitializeForest(x, y, size);
        }

        private void InitializeForest(int x, int y, int size)
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

        private void InitializeBrick(int x, int y, int size)
        {
            brick.Add(new Point(x + size * 3, y + size));
            brick.Add(new Point(x + size * 5, y + size));
            brick.Add(new Point(x + size * 4, y + size));
            brick.Add(new Point(x + size * 3, y + size * 2));
            brick.Add(new Point(x + size * 5, y + size * 2));
            brick.Add(new Point(x + size * 3, y + size * 3));
            brick.Add(new Point(x + size * 5, y + size * 3));
            brick.Add(new Point(x + size * 4, y + size * 3));
            brick.Add(new Point(x + size * 3, y + size * 5));
            brick.Add(new Point(x + size * 5, y + size * 5));
            brick.Add(new Point(x + size * 4, y + size * 5));
            brick.Add(new Point(x + size * 5, y + size * 4));
            brick.Add(new Point(x + size * 7, y + size));
            brick.Add(new Point(x + size * 8, y + size));
            brick.Add(new Point(x + size * 9, y + size));
            brick.Add(new Point(x + size * 7, y + size * 2));
            brick.Add(new Point(x + size * 9, y + size * 2));
            brick.Add(new Point(x + size * 7, y + size * 3));
            brick.Add(new Point(x + size * 9, y + size * 3));
            brick.Add(new Point(x + size * 7, y + size * 4));
            brick.Add(new Point(x + size * 9, y + size * 4));
            brick.Add(new Point(x + size * 7, y + size * 5));
            brick.Add(new Point(x + size * 8, y + size * 5));
            brick.Add(new Point(x + size * 9, y + size * 5));
            brick.Add(new Point(x + size, y + size * 8));
            brick.Add(new Point(x + size, y + size * 9));
            brick.Add(new Point(x + size, y + size * 10));
            brick.Add(new Point(x + size, y + size * 11));
            brick.Add(new Point(x + size * 3, y + size * 8));
            brick.Add(new Point(x + size * 3, y + size * 9));
            brick.Add(new Point(x + size * 3, y + size * 10));
            brick.Add(new Point(x + size * 3, y + size * 11));
            brick.Add(new Point(x + size * 9, y + size * 8));
            brick.Add(new Point(x + size * 9, y + size * 9));
            brick.Add(new Point(x + size * 9, y + size * 10));
            brick.Add(new Point(x + size * 9, y + size * 11));
            brick.Add(new Point(x + size * 11, y + size * 8));
            brick.Add(new Point(x + size * 11, y + size * 9));
            brick.Add(new Point(x + size * 11, y + size * 10));
            brick.Add(new Point(x + size * 11, y + size * 11));
            brick.Add(new Point(x + size * 5, y + size * 12));
            brick.Add(new Point(x + size * 5, y + size * 11));
            brick.Add(new Point(x + size * 6, y + size * 11));
            brick.Add(new Point(x + size * 7, y + size * 11));
            brick.Add(new Point(x + size * 7, y + size * 12));
            brick.Add(new Point(x + size * 7, y + size * 7));
            brick.Add(new Point(x + size * 5, y + size * 7));
            brick.Add(new Point(x + size * 7, y + size * 9));
            brick.Add(new Point(x + size * 6, y + size * 9));
            brick.Add(new Point(x + size * 5, y + size * 9));
        }

        private void InitializeStone(int x, int y, int size)
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
