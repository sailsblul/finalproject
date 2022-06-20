using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace cellescape
{
    class LevelButton
    {
        Rectangle _box;
        int _number;
        public LevelButton(int num, Rectangle rect)
        {
            _number = num;
            _box = rect;
        }
        public int Number
        {
            get { return _number; }
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, SpriteFont font)
        {
            spriteBatch.Draw(texture, _box, Color.Aquamarine);
            spriteBatch.DrawString(font, _number.ToString(), _box.Center.ToVector2() - font.MeasureString(_number.ToString()) / 2, Color.Gray);
        }
        public bool IsPressed(MouseState mouse) => mouse.LeftButton == ButtonState.Pressed && _box.Contains(mouse.Position);
    }
}
