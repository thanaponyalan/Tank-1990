using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WorldOfTanks
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    public partial class MainForm : Form
    {
        private bool left = false;
        private bool right = false;
        private bool up = false;
        private bool down = false;
        private bool shoot = false;
        private bool playStage = false; // прохождение уровня
        private bool beforeStage = false; // заставка перед картой
        private bool languageMenu = true; // окно меню
        private bool menu = false;
        private bool chooseDifficulty = false; // выбор сложности игры
        private bool gameOver = false;
        private bool mapWinner = false;
        private string lang;
        private string gameOverTxt;
        private string gameWinnerTxt;
        private Dictionary<string, Rectangle> languageMenuItems = new Dictionary<string, Rectangle>();
        private Dictionary<string, Rectangle> menuItems = new Dictionary<string, Rectangle>(); // список пунктов меню и их координаты
        private Dictionary<string, Rectangle> difficultyItems = new Dictionary<string, Rectangle>(); // список пунктов меню и их координаты
        private GameModel game;
        private Timer updateViewTimer = new Timer(); // таймер для отрисовки
        private Timer bulletsTimer = new Timer(); // таймер для перемещения всех пуль
        private Timer shootTimer = new Timer(); // таймер для отлавливания события нажатия на клавишу
        private Timer shootTimerForBots = new Timer(); // таймер для принятия решения стрелять/не стрелять ботам
        private Timer moveTimerForEasyBots = new Timer(); // таймер для перемещения легкого бота
        private Timer moveTimerForMediumBots = new Timer(); // таймер для перемещения среднего бота
        private Timer moveTimerForHardBots = new Timer(); // таймер для перемещения сложного бота
        private Timer beforeStageTimer = new Timer(); // таймер на заставку перед картой
        private Timer addBotTimer = new Timer(); // таймер для добавления ботов на карту
        private Timer winOrLoseAnimationTimer = new Timer(); // таймер для отрисовки строки "Gameover" или "You win"
        private Bitmap menuImage; // заставка
        private Bitmap difficultyImage; // заставка
        private List<Map> maps = new List<Map>(); // список со всеми картами
        private Bitmap bots = new Bitmap(Image.FromFile("../../bots.png"), new Size(20, 20));
        private Bitmap health = new Bitmap(Image.FromFile("../../health.png"), new Size(40, 40));
        private int currentMap = 0;
        private GameDifficulty currentDifficulty = GameDifficulty.Medium; // текущая сложность
        private List<string> stages = new List<string>(); // строки для заставки перед запуском карты
        private string winOrLose; // надпись "Gameover" или "You win"
        private Point winOrLoseCoordinates; // координаты надписи "Gameover" или "You win"
        private Point winOrLoseAnimation;
        private int countMiliseconds = 0;
        SoundPlayer gameSound = new SoundPlayer("../../shortSound.wav");
        private Label gameName = new Label();

        public MainForm()
        {
            InitializeComponent();
            gameSound.PlayLooping();
            menuImage = new Bitmap(Image.FromFile("../../MenuImage.jpg"), new Size(Map.mainFrame.Width, Map.mainFrame.Height));
            difficultyImage = new Bitmap(Image.FromFile("../../Difficulty.jpg"), new Size(Map.mainFrame.Width, Map.mainFrame.Height));
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
            addBotTimer.Interval = 500;
            addBotTimer.Tick += AddBotTimer_Tick;
            addBotTimer.Start();
            beforeStageTimer.Interval = 3000;
            beforeStageTimer.Tick += BeforeStageTimer_Tick;
            winOrLoseAnimationTimer.Interval = 30;
            winOrLoseAnimationTimer.Tick += WinOrLoseAnimationTimer_Tick;
            maps.Add(new Stage1());
            maps.Add(new Stage2());
            maps.Add(new Stage3());
            game = new GameModel(maps[currentMap], currentDifficulty);

            languageMenuItems.Add("English", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, Map.mainFrame.Y + (Map.mainFrame.Height - 160) / 2), new Size(140, 40)));
            languageMenuItems.Add("ไทย", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, languageMenuItems["English"].Bottom + 20), new Size(190, 40)));
            languageMenuItems.Add("русский", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, languageMenuItems["ไทย"].Bottom + 20), new Size(190, 40)));
        
            winOrLoseAnimation = winOrLoseCoordinates = new Point(Map.mainFrame.X + Map.mainFrame.Width / 2 - 100, Map.mainFrame.Y + 5);
            gameName.Text = "TANK-1990";
            gameName.Font = new Font(FontFamily.GenericSansSerif, 36.0F);
            gameName.AutoSize = true;
            int x = ((Screen.PrimaryScreen.WorkingArea.Width - gameName.Size.Width) / 2)-70;
            int y= Screen.PrimaryScreen.WorkingArea.Top +  50;
            gameName.Location=new Point(x,y);
            this.Controls.Add(gameName);
        }

        private void WinOrLoseAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (countMiliseconds < 4000)
            {
                if (winOrLoseAnimation.Y < Map.mainFrame.Y + Map.mainFrame.Height / 2)
                {
                    winOrLoseAnimation = new Point(winOrLoseAnimation.X, winOrLoseAnimation.Y + 5);
                }
                countMiliseconds += 30;
            }

            else
            {
                if (gameOver) { menu = true; }
                playStage = false;

                if (currentMap == maps.Count - 1 || gameOver) { menu = true; currentMap = 0; }
                else if (!gameOver) { currentMap++; beforeStage = true; beforeStageTimer.Start(); }
                winOrLoseAnimationTimer.Stop();
                countMiliseconds = 0;
                winOrLoseAnimation = winOrLoseCoordinates;
                gameOver = mapWinner = game.playerWin = game.playerWin = game.gameOver = false;
                StopMove();
                shoot = false;
            }
        }

        private void BeforeStageTimer_Tick(object sender, EventArgs e)
        {
            beforeStage = false;
            game.ChangeMap(maps[currentMap]);
            playStage = true;
            beforeStageTimer.Stop();
        }

        private void UpdateViewTimer_Tick(object sender, EventArgs e)
        {
            if (playStage)
            {
                if (left) game.Move(game.player, Direction.West);
                else if (right) game.Move(game.player, Direction.East);
                else if (up) game.Move(game.player, Direction.North);
                else if (down) game.Move(game.player, Direction.South);
            }

            Invalidate();
            Update();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Gray);
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), Map.mainFrame); // отрисовка основной рамки

            if (beforeStage) // отрисовка заставки перед запуском карты
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Gray), Map.mainFrame);
                e.Graphics.DrawString(stages[currentMap], new Font(new Font(FontFamily.GenericSansSerif, 24.0F), FontStyle.Bold),
                        new SolidBrush(Color.Black), Map.mainFrame.X + Map.mainFrame.Width / 2 - 50, Map.mainFrame.Y + Map.mainFrame.Height / 2 - 10);
            }

            if (languageMenu) // отрисовка пунктов меню
            {
                e.Graphics.DrawImage(menuImage, new Point(Map.mainFrame.X, Map.mainFrame.Y));

                foreach (var m in languageMenuItems)
                {
                    e.Graphics.DrawString(m.Key, new Font(new Font(FontFamily.GenericSansSerif, 24.0F), FontStyle.Bold),
                        new SolidBrush(Color.DarkOrange), m.Value);
                }
            }

            if (menu) // отрисовка пунктов меню
            {
                moveTimerForMediumBots.Start();
                moveTimerForEasyBots.Start();
                moveTimerForHardBots.Start();
                shootTimerForBots.Start();
                shootTimer.Start();

                e.Graphics.DrawImage(menuImage, new Point(Map.mainFrame.X, Map.mainFrame.Y));

                foreach (var m in menuItems)
                {
                    e.Graphics.DrawString(m.Key, new Font(new Font(FontFamily.GenericSansSerif, 20.0F), FontStyle.Bold),
                        new SolidBrush(Color.DarkOrange), m.Value);
                }
            }

            if (chooseDifficulty) // выбор сложности
            {
                e.Graphics.DrawImage(difficultyImage, new Point(Map.mainFrame.X, Map.mainFrame.Y));
                foreach (var d in difficultyItems)
                {
                    e.Graphics.DrawString(d.Key, new Font(new Font(FontFamily.GenericSansSerif, 24.0F), FontStyle.Bold),
                        new SolidBrush(Color.DarkOrange), d.Value);
                }
            }

            if (playStage) // отрисовка игрового процесса на карте
            {
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
                    e.Graphics.DrawImage(game.currentMap.imgForest, f);
                }

                foreach (var b in game.currentMap.brick)
                {
                    e.Graphics.DrawImage(game.currentMap.imgBrick, b);
                }

                foreach (var b in game.bullets) // отрисовка пуль
                {
                    if (b.tank is PlayerTank) e.Graphics.FillEllipse(new SolidBrush(Color.Aquamarine), new RectangleF(b.point, new Size(b.size, b.size)));
                    else e.Graphics.FillEllipse(new SolidBrush(Color.Orange), new RectangleF(b.point, new Size(b.size, b.size)));
                }

                Point point = new Point(Map.mainFrame.X + Map.mainFrame.Width + 10, Map.mainFrame.Y + 10);

                for (int i = 1; i <= game.maxBotAmount - game.killedBots; ++i)
                {
                    e.Graphics.DrawImage(bots, point);
                    if (i % 2 == 0) point = new Point(point.X - 20, point.Y + 20);
                    else point = new Point(point.X + 20, point.Y);
                }

                point = new Point(Map.mainFrame.X + Map.mainFrame.Width + 20, Map.mainFrame.Y + Map.mainFrame.Height - 100);
                e.Graphics.DrawImage(health, point);
                e.Graphics.DrawString(game.player.hitpoints.ToString(), new Font(new Font(FontFamily.GenericSansSerif, 18.0F), FontStyle.Bold),
                        new SolidBrush(Color.Red), new Point(point.X + 50, point.Y + 5));
            }

            if (gameOver || mapWinner)
            {
                e.Graphics.DrawString(winOrLose, new Font(new Font(FontFamily.GenericSansSerif, 26.0F), FontStyle.Bold),
                        new SolidBrush(Color.DeepPink), winOrLoseAnimation);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (playStage)
            {
                if (e.KeyCode == Keys.Left) { StopMove(); left = true; }
                if (e.KeyCode == Keys.Right) { StopMove(); right = true; }
                if (e.KeyCode == Keys.Up) { StopMove(); up = true; }
                if (e.KeyCode == Keys.Down) { StopMove(); down = true; }
                if (e.KeyCode == Keys.Z || e.KeyCode == Keys.X || e.KeyCode == Keys.Space) shoot = true;
            }
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
            if (playStage)
            {
                if (e.KeyCode == Keys.Left) left = false;
                else if (e.KeyCode == Keys.Right) right = false;
                else if (e.KeyCode == Keys.Up) up = false;
                else if (e.KeyCode == Keys.Down) down = false;
                if (e.KeyCode == Keys.Z || e.KeyCode == Keys.X || e.KeyCode == Keys.Space) shoot = false;
            }
            if (e.KeyCode==Keys.Escape) Application.Exit();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (languageMenu)
            {
                foreach (var l in languageMenuItems)
                {
                    if (l.Value.Contains(new Point(e.X, e.Y)))
                    {
                        Cursor.Current = Cursors.Hand;
                        break;
                    }
                }
            }

            else if (menu)
            {
                foreach (var m in menuItems)
                {
                    if (m.Value.Contains(new Point(e.X, e.Y)))
                    {
                        Cursor.Current = Cursors.Hand;
                        break;
                    }
                }
            }

            else if (chooseDifficulty)
            {
                foreach (var d in difficultyItems)
                {
                    if (d.Value.Contains(new Point(e.X, e.Y)))
                    {
                        Cursor.Current = Cursors.Hand;
                        break;
                    }
                }
            }
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (languageMenu)
            {
                if (languageMenuItems["English"].Contains(new Point(e.X,e.Y)))
                {
                    lang = "en";
                    gameOverTxt = "You Lose!";
                    gameWinnerTxt = "You Win.";
                    //Start
                    menuItems.Add("Start", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, Map.mainFrame.Y + (Map.mainFrame.Height - 160) / 2), new Size(140, 40)));

                    //Difficult
                    menuItems.Add("Difficult", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, menuItems["Start"].Bottom + 20), new Size(190, 40)));

                    //Exit
                    menuItems.Add("Exit", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, menuItems["Difficult"].Bottom + 20), new Size(140, 40)));

                    //Easy
                    difficultyItems.Add("Easy", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, Map.mainFrame.Y + (Map.mainFrame.Height - 160) / 2), new Size(140, 40)));

                    //Normal
                    difficultyItems.Add("Normal", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, difficultyItems["Easy"].Bottom + 20), new Size(140, 40)));

                    //Hard
                    difficultyItems.Add("Hard", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, difficultyItems["Normal"].Bottom + 20), new Size(140, 40)));

                    //Stage 1
                    stages.Add("Stage 1");

                    //Stage 2
                    stages.Add("Stage 2");

                    //Stage 3
                    stages.Add("Stage 3");

                    languageMenu = false;
                    menu = true;
                }
                else if (languageMenuItems["ไทย"].Contains(new Point(e.X, e.Y)))
                {
                    gameOverTxt = "คุณแพ้";
                    gameWinnerTxt = "คุณชนะ";
                    lang = "th";
                    //Start
                    menuItems.Add("เริ่มต้น", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, Map.mainFrame.Y + (Map.mainFrame.Height - 160) / 2), new Size(140, 40)));

                    //Difficult
                    menuItems.Add("ระดับความยาก", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, menuItems["เริ่มต้น"].Bottom + 20), new Size(190, 40)));

                    //Exit
                    menuItems.Add("ออก", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, menuItems["ระดับความยาก"].Bottom + 20), new Size(140, 40)));

                    //Easy
                    difficultyItems.Add("ง่าย", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, Map.mainFrame.Y + (Map.mainFrame.Height - 160) / 2), new Size(140, 40)));

                    //Normal
                    difficultyItems.Add("ปานกลาง", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, difficultyItems["ง่าย"].Bottom + 20), new Size(140, 40)));

                    //Hard
                    difficultyItems.Add("ยาก", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, difficultyItems["ปานกลาง"].Bottom + 20), new Size(140, 40)));

                    //Stage 1
                    stages.Add("ด่าน 1");

                    //Stage 2
                    stages.Add("ด่าน 2");

                    //Stage 3
                    stages.Add("ด่าน 3");

                    languageMenu = false;
                    menu = true;
                }
                else if (languageMenuItems["русский"].Contains(new Point(e.X, e.Y)))
                {
                    gameOverTxt = "Вы проиграли!";
                    gameWinnerTxt = "С победой!";
                    lang = "rs";
                    //Start
                    menuItems.Add("Старт", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, Map.mainFrame.Y + (Map.mainFrame.Height - 160) / 2), new Size(140, 40)));

                    //Difficult
                    menuItems.Add("Сложность", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, menuItems["Старт"].Bottom + 20), new Size(190, 40)));

                    //Exit
                    menuItems.Add("Выход", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, menuItems["Сложность"].Bottom + 20), new Size(140, 40)));

                    //Easy
                    difficultyItems.Add("Легко", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, Map.mainFrame.Y + (Map.mainFrame.Height - 160) / 2), new Size(140, 40)));

                    //Normal
                    difficultyItems.Add("Средне", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, difficultyItems["Легко"].Bottom + 20), new Size(140, 40)));

                    //Hard
                    difficultyItems.Add("Сложно", new Rectangle(new Point(Map.mainFrame.X + (Map.mainFrame.Width - 140) / 2, difficultyItems["Средне"].Bottom + 20), new Size(140, 40)));

                    //Stage 1
                    stages.Add("Карта 1");

                    //Stage 2
                    stages.Add("Карта 2");

                    //Stage 3
                    stages.Add("Карта 3");

                    languageMenu = false;
                    menu = true;
                }
            }

            else if (menu)
            {
                if (lang=="rs") {
                    if (menuItems["Старт"].Contains(new Point(e.X, e.Y)))
                    {
                        menu = false;
                        beforeStage = true;
                        beforeStageTimer.Start();
                        game = new GameModel(maps[currentMap], currentDifficulty);
                    }
                    else if (menuItems["Сложность"].Contains(new Point(e.X, e.Y)))
                    {
                        menu = false;
                        chooseDifficulty = true;
                    }
                    else if (menuItems["Выход"].Contains(new Point(e.X, e.Y))) { Close(); }
                }

                else if (lang=="en")
                {
                    if (menuItems["Start"].Contains(new Point(e.X, e.Y)))
                    {
                        menu = false;
                        beforeStage = true;
                        beforeStageTimer.Start();
                        game = new GameModel(maps[currentMap], currentDifficulty);
                    }
                    else if (menuItems["Difficult"].Contains(new Point(e.X, e.Y)))
                    {
                        menu = false;
                        chooseDifficulty = true;
                    }
                    else if (menuItems["Exit"].Contains(new Point(e.X, e.Y))) { Close(); }
                }

                else if (lang=="th")
                {
                    if (menuItems["เริ่มต้น"].Contains(new Point(e.X, e.Y)))
                    {
                        menu = false;
                        beforeStage = true;
                        beforeStageTimer.Start();
                        game = new GameModel(maps[currentMap], currentDifficulty);
                    }
                    else if (menuItems["ระดับความยาก"].Contains(new Point(e.X, e.Y)))
                    {
                        menu = false;
                        chooseDifficulty = true;
                    }
                    else if (menuItems["ออก"].Contains(new Point(e.X, e.Y))) { Close(); }
                }
            }

            else if (chooseDifficulty)
            {
                if (lang=="rs") {
                    if (difficultyItems["Легко"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Easy;
                        SetTimersForEasyGame();
                        menu = true;
                        chooseDifficulty = false;
                    }

                    else if (difficultyItems["Средне"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Medium;
                        SetTimersForMediumGame();
                        menu = true;
                        chooseDifficulty = false;
                    }

                    else if (difficultyItems["Сложно"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Hard;
                        SetTimersForHardGame();
                        menu = true;
                        chooseDifficulty = false;
                    }
                }

                else if (lang=="en")
                {
                    if (difficultyItems["Easy"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Easy;
                        SetTimersForEasyGame();
                        menu = true;
                        chooseDifficulty = false;
                    }

                    else if (difficultyItems["Normal"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Medium;
                        SetTimersForMediumGame();
                        menu = true;
                        chooseDifficulty = false;
                    }

                    else if (difficultyItems["Hard"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Hard;
                        SetTimersForHardGame();
                        menu = true;
                        chooseDifficulty = false;
                    }
                }

                else if (lang=="th")
                {
                    if (difficultyItems["ง่าย"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Easy;
                        SetTimersForEasyGame();
                        menu = true;
                        chooseDifficulty = false;
                    }

                    else if (difficultyItems["ปานกลาง"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Medium;
                        SetTimersForMediumGame();
                        menu = true;
                        chooseDifficulty = false;
                    }

                    else if (difficultyItems["ยาก"].Contains(new Point(e.X, e.Y)))
                    {
                        currentDifficulty = GameDifficulty.Hard;
                        SetTimersForHardGame();
                        menu = true;
                        chooseDifficulty = false;
                    }
                }
            }
        }

        private void SetTimersForEasyGame()
        {
            moveTimerForEasyBots.Interval = 45;
            moveTimerForMediumBots.Interval = 60;
            moveTimerForHardBots.Interval = 70;
            shootTimerForBots.Interval = 500;
        }

        private void SetTimersForMediumGame()
        {
            moveTimerForEasyBots.Interval = 35;
            moveTimerForMediumBots.Interval = 50;
            moveTimerForHardBots.Interval = 70;
            shootTimerForBots.Interval = 350;
        }

        private void SetTimersForHardGame()
        {
            moveTimerForEasyBots.Interval = 30;
            moveTimerForMediumBots.Interval = 40;
            moveTimerForHardBots.Interval = 60;
            shootTimerForBots.Interval = 200;
        }

        private void AddBotTimer_Tick(object sender, EventArgs e)
        {
            if (playStage) game.AddBot();
        }

        private void MoveTimerForHardBots_Tick(object sender, EventArgs e)
        {
            if (playStage) game.MoveBots(BotDifficulty.Hard);
        }

        private void MoveTimerForMediumBots_Tick(object sender, EventArgs e)
        {
            if (playStage) game.MoveBots(BotDifficulty.Medium);
        }

        private void MoveTimerForEasyBots_Tick(object sender, EventArgs e)
        {
            if (playStage) game.MoveBots(BotDifficulty.Easy);
        }

        private void ShootTimerForBots_Tick(object sender, EventArgs e)
        {
            if (playStage)
            {
                foreach (var bot in game.bots) { game.Shoot(bot); }
            }
        }

        private void ShootTimer_Tick(object sender, EventArgs e)
        {
            if (shoot) game.Shoot(game.player);
        }

        private void BulletsTimer_Tick(object sender, EventArgs e)
        {

            if (playStage)
            {
                if (game.gameOver)
                {
                    gameOver = true;
                    winOrLose = gameOverTxt;
                    winOrLoseAnimationTimer.Start();
                    moveTimerForMediumBots.Stop();
                    moveTimerForEasyBots.Stop();
                    moveTimerForHardBots.Stop();
                    shootTimerForBots.Stop();
                    shootTimer.Stop();
                }

                else if (game.playerWin)
                {
                    mapWinner = true;
                    winOrLose = gameWinnerTxt;
                    winOrLoseAnimationTimer.Start();
                }

                game.MoveBullet();
            }
        }
    }
}
