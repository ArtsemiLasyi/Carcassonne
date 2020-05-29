using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Carcassonne
{
    public class HelpScreen : Screen
    {
        public HelpScreen(List<GraphObject> _graphobject)
        {
            graphList = new List<GraphObject>(_graphobject);
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
            changeScreen = PossibleScreen.help;

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
                    if (_graphObject.Name.Equals("EXIT"))
                        changeScreen = PossibleScreen.main;
                }
            }

        }

        public override void LoadContent(ContentManager Content, int WIDTH, int HEIGHT)
        {
            GraphObject mainMenuBackground = new GraphObject("BACKGROUND", Content.Load<Texture2D>("Images/menu/menuBack"), Vector2.Zero, Color.White, false);
            GraphObject mainMenuTxtExit = new GraphObject("EXIT", Content.Load<Texture2D>(GameSettings.TEXTUREEXIT), Vector2.Zero, Color.Black, true);
            GraphObject rulesA = new GraphObject("RULESA", Content.Load<Texture2D>("Images/menu/rulesA"), Vector2.Zero, Color.White, false);
            GraphObject rulesB = new GraphObject("RULESB", Content.Load<Texture2D>("Images/menu/rulesB"), Vector2.Zero, Color.White, false);
            rulesA.Scale = 1.30f;
            rulesB.Scale = 1.30f;
            rulesA.Position = new Vector2(0, 0);
            rulesB.Position = new Vector2(rulesA.Texture.Width*rulesA.Scale, 0);
            mainMenuTxtExit.Position = new Vector2(WIDTH - (mainMenuTxtExit.Texture.Width), 6 * HEIGHT / 7);
            graphList.Add(mainMenuBackground);
            graphList.Add(mainMenuTxtExit);
            graphList.Add(rulesA);
            graphList.Add(rulesB);
        }

    }
}
