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

        public MainForm()
        {
            InitializeComponent();
            game = new GameModel();
            updateViewTimer.Interval = 30; // fps
            updateViewTimer.Tick += UpdateViewTimer_Tick;
            updateViewTimer.Start();
        }

        private void UpdateViewTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            Update();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            e.Graphics.DrawImage(game.player.img, new Point(game.player.tank.X, game.player.tank.Y));
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                game.player.Move(Direction.West);
            }

            else if (e.KeyCode == Keys.Right)
            {
                game.player.Move(Direction.East);
            }

            else if (e.KeyCode == Keys.Up)
            {
                game.player.Move(Direction.North);
            }

            else if (e.KeyCode == Keys.Down)
            {
                game.player.Move(Direction.South);
            }
        }
    }
}
