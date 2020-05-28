using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    public class CellInformation
    {
        public string name;
        public int number;
        public byte[,] matrix = new byte[5,5];
        public List<CellObject> cellObjects;

        public CellInformation(string name, int number, byte[,] matrix, List<CellObject> cellObjects)
        {
            this.cellObjects = cellObjects;
            this.matrix = matrix;
            this.name = name;
            this.number = number;
        }
    }
}
