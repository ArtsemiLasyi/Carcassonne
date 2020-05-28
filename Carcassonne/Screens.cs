using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    public class Screens
    {
        public List<Screen> screenList;

        public Screens(List<Screen> _screenList)
        {
            screenList = _screenList;
        }
    }

    public enum PossibleScreen
    {
        main = 0,
        help = 1,
        game = 2,
        quit = 4,
        startgame = 8,
    }
}
