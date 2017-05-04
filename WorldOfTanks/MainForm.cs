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
        private bool left = false;
        private bool right = false;
        private bool up = false;
        private bool down = false;
        private bool shoot = false;
        private GameModel game;
        private Timer updateViewTimer = new Timer();
        private Timer bulletsTimer = new Timer();
        private Timer shootTimer = new Timer();
        private Timer shootTimerForBots = new Timer();
        private Timer moveTimerForEasyBots = new Timer();
        private Timer moveTimerForMediumBots = new Timer();
        private Timer moveTimerForHardBots = new Timer();
        private Timer addBotTimer= new Timer();


        public MainForm()
        {
            InitializeComponent();
            game = new GameModel();
            updateViewTimer.Interval = 30; // fps
            updateViewTimer.Tick += UpdateViewTimer_Tick;
            updateViewTimer.Start();
            bulletsTimer.Interval = 1; // fps
            bulletsTimer.Tick += BulletsTimer_Tick;
            bulletsTimer.Start();
            shootTimer.Interval = 150;
            shootTimer.Tick += ShootTimer_Tick;
            shootTimer.Start();
            shootTimerForBots.Interval = 500;
            shootTimerForBots.Tick += ShootTimerForBots_Tick;
            shootTimerForBots.Start();
            moveTimerForEasyBots.Interval = 30;
            moveTimerForEasyBots.Tick += MoveTimerForEasyBots_Tick;
            moveTimerForEasyBots.Start();
            moveTimerForMediumBots.Interval = 50;
            moveTimerForMediumBots.Tick += MoveTimerForMediumBots_Tick;
            moveTimerForMediumBots.Start();
            moveTimerForHardBots.Interval = 70;
            moveTimerForHardBots.Tick += MoveTimerForHardBots_Tick;
            moveTimerForHardBots.Start();
            addBotTimer.Interval = 2000;
            addBotTimer.Tick += AddBotTimer_Tick;
            addBotTimer.Start();
        }

        private void AddBotTimer_Tick(object sender, EventArgs e)
        {
            game.AddBot();           
        }

        private void MoveTimerForHardBots_Tick(object sender, EventArgs e)
        {
            game.MoveBots(BotDifficulty.Hard);
        }

        private void MoveTimerForMediumBots_Tick(object sender, EventArgs e)
        {
            game.MoveBots(BotDifficulty.Medium);
        }

        private void MoveTimerForEasyBots_Tick(object sender, EventArgs e)
        {
            game.MoveBots(BotDifficulty.Easy);
        }

        private void ShootTimerForBots_Tick(object sender, EventArgs e)
        {
            foreach (var bot in game.bots)
            {
                game.Shoot(bot);
            }
        }

        private void ShootTimer_Tick(object sender, EventArgs e)
        {
            if (shoot) game.Shoot(game.player);
        }

        private void BulletsTimer_Tick(object sender, EventArgs e)
        {
            game.MoveBullet();
        }

        private void UpdateViewTimer_Tick(object sender, EventArgs e)
        {

            if (left) game.Move(game.player, Direction.West);
            else if (right) game.Move(game.player, Direction.East);
            else if (up) game.Move(game.player, Direction.North);
            else if (down) game.Move(game.player, Direction.South);

            Invalidate();
            Update();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (game.gameOver || game.playerWin) Close();

            e.Graphics.Clear(Color.Gray);
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), game.currentMap.mainFrame); // отрисовка основной рамки
            e.Graphics.DrawImage(game.player.img, new Point(game.player.point.X, game.player.point.Y)); // отрисовка танка

            foreach (var t in game.bots)
            {
                e.Graphics.DrawImage(t.img, new Point(t.point.X, t.point.Y)); // отрисовка ботов
            }

            e.Graphics.DrawImage(game.currentMap.eagle, game.currentMap.pointEagle); // отрисовка орла

            foreach (var s in game.currentMap.stone)
            {
                e.Graphics.DrawImage(game.currentMap.imgStone, s);
            }

            foreach (var f in game.currentMap.forest)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(110, 0, 255, 111)), new Rectangle(f, new Size(60, 60)));
            }

            foreach (var b in game.currentMap.brick)
            {
                e.Graphics.DrawImage(game.currentMap.imgBrick, b);
            }

            foreach (var b in game.bullets) // отрисовка пуль
            {
                if(b.tank is PlayerTank)e.Graphics.FillEllipse(new SolidBrush(Color.Aquamarine), new RectangleF(b.point, new Size(b.size, b.size)));
                else e.Graphics.FillEllipse(new SolidBrush(Color.Orange), new RectangleF(b.point, new Size(b.size, b.size)));
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { StopMove(); left = true; }
            if (e.KeyCode == Keys.Right) { StopMove(); right = true; }
            if (e.KeyCode == Keys.Up) { StopMove(); up = true; }
            if (e.KeyCode == Keys.Down) { StopMove(); down = true; }
            if (e.KeyCode == Keys.Z || e.KeyCode == Keys.X) shoot = true;
        }

        private void StopMove()
        {
            left = false;
            right = false;
            up = false;
            down = false;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) left = false;
            else if (e.KeyCode == Keys.Right) right = false;
            else if (e.KeyCode == Keys.Up) up = false;
            else if (e.KeyCode == Keys.Down) down = false;
            if (e.KeyCode == Keys.Z || e.KeyCode == Keys.X) shoot = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Text = "X: " + e.X + "Y: " + e.Y;
        }
    }
}
