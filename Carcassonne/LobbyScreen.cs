using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Carcassonne
{
    public class LobbyScreen : Screen
    {
        SpriteFont textBlock;
        Vector2 messagePosition;
        Vector2 chatPosition;
        GraphObject gameMenuInputForm;

        public LobbyScreen(List<GraphObject> _graphobject)
        {
            graphList = new List<GraphObject>(_graphobject);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GraphObject _graphObject in graphList)
            {
                _graphObject.Draw(spriteBatch);
            }
            spriteBatch.DrawString(textBlock, GameGlobals.message, messagePosition, Color.Black, 0, Vector2.Zero, 4.0f, new SpriteEffects(), 0.0f);
            spriteBatch.DrawString(textBlock, GameGlobals.chat, chatPosition, Color.Black, 0, Vector2.Zero, 2.0f, new SpriteEffects(), 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            Rectangle MouseRect = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            changeScreen = PossibleScreen.lobby;

            GameGlobals.message = GameGlobals.UpdateString(GameGlobals.message, 23);


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
                        changeScreen = PossibleScreen.main;
                        GameGlobals.Player.Disconnect();
                        GameGlobals.StopServer();
                        GameGlobals.Initialize();
                        Hide();
                    }
                    if (_graphObject.Name.Equals("SENDTEXT"))
                    {
                        GameGlobals.Player.SendMessage(GameGlobals.message);
                        GameGlobals.message = "";
                    }
                    if ((_graphObject.Name.Equals("START")))
                    {
                        if (GameGlobals.Player.isReady == false)
                        {
                            GameGlobals.Player.SendMessage(GameGlobals.IAMREADY);
                            GameGlobals.Player.isReady = true;
                        }
                        if (GameGlobals.GAMESTATE == GameGlobals.GameState.game)
                        {
                            changeScreen = PossibleScreen.startgame;
                            Hide();
                        }
                    }

                }
            }

        }

        public override void LoadContent(ContentManager Content, int WIDTH, int HEIGHT)
        {
            textBlock = Content.Load<SpriteFont>("Fonts/File");
            GraphObject gameMenuBackground = new GraphObject("BACKGROUND", Content.Load<Texture2D>(GameGlobals.TEXTUREBACKGROUND), Vector2.Zero, Color.White, false);
            gameMenuBackground.Scale = 4.0f;
            GraphObject gameMenuTxtBack = new GraphObject("BACK", Content.Load<Texture2D>(GameGlobals.TEXTUREEXIT), Vector2.Zero, Color.Black, true);
            gameMenuTxtBack.Position = new Vector2(WIDTH - (2 * gameMenuTxtBack.Texture.Width), 1 * HEIGHT / 15);
            GraphObject gameMenuTxtStartGame = new GraphObject("START", Content.Load<Texture2D>(GameGlobals.TEXTURESTARTNEWGAME), Vector2.Zero, Color.Black, true);
            gameMenuTxtStartGame.Position = new Vector2(gameMenuTxtStartGame.Texture.Width / 2, 6 * HEIGHT / 7);
            GraphObject gameMenuSendText = new GraphObject("SENDTEXT", Content.Load<Texture2D>(GameGlobals.TEXTURESENDTEXT), Vector2.Zero, Color.Black, true);
            gameMenuSendText.Position = new Vector2(WIDTH - (2 * gameMenuSendText.Texture.Width), 6 * HEIGHT / 7);
            gameMenuInputForm = new GraphObject("INPUT", Content.Load<Texture2D>(GameGlobals.TEXTUREINPUTFORM), Vector2.Zero, Color.Black, false);
            gameMenuInputForm.Position = new Vector2(gameMenuInputForm.Texture.Width / 2, 6 * HEIGHT / 7);
            GraphObject gameMenuChat = new GraphObject("CHAT", Content.Load<Texture2D>(GameGlobals.TEXTURECHAT), Vector2.Zero, Color.White, false);
            gameMenuChat.Position = new Vector2(WIDTH / 4, 1 * HEIGHT / 7);
            messagePosition = gameMenuInputForm.Position;
            chatPosition = gameMenuChat.Position;
            graphList.Add(gameMenuBackground);
            graphList.Add(gameMenuTxtBack);
            graphList.Add(gameMenuTxtStartGame);
            graphList.Add(gameMenuInputForm);
            graphList.Add(gameMenuSendText);
            graphList.Add(gameMenuChat);
        }
    }
}
