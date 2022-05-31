using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace testing
{
    class LevelManager
    {
        Level[] _levels;
        int _levelNumber = 0;
        public LevelManager()
        {
            _levels = new Level[]
            {
                new Level("Click to move", new List<Rectangle>()
                {
                    new Rectangle(400, 0, 20, 300)
                }, new List<Ball>()
                {
                    new Ball(new Vector2(30, 30), 10, Color.Red),
                }, new Rectangle(950, 10, 40, 20)),

                new Level("Two of them...", new List<Rectangle>()
                {
                    new Rectangle(0, 0, 450, 300),
                    new Rectangle(550, 0, 450, 300),
                    new Rectangle(0, 400, 1000, 300)
                }, new List<Ball>()
                {
                    new Ball(new Vector2(60, 350), 10, Color.Red),
                    new Ball(new Vector2(940, 350), 10, Color.Red)
                }, new Rectangle(450, 10, 100, 50)),

                new Level("Danger!!!!", new List<Rectangle>()
                {
                    new Rectangle(0, 0, 320, 500),
                    new Rectangle(680, 0, 320, 500)
                }, new List<Ball>()
                {
                    new Ball(new Vector2(50, 670), 10, Color.Red)
                }, new Rectangle(930, 640, 60, 60), new List<Rectangle>(){ 
                    new Rectangle(475, 90, 50, 610)
                }),

                new Level("some ball will fall", new List<Rectangle>()
                {
                    new Rectangle(0, 60, 950, 30),
                    new Rectangle(950, 250, 50, 450),
                    new Rectangle()
                }, new List<Ball>()
                {
                    new Ball(new Vector2(20, 690), 10, Color.Blue)
                }, new Rectangle(10, 10, 50, 50)),
            };
        }
        public Level[] Levels
        {
            get { return _levels; }
        }
        public int LevelNumber
        {
            get { return _levelNumber; }
            set { _levelNumber = value; }
        }
    }
}
/* new Level("", new List<Rectangle>()
                {
                    new Rectangle()
                }, new List<Ball>()
                {
                    new Ball(new Vector2(), 10, Color.Red)
                }, new Rectangle()),
*/