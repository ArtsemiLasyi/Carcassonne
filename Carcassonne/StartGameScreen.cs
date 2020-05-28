using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Security.Cryptography.X509Certificates;

namespace Carcassonne
{
    public class StartGameScreen : Screen
    {
        List<FieldCell> Cells = new List<FieldCell>();

        public StartGameScreen(List<GraphObject> _graphobject)
        {
            graphList = new List<GraphObject>(_graphobject);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GraphObject _graphObject in graphList)
            {
                _graphObject.Draw(spriteBatch);
            }
            GameField.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            Rectangle MouseRect = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            changeScreen = PossibleScreen.startgame;

            if(currentMouseState.LeftButton == ButtonState.Pressed)
            {
                
            }

            UpdateGameField();

            foreach (GraphObject _graphObject in graphList)
            {
                var temp = new Rectangle((int)_graphObject.Position.X, (int)_graphObject.Position.Y, _graphObject.Texture.Width, _graphObject.Texture.Height);
                if ((MouseRect.Intersects(temp)) && (_graphObject.IsButton))
                {
                    _graphObject.Color = Color.Brown;
                }
                else
                {
                    _graphObject.Color = Color.White;
                }

                if ((currentMouseState.LeftButton == ButtonState.Pressed) && MouseRect.Intersects(temp) && (_graphObject.IsButton) && (_graphObject.Color == Color.Brown))
                {
                    Hide();
                    if (_graphObject.Name.Equals("BACK"))
                        changeScreen = PossibleScreen.game;
                }
            }

        }

        public override void LoadContent(ContentManager Content, int WIDTH, int HEIGHT)
        {
            GameField.getInformation();
            GameField.Initialize();
            GraphObject gameMenuBackground = new GraphObject("BACKGROUND", Content.Load<Texture2D>("Images/menu/treeTextureNew"), Vector2.Zero, Color.White, false);
            gameMenuBackground.Scale = 4.0f;
            GraphObject gameMenuTxtBack = new GraphObject("BACK", Content.Load<Texture2D>("Images/menu/menuTxtExit"), Vector2.Zero, Color.Black, true);
            gameMenuTxtBack.Position = new Vector2(WIDTH - (gameMenuTxtBack.Texture.Width)*3/4, 1 * HEIGHT / 15);
            LoadCells(Cells, Content, WIDTH, HEIGHT);
            graphList.Add(gameMenuBackground);
            graphList.Add(gameMenuTxtBack);

            var cell = GameField.getFieldCellByName("WinD", Cells);
            GameField.FillCell(1, 1, cell);
        }

