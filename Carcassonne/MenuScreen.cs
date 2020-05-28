using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Carcassonne
{
    public class MenuScreen : Screen
    {

        public MenuScreen(List<GraphObject> _graphobject)
        {
            graphList = new List<GraphObject>(_graphobject);
            Show();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GraphObject _graphObject in graphList)
            {
                _graphObject.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            Rectangle MouseRect = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            changeScreen = PossibleScreen.main;


            foreach (GraphObject _graphObject in graphList)
            {
                var temp = new Rectangle((int)_graphObject.Position.X, (int)_graphObject.Position.Y, _graphObject.Texture.Width, _graphObject.Texture.Height);
                if ((MouseRect.Intersects(temp)) && (_graphObject.IsButton))
                {
                    _graphObject.Color = Color.Black;
                }
                else
                {
                    _graphObject.Color = Color.White;
                }

                if ((currentMouseState.LeftButton == ButtonState.Pressed) && MouseRect.Intersects(temp) && (_graphObject.IsButton) && (_graphObject.Color == Color.Black))
                {
                    Hide();
                    if (_graphObject.Name.Equals("START"))
                        changeScreen = PossibleScreen.game;
                    else if (_graphObject.Name.Equals("HELP"))
                        changeScreen = PossibleScreen.help;
                    else if (_graphObject.Name.Equals("EXIT"))
                        changeScreen = PossibleScreen.quit;
                }
            }

        }

        public override void LoadContent(ContentManager Content, int WIDTH, int HEIGHT)
        {
            GraphObject mainMenuBackground = new GraphObject("BACKGROUND", Content.Load<Texture2D>("Images/menu/menuBack"), Vector2.Zero, Color.White, false);
            GraphObject mainMenuGameName = new GraphObject("LOGO", Content.Load<Texture2D>("Images/menu/menuGameNameEn"), Vector2.Zero, Color.White, false);
            mainMenuGameName.Position = new Vector2(WIDTH / 2 - (mainMenuGameName.Texture.Width / 2), 0);
            GraphObject mainMenuTxtStartGame = new GraphObject("START", Content.Load<Texture2D>("Images/menu/menuTxtStartGame"), Vector2.Zero, Color.Black, true);
            mainMenuTxtStartGame.Position = new Vector2(WIDTH / 2 - (mainMenuTxtStartGame.Texture.Width / 2), 2 * HEIGHT / 7);
            GraphObject mainMenuTxtHelp = new GraphObject("HELP", Content.Load<Texture2D>("Images/menu/menuTxtHelp"), Vector2.Zero, Color.Black, true);
            mainMenuTxtHelp.Position = new Vector2(WIDTH / 2 - (mainMenuTxtHelp.Texture.Width / 2), 4 * HEIGHT / 7);
            GraphObject mainMenuTxtExit = new GraphObject("EXIT", Content.Load<Texture2D>("Images/menu/menuTxtExit"), Vector2.Zero, Color.Black, true);
            mainMenuTxtExit.Position = new Vector2(WIDTH / 2 - (mainMenuTxtExit.Texture.Width / 2), 6 * HEIGHT / 7);
            graphList.Add(mainMenuBackground);
            graphList.Add(mainMenuGameName);
            graphList.Add(mainMenuTxtStartGame);
            graphList.Add(mainMenuTxtHelp);
            graphList.Add(mainMenuTxtExit);
        }

    }
}
