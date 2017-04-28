using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    public abstract class Tank
    {
        protected Direction direction; // направление движения танка
        protected Dictionary<Direction, KeyValuePair<int, int>> shift; // смещение танка в зависимости от направления движения
        protected int hitpoints; // кол-во жизней
        public Rectangle tank; // координаты левого верхнего угла и длина/ширина танка
        public Bitmap img; // картинка для танка
        public Dictionary<Direction, int> windRose = new Dictionary<Direction, int>(); // роза ветров

        public Tank()
        {
            shift = new Dictionary<Direction, KeyValuePair<int, int>>();
            shift.Add(Direction.North, new KeyValuePair<int, int>(0, -10));
            shift.Add(Direction.South, new KeyValuePair<int, int>(0, 10));
            shift.Add(Direction.East, new KeyValuePair<int, int>(10, 0));
            shift.Add(Direction.West, new KeyValuePair<int, int>(-10, 0));
            windRose.Add(Direction.North, 0);
            windRose.Add(Direction.East, 90);
            windRose.Add(Direction.West, 270);
            windRose.Add(Direction.South, 180);
        }

        public abstract void Shoot(); // выстрел 
        public abstract void Move(Direction dir); // перемещение танка
    }
}
