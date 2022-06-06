using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing
{
    class Level
    {
        List<Rectangle> _walls;
        List<Ball> _balls;
        List<Ball> _startingBalls;
        Rectangle _goal;
        string _name;
        List<Rectangle> _dangers;
        public Level(string name, List<Rectangle> walls, List<Ball> balls, Rectangle goal)
        {
            _name = name;
            _walls = walls;
            _startingBalls = balls;
            _goal = goal;
            _dangers = new List<Rectangle>();
        }
        public Level(string name, List<Rectangle> walls, List<Ball> balls, Rectangle goal, List<Rectangle> dangers)
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
        public List<Ball> Balls
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
        public void DrawLevel(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Rectangle wall in _walls)
                spriteBatch.Draw(texture, wall, Color.Black);
            foreach (Rectangle danger in _dangers)
                spriteBatch.Draw(texture, danger, Color.DarkOrange);
            spriteBatch.Draw(texture, _goal, Color.LimeGreen);
        }
        public void Reset()
        {
            _balls = new List<Ball>();
            foreach (Ball ball in _startingBalls)
                _balls.Add(ball.Copy());
        }
    }
}
