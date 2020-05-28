using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Carcassonne
{
    public class GraphObject
    {
        public string Name { get; protected set; }
        public Texture2D Texture { set; get; }        //рисуемая текстура
        public Rectangle Rectangle { set; get; }      //указывает на прямоугольную область текстуры, которую надо отрисовать
        public Color Color { set; get; }              //цвет подсветки
        public Vector2 Position;                      //координаты верхнего левого угла текстуры
        public float Scale;                           //коэффициент масштабирования
        public SpriteEffects Effects { set; get; }    //отразить текстуру
        public float Rotation { set; get; }           //задает угол вращения изображения
        public Vector2 Origin { set; get; }           //центр вращения
        public float LayerDepth { set; get; }         //глубина наложения относительно других изображений

        public bool IsButton { set; get; }

        public GraphObject(string name, Texture2D _texture, Vector2 _position, Color _color, bool _isbutton)
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
            IsButton = _isbutton;
        }

        public GraphObject()
        {
            Name = "NONAME";
            Texture = null;
            Position = Vector2.Zero;
            Color = Color.White;
            IsButton = false;
        }

        public GraphObject(string name, Texture2D _texture, Vector2 _position, Color _color)
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
        }

        public GraphObject(string name, Texture2D _texture, Vector2 _position, Color _color,
                           Rectangle _rectangle, float _scale, float _rotation,
                           SpriteEffects _spriteEffects, Vector2 _origin, float _layerdepth, bool _isbutton)
        {
            Name = name;
            Texture = _texture;
            Position = _position;
            Color = _color;
            Rectangle = _rectangle;
            Scale = _scale;
            Effects = _spriteEffects;
            Rotation = _rotation;
            Origin = _origin;
            LayerDepth = _layerdepth;
            IsButton = _isbutton;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Position, Rectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }
    }
}
