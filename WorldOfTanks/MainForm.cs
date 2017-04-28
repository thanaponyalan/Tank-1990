using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldOfTanks
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public partial class MainForm : Form
    {
        private GameModel game;
        private Timer updateViewTimer = new Timer();
        private Timer bulletsTimer = new Timer();

        public MainForm()
        {
            InitializeComponent();
            game = new GameModel();
            updateViewTimer.Interval = 30; // fps
            updateViewTimer.Tick += UpdateViewTimer_Tick;
            updateViewTimer.Start();
            bulletsTimer.Interval = 1; // fps
            bulletsTimer.Tick += BulletsTimer_Tick; ;
            bulletsTimer.Start();
        }

        private void BulletsTimer_Tick(object sender, EventArgs e)
        {
            game.MoveBullet();
        }

        private void UpdateViewTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            Update();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            e.Graphics.FillRectangle(new SolidBrush(Color.Wheat), new RectangleF(new Point(100, 100), new Size(900, 900)));
            //e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(game.player.point.X, game.player.point.Y), new Size(40, 40)));
            e.Graphics.DrawImage(game.player.img, new Point(game.player.point.X, game.player.point.Y));

            foreach(var b in game.bullets)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.DarkOrange), new RectangleF(b.point, new Size(b.size, b.size)));
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                game.Move(game.player, Direction.West);
            }

            else if (e.KeyCode == Keys.Right)
            {
                game.Move(game.player, Direction.East);
            }

            else if (e.KeyCode == Keys.Up)
            {
                game.Move(game.player, Direction.North);
            }

            else if (e.KeyCode == Keys.Down)
            {
                game.Move(game.player, Direction.South);
            }

            else if (e.KeyCode == Keys.Z || e.KeyCode == Keys.X)
            {
                game.Shoot(game.player);
            }
        }
    }
}
