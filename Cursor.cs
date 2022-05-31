using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing
{
    class Cursor
    {
        Point _pos;
        public Cursor(Point pos)
        {
            _pos = pos;
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
    }
}
