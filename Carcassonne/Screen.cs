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
    abstract public class Screen
    {

        public List<GraphObject> graphList;
        public PossibleScreen changeScreen;

        public bool IsActive { set; get; }
        abstract public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        abstract public void Update(GameTime gameTime);

        abstract public void LoadContent(ContentManager Content, int WIDTH, int HEIGHT);

        public void Show(Screen screen)
        {
            screen.IsActive = true;
        }

        public void Hide(Screen screen)
        {
            screen.IsActive = false;
        }

        public void Show()
        {
            IsActive = true;
        }

        public void Hide()
        {
            IsActive = false;
        }
    }
}
