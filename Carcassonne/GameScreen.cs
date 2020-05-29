using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Carcassonne
{
    public class GameScreen : Screen
    {
        SpriteFont textBlock;
        Vector2 textPosition;
        string serverIP = "";
        GraphObject gameMenuInputForm;



        public GameScreen(List<GraphObject> _graphobject)
        {
            graphList = new List<GraphObject>(_graphobject);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GraphObject _graphObject in graphList)
            {
                _graphObject.Draw(spriteBatch);
            }
            spriteBatch.DrawString(textBlock, serverIP, textPosition, Color.Black, 0, Vector2.Zero, 4.0f, new SpriteEffects(), 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            Rectangle MouseRect = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            changeScreen = PossibleScreen.game;

            serverIP = UpdateString(serverIP);

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
                    MediaPlayer.Stop();
                    if (_graphObject.Name.Equals("BACK"))
                        changeScreen = PossibleScreen.main;
                    if (_graphObject.Name.Equals("START"))
                        changeScreen = PossibleScreen.startgame;
                    Hide();
                }
            }

        }

        public override void LoadContent(ContentManager Content, int WIDTH, int HEIGHT)
        {
            textBlock = Content.Load<SpriteFont>("Fonts/File");
            GraphObject gameMenuBackground = new GraphObject("BACKGROUND",Content.Load<Texture2D>(GameSettings.TEXTUREBACKGROUND), Vector2.Zero, Color.White, false);
            gameMenuBackground.Scale = 4.0f;
            GraphObject gameMenuTxtBack = new GraphObject("BACK", Content.Load<Texture2D>("Images/menu/menuTxtExit"), Vector2.Zero, Color.Black, true);
            gameMenuTxtBack.Position = new Vector2(WIDTH - (gameMenuTxtBack.Texture.Width), 6 * HEIGHT / 7);
            GraphObject gameMenuTxtStartGame = new GraphObject("START", Content.Load<Texture2D>("Images/menu/menuStartNewGame"), Vector2.Zero, Color.Black, true);
            gameMenuTxtStartGame.Position = new Vector2(gameMenuTxtStartGame.Texture.Width/2, 6 * HEIGHT / 7);
            gameMenuTxtStartGame.Scale = 1.3f;
            gameMenuInputForm = new GraphObject("INPUT", Content.Load<Texture2D>("Images/menu/inputForm"), Vector2.Zero, Color.Black, false);
            gameMenuInputForm.Position = new Vector2(gameMenuInputForm.Texture.Width / 2, 2 * HEIGHT / 7);
            textPosition = gameMenuInputForm.Position;
            graphList.Add(gameMenuBackground);
            graphList.Add(gameMenuTxtBack);
            graphList.Add(gameMenuTxtStartGame);
            graphList.Add(gameMenuInputForm);
        }

        private string UpdateString(string serverIP)
        {
            MyKeyboard.GetCurrentState();
            serverIP = MyKeyboard.getInput(serverIP);
            if (serverIP.Length > 15)
                serverIP = serverIP.Substring(0, serverIP.Length - 1);
            return serverIP;
        }
    }
}
