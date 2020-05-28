using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{

    [Serializable]
    public class CellObject
    {
        public string objectType;
        public byte[] Coordinates;

        public CellObject(string objectType, byte[] coordinates)
        {
            this.Coordinates = coordinates;
            this.objectType = objectType;
        }
    }

}
