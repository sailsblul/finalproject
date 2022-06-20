using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace cellescape
{
    class Cursor
    {
        private Point _pos;
        private Texture2D _texture;
        public Cursor(Point pos, Texture2D texture)
        {
            _pos = pos;
            _texture = texture;
        }
        public Point Position
        {
            get { return _pos; }
            set { _pos = value; }
        }
        public Rectangle Rect
        {
            get { return new Rectangle(_pos - new Point(5), new Point(10)); }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rect, Color.Plum);
        }
    }
}
