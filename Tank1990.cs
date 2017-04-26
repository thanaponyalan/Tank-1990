using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank1990
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public partial class Tank1990 : Form
    {

        public List<Bullet> bullets; // снаряды
        private Graphics graphics; // холст для фигур
        private PlayerTank playerTank; // танк пользователя
        private IMap currentMap; // текущая карта
        private Dictionary<Keys, Direction> direction;

        public Tank1990()
        {
            InitializeComponent();
            playerTank = new PlayerTank();
            playerTank.UpdateView += UpdateView;
            graphics = this.CreateGraphics();
            direction = new Dictionary<Keys, Direction>();
            direction.Add(Keys.Left, Direction.West);
            direction.Add(Keys.Right, Direction.East);
            direction.Add(Keys.Down, Direction.South);
            direction.Add(Keys.Up, Direction.North);

        }

        public void UpdateView(object sender, PaintEventArgs e)
        {
            BufferedGraphicsContext currentContext;
            BufferedGraphics buffer;
            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with Form1, and with 
            // dimensions the same size as the drawing surface of Form1.
            buffer = currentContext.Allocate(this.CreateGraphics(),
               this.DisplayRectangle);
            buffer.Graphics.Clear(SystemColors.Control);

            // рисуем все фигуры


            Pen pen = new Pen(Color.Black);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pen.Width = 5;
            buffer.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, Color.Black)), new Rectangle(playerTank.TopLeftCorner, new Size(playerTank.Width, playerTank.Length)));
            //buffer.Graphics.DrawRectangle(pen, new Rectangle(playerTank.TopLeftCorner, new Size(playerTank.Width, playerTank.Length)));


            // Renders the contents of the buffer to the specified drawing surface.
            buffer.Render(graphics);
            buffer.Dispose();
        }

        // отлавливает нажатую кнопку стрелки
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right ||
                keyData == Keys.Up || keyData == Keys.Down)
            {
                if(playerTank.direction == direction[keyData]) playerTank.Move(currentMap);
                else playerTank.direction = direction[keyData];
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
