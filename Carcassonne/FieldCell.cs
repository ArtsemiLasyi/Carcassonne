using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    [Serializable]
    public class FieldCell : Cell
    {
        public GraphCell graphCell;

        public FieldCell(byte[,] matrix, GraphCell graph) : base(matrix)
        {
            this.matrix = matrix;
            this.graphCell = graph;
        }
        
        public FieldCell()
        {
            this.matrix = new byte[size, size];
            this.graphCell = new GraphCell();
        }

        public FieldCell(Cell cell, GraphCell graphCell)
        {
            this.matrix = cell.matrix;
            this.graphCell = graphCell;
            this.SentServants = cell.SentServants;
        }

        public FieldCell(CellInformation cellInformation, GraphCell graphCell)
        {
            this.matrix = cellInformation.matrix;
            this.SentServants = new List<SentServant>();
            this.graphCell = graphCell;
        }
    }
}
