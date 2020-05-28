using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    public class Player
    {
        public PlayerColor Color { get; private set; }
        public int Points { get; private set; }
        public int FreeServants { get; private set; }

        private List<SentServant> sentServants;


        public Player(PlayerColor color)
        {
            Color = color;
            Points = 0;
            FreeServants = 7;
            sentServants = new List<SentServant>();
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

        /*
        public void SendServant(int x, int y)
        {
            var newServant = new SentServant(x, y);
            sentServants.Add(newServant);
            FreeServants -= 1;
        }

        public void ReturnServant(int x, int y)
        {
            DeleteServant(x, y);
            FreeServants += 1;
        }

        private void DeleteServant(int x, int y)
        {
            foreach(var servant in sentServants)
            {
                if ((servant.X == x)&&(servant.Y == y))
                {
                    sentServants.Remove(servant);
                    return;
                }
            }
        }
        */
    }
}
