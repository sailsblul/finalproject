using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace cellescape
{
    class Cell
    {
        private Vector2 _location;
        private Vector2 _speed;
        private Color _color;
        private int _radius;
        public Cell(Vector2 location, int radius, Color color)
        {
            _location = location;
            _speed = new Vector2(0);
            _color = color;
            _radius = radius;

        }
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public Rectangle Rect
        {
            get { return new Rectangle((int)_location.X - _radius, (int)_location.Y - _radius, 2 * _radius, 2 * _radius); }
        }
        public Vector2 Center
        {
            get { return _location; }
        }
        public int Radius
        {
            get { return _radius; }
        }
        public Color Colour
        {
            get { return _color; }
            set { _color = value; }
        }
        public void Move()
        {
            _location += _speed;
        }
        public bool Intersects(Rectangle rect)
        {
            float rW = rect.Width / 2;
            float rH = rect.Height / 2;

            float distX = Math.Abs(_location.X - (rect.Left + rW));
            float distY = Math.Abs(_location.Y - (rect.Top + rH));

            if (distX >= _radius + rW || distY >= _radius + rH)
                return false;
            if (distX < rW || distY < rH)
                return true;

            distX -= rW;
            distY -= rH;

            if (distX * distX + distY * distY < _radius * _radius)
                return true;

            return false;
        }
        public void Bounce(Rectangle rect)
        {
            float n = 0;
            while (Intersects(rect))
            {
                _location -= Speed * new Vector2((float)(0.00001 * n));
                n += 1;
            }
            _speed *= FindBounce(rect);
        }
        private Vector2 FindBounce(Rectangle rect)
        {
            if (_speed.X == 0)
                return new Vector2(1, -1);
            if (_speed.Y == 0)
                return new Vector2(-1, 1);
            float distX;
            float distY;
            if (Speed.X > 0)
                distX = rect.Left - _location.X;
            else
                distX = _location.X - rect.Right;
            if (Speed.Y > 0)
                distY = rect.Top - _location.Y;
            else
                distY = _location.Y - rect.Bottom;

            if (Math.Abs(_radius - distX) == Math.Abs(_radius - distY))
                return new Vector2(-1, -1);
            if (Math.Abs(_radius - distX) >= Math.Abs(_radius - distY))
                return new Vector2(1, -1);
            return new Vector2(-1, 1);

        }
        public Cell Copy()
        {
            return new Cell(_location, _radius, _color);
        }
        public void StopOOB(List<Rectangle> edges)
        {
            /*bool done = false;
            _location -= _speed;
            while (!done)
            {
                _location += _speed / Math.Max(_speed.X, _speed.Y);
                foreach(Rectangle rect in edges)
                {
                    if (Intersects(rect))
                    {
                        done = true;
                        Bounce(rect);
                    }
                }
            }
            */
            if (_location.X < 10)
                _location.X = edges[1].Right + _radius;
            if (_location.X > 990)
                _location.X = edges[3].X - _radius;
            if (_location.Y < 0)
                _location.Y = edges[2].Bottom + _radius;
            if (_location.Y > 700)
                _location.Y = edges[0].Y - _radius;
            //fix contrarians glitching out of the level!!!!!!
            //still doesnt quite work, program freezes...
        }
    }
}
