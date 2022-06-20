using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace cellescape
{
    class Level
    {
        private List<Rectangle> _walls;
        private List<Cell> _balls;
        private List<Cell> _startingBalls;
        private Rectangle _goal;
        private string _name;
        private List<Rectangle> _dangers;
        public Level(string name, List<Rectangle> walls, List<Cell> balls, Rectangle goal)
        {
            _name = name;
            _walls = walls;
            _startingBalls = balls;
            _goal = goal;
            _dangers = new List<Rectangle>();
        }
        public Level(string name, List<Rectangle> walls, List<Cell> balls, Rectangle goal, List<Rectangle> dangers)
        {
            _name = name;
            _walls = walls;
            _startingBalls = balls;
            _goal = goal;
            _dangers = dangers;
        }
        public List<Rectangle> Walls
        {
            get { return _walls; }
        }
        public List<Cell> Balls
        {
            get { return _balls; }
        }
        public Rectangle Goal
        {
            get { return _goal; }
        }
        public string Name
        {
            get { return _name; }
        }
        public List<Rectangle> Objects
        {
            get
            {
                List<Rectangle> objs = new List<Rectangle>(Walls)
                {
                    _goal
                };
                objs.AddRange(Dangers);
                return objs;
            }
        }
        public List<Rectangle> Dangers
        {
            get { return _dangers; }
        }
        List<Rectangle> SmallWalls
        {
            get
            {
                List<Rectangle> smalls = new List<Rectangle>();
                foreach (Rectangle wall in _walls)
                    if (wall.Width <= 20 || wall.Height <= 20)
                        smalls.Add(wall);
                return smalls;
            }
        }
        List<Rectangle> BigWalls
        {
            get
            {
                List<Rectangle> bigs = new List<Rectangle>();
                foreach (Rectangle wall in _walls)
                    if (wall.Width > 20 && wall.Height > 20)
                        bigs.Add(wall);
                return bigs;
            }
        }
        public void DrawLevel(SpriteBatch spriteBatch, Texture2D rectTexture, Texture2D circle, Texture2D lava)
        {
            foreach (Rectangle wall in SmallWalls)
                spriteBatch.Draw(rectTexture, wall, Color.Black);
            foreach (Rectangle wall in BigWalls)
            {
                spriteBatch.Draw(rectTexture, new Rectangle(wall.X, wall.Y + 10, wall.Width, wall.Height - 20), Color.Black);
                spriteBatch.Draw(rectTexture, new Rectangle(wall.X + 10, wall.Y, wall.Width - 20, wall.Height), Color.Black);
                spriteBatch.Draw(circle, new Rectangle(wall.Location, new Point(20)), Color.Black);
                spriteBatch.Draw(circle, new Rectangle(wall.Location + wall.Size - new Point(20), new Point(20)), Color.Black);
                spriteBatch.Draw(circle, new Rectangle(wall.X, wall.Bottom - 20, 20, 20), Color.Black);
                spriteBatch.Draw(circle, new Rectangle(wall.Right - 20, wall.Y, 20, 20), Color.Black);
            }
            foreach (Rectangle wall in BigWalls)
                spriteBatch.Draw(rectTexture, new Rectangle(wall.Location + new Point(10), wall.Size - new Point(20)), new Color(45, 45, 45));

            foreach (Rectangle danger in _dangers)
                spriteBatch.Draw(lava, new Vector2(danger.X, danger.Y), new Rectangle(0, 0, danger.Width, danger.Height), Color.White);
            spriteBatch.Draw(rectTexture, _goal, Color.LimeGreen);
        }
        public void Reset()
        {
            _balls = new List<Cell>();
            foreach (Cell ball in _startingBalls)
                _balls.Add(ball.Copy());
        }
    }
}
