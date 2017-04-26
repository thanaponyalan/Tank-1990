using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank1990
{
    public interface IMap
    {
        bool IsEmptyWay(MoveElement element); // можно ли двигаться дальше
    }
}
