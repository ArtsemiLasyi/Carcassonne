using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Carcassonne
{
    public static class GameField
    {
        public const int width = 83*2+1;
        public const int height = 83*2+1;
        public const int size = 5;
        private static string source = "field.conf";
        public static FieldCell[,] gameField = new FieldCell[height, width];
        public static List<CellInformation> cellInformations = new List<CellInformation>();

        public static void Initialize()
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j] = new FieldCell();
                }
            }
        }
        
        public static void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    if (gameField[i, j].graphCell.Texture != null)
                        gameField[i, j].graphCell.Draw(spritebatch);
                }
            }
        }

        public static FieldCell getFieldCellByName(string name, List<FieldCell> fieldCells)
        {
            foreach(var fieldcell in fieldCells)
            {
                if (name.Equals(fieldcell.graphCell.Name))
                    return fieldcell;
            }
            return null;
        }

        public static CellInformation getCellInformationByName(string name, List<CellInformation> cellInformations)
        {
            foreach (var cellInformation in cellInformations)
            {
                if (name.Equals(cellInformation.name))
                    return cellInformation;
            }
            return null;
        }

        public static void getInformation()
        {
            StreamReader stream = new StreamReader(source);
            string information = stream.ReadToEnd();
            
            Regex regex1 = new Regex(@"[A-Za-z]* [0-9]*");                           //для названия и количества
            Regex regex2 = new Regex(@"{.*}");                                       //для клеток поля
            Regex regex3 = new Regex(@"((city|road|monastery)\([\,0-9]*\)\r\n)+");     //для объектов
            MatchCollection matches1 = regex1.Matches(information);
            MatchCollection matches2 = regex2.Matches(information);
            MatchCollection matches3 = regex3.Matches(information);
            for(int i = 0; i < matches1.Count; i++)
            {
                List<CellObject> cellObjects = new List<CellObject>();
                string name = matches1[i].Value.Substring(0, matches1[i].Value.IndexOf(" "));
                int number = Int32.Parse(matches1[i].Value.Substring(matches1[i].Value.IndexOf(" ")+1));
                byte[,] matrix = new byte[size, size];
                string bytes = matches2[i].Value;
                int index = 1;
                for (int j = 0; j < size; j++)
                {
                    for(int z = 0; z < size; z++)
                    {
                        string ciffer = bytes.Substring(index, 1);
                        matrix[j, z] = byte.Parse(ciffer);
                        index += 2;
                    }
                }
                string[] objects = matches3[i].Value.Split(new char[] { '\r', '\n' });
                for(int k = 0; k < objects.Length; k++)
                {
                    int ind = objects[k].IndexOf("(");
                    if (ind != -1)
                    {
                        string type = objects[k].Substring(0, ind);
                        string[] coordinates = objects[k].Substring(ind + 1).Split(new char[] { ',', ')' });
                        byte[] byteCoordinates = new byte[coordinates.Length];
                        for (int t = 0; t < coordinates.Length; t++)
                        {
                            if (!coordinates[t].Equals(""))
                                byteCoordinates[t] = byte.Parse(coordinates[t]);
                        }
                        cellObjects.Add(new CellObject(type, byteCoordinates));
                    }
                }
                cellInformations.Add(new CellInformation(name, number, matrix, cellObjects));
            }
        }

        public static void FillCell(int x, int y, FieldCell fieldCell)
        {
            fieldCell.graphCell.Position.X = x*fieldCell.graphCell.Texture.Width * fieldCell.graphCell.Scale;
            fieldCell.graphCell.Position.Y = y*fieldCell.graphCell.Texture.Height * fieldCell.graphCell.Scale;
            gameField[y, x] = fieldCell;
        }




        public static void MoveUp()
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.Y += 10.0f;
                }
            }
        }

        public static void MoveUp(float value)
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.Y += value;
                }
            }
        }

        public static void MoveDown()
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.Y -= 10.0f;
                }
            }
        }

        public static void MoveDown(float value)
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.Y -= value;
                }
            }
        }

        public static void MoveLeft()
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.X -= 10.0f;
                }
            }
        }

        public static void MoveLeft(float value)
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.X -= value;
                }
            }
        }

        public static void MoveRight()
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.X += 10.0f;
                }
            }
        }

        public static void MoveRight(float value)
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    GameField.gameField[i, j].graphCell.Position.X += value;
                }
            }
        }

        public static void Decrease()
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    if (GameField.gameField[i, j].graphCell.Scale < 0.5f)
                        return;
                    GameField.gameField[i, j].graphCell.Scale -= 0.01f;
                    if (gameField[i, j].graphCell.Texture != null)
                    {
                        GameField.gameField[i, j].graphCell.Position.X -= 0.01f * gameField[i, j].graphCell.Texture.Width * j;
                        GameField.gameField[i, j].graphCell.Position.Y -= 0.01f * gameField[i, j].graphCell.Texture.Height * i;
                    }
                }
            }
        }

        public static void Increase()
        {
            for (int i = 0; i < GameField.height; i++)
            {
                for (int j = 0; j < GameField.width; j++)
                {
                    if (GameField.gameField[i, j].graphCell.Scale > 1.5f)
                        return;
                    GameField.gameField[i, j].graphCell.Scale += 0.01f;
                    if (gameField[i, j].graphCell.Texture != null)
                    {
                        GameField.gameField[i, j].graphCell.Position.X += 0.01f * gameField[i, j].graphCell.Texture.Width * j;
                        GameField.gameField[i, j].graphCell.Position.Y += 0.01f * gameField[i, j].graphCell.Texture.Height * i;
                    }
                }
            }
        }
    }
}
