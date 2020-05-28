using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Carcassonne
{
    public class GraphCell : GraphObject
    {
        public bool isCell { get; private set; }

        public GraphCell(string name, Texture2D _texture, Vector2 _position, Color _color) : base(name,_texture,_position,_color)
        {
            Name = name;
            Texture = _texture;
            Position = _position;
            Color = _color;
            int x = (int)_position.X;
            int y = (int)_position.Y;
            Rectangle = new Rectangle(x, y, _texture.Width, _texture.Height);
            Scale = 1.0f;
            Effects = new SpriteEffects();
            Rotation = 0.0f;
            Origin = Vector2.Zero;
            LayerDepth = 0;
            IsButton = false;
            isCell = true;
        }

        public GraphCell()
        {
            Name = "NONAME";
            Texture = null;
            Position = Vector2.Zero;
            Color = Color.White;
            Rectangle = new Rectangle();
            Scale = 1.0f;
            Effects = new SpriteEffects();
            Rotation = 0.0f;
            Origin = Vector2.Zero;
            LayerDepth = 0;
            IsButton = false;
            isCell = true;
        }

        public void Rotate()
        {
            this.Rotation = 1.57f;
        }
    }
}
