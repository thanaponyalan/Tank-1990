using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldOfTanks
{
    public abstract class Map
    {
        public int size = 60; // размер текстур
        static public Rectangle mainFrame; // основная рамка игрового поля
        public Point startPosition; // стартовая позиция танка пользователя
        public List<Point> startPositionForBots; // стартовые позиции для ботов
        public Bitmap eagle = new Bitmap(Image.FromFile("../../eagle.png"), new Size(60, 60));
        public Point pointEagle;
        public List<Point> forest = new List<Point>(); // зеленый квадрат
        public List<Point> brick = new List<Point>(); // пробиваемая текстура
        public List<Point> stone = new List<Point>(); // непробиваемая текстура
        public Bitmap imgStone = new Bitmap(Image.FromFile("../../stone.png"), new Size(60, 60));
        public Bitmap imgBrick = new Bitmap(Image.FromFile("../../brick.jpg"), new Size(30, 30));

        public Map(Tank player)
        { 
            int x = (SystemInformation.VirtualScreen.Width - 13 * size) / 2;
            int y = (SystemInformation.VirtualScreen.Height - 13 * size) / 2;
            mainFrame = new Rectangle(new Point(x, y), new Size(size * 13, size * 13));
            startPosition = new Point(mainFrame.X + 4 * size, mainFrame.Y + 12 * size);
            player.point = startPosition;
            pointEagle = new Point(mainFrame.X + 6 * size, mainFrame.Y + 12 * size);
        }
    }
}
