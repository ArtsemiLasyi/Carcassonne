using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    [Serializable]
    public class SentServant
    {
        public string Type { get; private set; }
        public CellObject Destination { get; private set; }

        public SentServant(string objectType, CellObject objectDestination)
        {
            this.Type = objectType;
            this.Destination = objectDestination;
        }
    }
}
