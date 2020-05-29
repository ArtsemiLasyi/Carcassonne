using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Net.Sockets;
using System.Net;

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

            serverIP = GameGlobals.UpdateString(serverIP, 15);

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
                    if (_graphObject.Name.Equals("BACK"))
                    {
                        MediaPlayer.Stop();
                        changeScreen = PossibleScreen.main;
                        Hide();
                    }
                    if (_graphObject.Name.Equals("SERVER"))
                    {
                        MediaPlayer.Stop();
                        GameGlobals.Initialize();
                        GameGlobals.StartServer();
                        GameGlobals.Player = new Player(GameGlobals.NICKNAMES[0], Player.PlayerColor.Red);
                        GameGlobals.Player.Connect(GameGlobals.LocalIPAddress(), GameGlobals.PORT);
                        changeScreen = PossibleScreen.lobby;
                        Hide();
                    }
                    if (_graphObject.Name.Equals("JOIN"))
                    {
                        if (GameGlobals.isIP(serverIP))
                        {
                            MediaPlayer.Stop();
                            GameGlobals.Initialize();
                            GameGlobals.Player = new Player(GameGlobals.NICKNAMES[1], Player.PlayerColor.Blue);
                            GameGlobals.Player.Connect(serverIP, GameGlobals.PORT);
                            changeScreen = PossibleScreen.lobby;
                            Hide();
                        }
                    }
                }
            }

        }

        public override void LoadContent(ContentManager Content, int WIDTH, int HEIGHT)
        {
            textBlock = Content.Load<SpriteFont>("Fonts/File");
            GraphObject gameMenuBackground = new GraphObject("BACKGROUND",Content.Load<Texture2D>(GameGlobals.TEXTUREBACKGROUND), Vector2.Zero, Color.White, false);
            gameMenuBackground.Scale = 4.0f;
            GraphObject gameMenuTxtBack = new GraphObject("BACK", Content.Load<Texture2D>(GameGlobals.TEXTUREEXIT), Vector2.Zero, Color.Black, true);
            gameMenuTxtBack.Position = new Vector2(WIDTH - (2*gameMenuTxtBack.Texture.Width), 6 * HEIGHT / 7);
            GraphObject gameMenuTxtNewServer = new GraphObject("SERVER", Content.Load<Texture2D>(GameGlobals.TEXTURENEWSERVER), Vector2.Zero, Color.Black, true);
            gameMenuTxtNewServer.Position = new Vector2(WIDTH / 2 - (gameMenuTxtNewServer.Texture.Width / 2), 3 * HEIGHT / 7);
            GraphObject gameMenuTxtJoinGame = new GraphObject("JOIN", Content.Load<Texture2D>(GameGlobals.TEXTUREJOIN), Vector2.Zero, Color.Black, true);
            gameMenuTxtJoinGame.Position = new Vector2(WIDTH / 2 - (gameMenuTxtJoinGame.Texture.Width / 2), 5 * HEIGHT / 7);
            gameMenuInputForm = new GraphObject("INPUT", Content.Load<Texture2D>(GameGlobals.TEXTUREINPUTFORM), Vector2.Zero, Color.Black, false);
            gameMenuInputForm.Position = new Vector2(gameMenuInputForm.Texture.Width / 2, 2 * HEIGHT / 7);
            textPosition = gameMenuInputForm.Position;
            graphList.Add(gameMenuBackground);
            graphList.Add(gameMenuTxtBack);
            graphList.Add(gameMenuTxtJoinGame);
            graphList.Add(gameMenuTxtNewServer);
            graphList.Add(gameMenuInputForm);
        }
    }
}
