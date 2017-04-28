using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    public class GameModel
    {
        public bool gameOver = false;
        public  Tank player = new PlayerTank();
        public List<Tank> bot;

        public GameModel()
        {

        }
    }
}
