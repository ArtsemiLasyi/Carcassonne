using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    [Serializable]
    public class Cell
    {
        public const int size = 5;
        public int X { get; set; }
        public int Y { get; set; }
        public byte[,] matrix = new byte[size, size];
        public List<CellObject> cellObjects;
        public List<SentServant> SentServants;

        public Cell(byte[,] matrix)
        {
            this.matrix = matrix;
        }

        public Cell()
        {
            this.matrix = new byte[size, size];
        }

        public Cell(CellInformation cellInformation, List<SentServant> sentServants)
        {
            this.matrix = cellInformation.matrix;
            this.cellObjects = cellInformation.cellObjects;
            this.SentServants = sentServants;
        }

        public Cell(CellInformation cellInformation)
        {
            this.matrix = cellInformation.matrix;
            this.cellObjects = cellInformation.cellObjects;
            this.SentServants = new List<SentServant>();
        }

        public void Rotate()
        {
            byte[,] buffer = matrix;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int indexX = size - i - 1;
                    int indexY = j;
                    matrix[indexY, indexX] = buffer[i, j];
                }
            }
            foreach (var field in cellObjects)
            {
                byte[] anotherbuffer = (byte[])field.Coordinates.Clone();

                for (int i = 0; i < field.Coordinates.Length; i++)
                {
                    int x = i % size;
                    int y = i / size;
                    int newX = size - y - 1;
                    int newY = x;
                    field.Coordinates[newY * size + newX] = anotherbuffer[y * size + x];
                }
            }
        }
    }
}