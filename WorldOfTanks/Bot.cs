using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanks
{
    class Bot : Tank
    {
        public bool canMove;
        public int countStep;

        public Bot()
        {
            countStep = 0;
            canMove = true;
        }
    }
}