        private void UpdateGameField()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                GameField.MoveUp();
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                GameField.MoveDown();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                GameField.MoveRight();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                GameField.MoveLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                GameField.Increase();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                GameField.Decrease();
        }

        private void LoadCells(List<FieldCell> Cells, ContentManager Content, int WIDTH, int HEIGHT)
        {
            GraphCell WinA = new GraphCell("WinA", Content.Load<Texture2D>("Images/cells/Win A"), Vector2.Zero, Color.White);
            GraphCell WinB = new GraphCell("WinB", Content.Load<Texture2D>("Images/cells/Win B"), Vector2.Zero, Color.White);
            GraphCell WinC = new GraphCell("WinC", Content.Load<Texture2D>("Images/cells/Win C"), Vector2.Zero, Color.White);
            GraphCell WinD = new GraphCell("WinD", Content.Load<Texture2D>("Images/cells/Win D"), Vector2.Zero, Color.White);
            GraphCell WinE = new GraphCell("WinE", Content.Load<Texture2D>("Images/cells/Win E"), Vector2.Zero, Color.White);
            GraphCell WinF = new GraphCell("WinF", Content.Load<Texture2D>("Images/cells/Win F"), Vector2.Zero, Color.White);
            GraphCell WinG = new GraphCell("WinG", Content.Load<Texture2D>("Images/cells/Win G"), Vector2.Zero, Color.White);
            GraphCell WinH = new GraphCell("WinH", Content.Load<Texture2D>("Images/cells/Win H"), Vector2.Zero, Color.White);
            GraphCell WinI = new GraphCell("WinI", Content.Load<Texture2D>("Images/cells/Win I"), Vector2.Zero, Color.White);
            GraphCell WinJ = new GraphCell("WinJ", Content.Load<Texture2D>("Images/cells/Win J"), Vector2.Zero, Color.White);
            GraphCell WinK = new GraphCell("WinK", Content.Load<Texture2D>("Images/cells/Win K"), Vector2.Zero, Color.White);
            GraphCell WinL = new GraphCell("WinL", Content.Load<Texture2D>("Images/cells/Win L"), Vector2.Zero, Color.White);
            GraphCell WinM = new GraphCell("WinM", Content.Load<Texture2D>("Images/cells/Win M"), Vector2.Zero, Color.White);
            GraphCell WinN = new GraphCell("WinN", Content.Load<Texture2D>("Images/cells/Win N"), Vector2.Zero, Color.White);
            GraphCell WinO = new GraphCell("WinO", Content.Load<Texture2D>("Images/cells/Win O"), Vector2.Zero, Color.White);
            GraphCell WinP = new GraphCell("WinP", Content.Load<Texture2D>("Images/cells/Win P"), Vector2.Zero, Color.White);
            GraphCell WinQ = new GraphCell("WinQ", Content.Load<Texture2D>("Images/cells/Win Q"), Vector2.Zero, Color.White);
            GraphCell WinR = new GraphCell("WinR", Content.Load<Texture2D>("Images/cells/Win R"), Vector2.Zero, Color.White);
            GraphCell WinS = new GraphCell("WinS", Content.Load<Texture2D>("Images/cells/Win S"), Vector2.Zero, Color.White);
            GraphCell WinT = new GraphCell("WinT", Content.Load<Texture2D>("Images/cells/Win T"), Vector2.Zero, Color.White);
            GraphCell WinU = new GraphCell("WinU", Content.Load<Texture2D>("Images/cells/Win U"), Vector2.Zero, Color.White);
            GraphCell WinV = new GraphCell("WinV", Content.Load<Texture2D>("Images/cells/Win V"), Vector2.Zero, Color.White);
            GraphCell WinW = new GraphCell("WinW", Content.Load<Texture2D>("Images/cells/Win W"), Vector2.Zero, Color.White);
            GraphCell WinX = new GraphCell("WinX", Content.Load<Texture2D>("Images/cells/Win X"), Vector2.Zero, Color.White);
            GraphCell WinTierA = new GraphCell("WinTierA", Content.Load<Texture2D>("Images/cells/WinTier A"), Vector2.Zero, Color.White);
            GraphCell WinTierB = new GraphCell("WinTierB", Content.Load<Texture2D>("Images/cells/WinTier B"), Vector2.Zero, Color.White);
            GraphCell WinTierC = new GraphCell("WinTierC", Content.Load<Texture2D>("Images/cells/WinTier C"), Vector2.Zero, Color.White);
            GraphCell WinTierD = new GraphCell("WinTierD", Content.Load<Texture2D>("Images/cells/WinTier D"), Vector2.Zero, Color.White);
            GraphCell WinTierE = new GraphCell("WinTierE", Content.Load<Texture2D>("Images/cells/WinTier E"), Vector2.Zero, Color.White);
            GraphCell WinTierF = new GraphCell("WinTierF", Content.Load<Texture2D>("Images/cells/WinTier F"), Vector2.Zero, Color.White);
            GraphCell WinTierG = new GraphCell("WinTierG", Content.Load<Texture2D>("Images/cells/WinTier G"), Vector2.Zero, Color.White);
            GraphCell WinTierH = new GraphCell("WinTierH", Content.Load<Texture2D>("Images/cells/WinTier H"), Vector2.Zero, Color.White);
            GraphCell WinTierI = new GraphCell("WinTierI", Content.Load<Texture2D>("Images/cells/WinTier I"), Vector2.Zero, Color.White);
            GraphCell WinTierJ = new GraphCell("WinTierJ", Content.Load<Texture2D>("Images/cells/WinTier J"), Vector2.Zero, Color.White);
            GraphCell WinTierK = new GraphCell("WinTierK", Content.Load<Texture2D>("Images/cells/WinTier K"), Vector2.Zero, Color.White);
            GraphCell WinTierL = new GraphCell("WinTierL", Content.Load<Texture2D>("Images/cells/WinTier L"), Vector2.Zero, Color.White);
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinA", GameField.cellInformations), WinA));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinB", GameField.cellInformations), WinB));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinC", GameField.cellInformations), WinC));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinD", GameField.cellInformations), WinD));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinE", GameField.cellInformations), WinE));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinF", GameField.cellInformations), WinF));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinG", GameField.cellInformations), WinG));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinH", GameField.cellInformations), WinH));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinI", GameField.cellInformations), WinI));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinJ", GameField.cellInformations), WinJ));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinK", GameField.cellInformations), WinK));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinL", GameField.cellInformations), WinL));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinM", GameField.cellInformations), WinM));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinN", GameField.cellInformations), WinN));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinO", GameField.cellInformations), WinO));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinP", GameField.cellInformations), WinP));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinQ", GameField.cellInformations), WinQ));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinR", GameField.cellInformations), WinR));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinS", GameField.cellInformations), WinS));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinT", GameField.cellInformations), WinT));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinU", GameField.cellInformations), WinU));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinV", GameField.cellInformations), WinV));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinW", GameField.cellInformations), WinW));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinX", GameField.cellInformations), WinX));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierA", GameField.cellInformations), WinTierA));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierB", GameField.cellInformations), WinTierB));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierC", GameField.cellInformations), WinTierC));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierD", GameField.cellInformations), WinTierD));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierE", GameField.cellInformations), WinTierE));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierF", GameField.cellInformations), WinTierF));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierG", GameField.cellInformations), WinTierG));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierH", GameField.cellInformations), WinTierH));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierI", GameField.cellInformations), WinTierI));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierJ", GameField.cellInformations), WinTierJ));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierK", GameField.cellInformations), WinTierK));
            Cells.Add(new FieldCell(GameField.getCellInformationByName("WinTierL", GameField.cellInformations), WinTierL));
        }
    }
}
