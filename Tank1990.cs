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
        public Tank1990()
        {
            InitializeComponent();
            playerTank = new PlayerTank();
            graphics = this.CreateGraphics();
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
            buffer.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.White)), new Rectangle(playerTank.TopLeftCorner, new Size(playerTank.Width, playerTank.Length)));
            buffer.Graphics.DrawRectangle(pen, new Rectangle(playerTank.TopLeftCorner, new Size(playerTank.Width, playerTank.Length)));


            // Renders the contents of the buffer to the specified drawing surface.
            buffer.Render(graphics);
            buffer.Dispose();
        }
    }
}
