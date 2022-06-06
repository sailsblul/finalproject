using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing
{
    class Ball
    {
        Vector2 _location;
        Vector2 _speed;
        Color _color;
        int _radius;
        public Ball(Vector2 location, int radius, Color color)
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
        public Ball Copy()
        {
            return new Ball(_location, _radius, _color);
        }
        public void UndoMove()
        {
            _location -= _speed;
        }
    }
}
