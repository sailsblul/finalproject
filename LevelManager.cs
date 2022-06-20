using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace cellescape
{
    class LevelManager
    {
        private List<LevelButton> _buttons = new List<LevelButton>();
        private Level[] _levels;
        private float[] _bestTimes;
        public LevelManager()
        {
            _levels = new Level[]
            {
                new Level("Click to move", new List<Rectangle>()
                {
                    new Rectangle(400, 0, 20, 300)
                }, new List<Cell>()
                {
                    new Cell(new Vector2(30, 30), 10, Color.Red),
                }, new Rectangle(950, 10, 40, 20)),

                new Level("Two of them...", new List<Rectangle>()
                {
                    new Rectangle(0, 0, 450, 300),
                    new Rectangle(550, 0, 450, 300),
                    new Rectangle(0, 400, 1000, 300)
                }, new List<Cell>()
                {
                    new Cell(new Vector2(60, 350), 10, Color.Red),
                    new Cell(new Vector2(940, 350), 10, Color.Red)
                }, new Rectangle(450, 10, 100, 50)),

                new Level("Danger!!!!", new List<Rectangle>()
                {
                    new Rectangle(0, 0, 320, 500),
                    new Rectangle(680, 0, 320, 500)
                }, new List<Cell>()
                {
                    new Cell(new Vector2(50, 670), 10, Color.Red)
                }, new Rectangle(930, 640, 60, 60), new List<Rectangle>(){
                    new Rectangle(475, 90, 50, 610)
                }),

                new Level("Cell wall? more like cell FALL", new List<Rectangle>()
                {
                    new Rectangle(0, 120, 800, 50),
                    new Rectangle(800, 400, 200, 350),
                    new Rectangle(400, 500, 200, 250)
                }, new List<Cell>()
                {
                    new Cell(new Vector2(20, 690), 10, Color.Magenta)
                }, new Rectangle(10, 10, 50, 70)),

                new Level("Getting harder now :)", new List<Rectangle>()
                {
                    new Rectangle(465, 665, 10, 35),
                    new Rectangle(525, 665, 10, 35),
                    new Rectangle(60, 400, 50, 20),
                    new Rectangle(160, 400, 50, 20),
                    new Rectangle(790, 400, 50, 20),
                    new Rectangle(890, 400, 50, 20),
                }, new List<Cell>()
                {
                    new Cell(new Vector2(85, 390), 10, Color.Magenta),
                    new Cell(new Vector2(915 ,390), 10, Color.Magenta)
                }, new Rectangle(475, 670, 50, 30), new List<Rectangle>()
                {
                    new Rectangle(10, 670, 455, 30),
                    new Rectangle(535, 670, 455, 30),
                }),

                new Level("Mindless contrarian", new List<Rectangle>()
                {
                    new Rectangle(90, 0, 350, 300),
                    new Rectangle(90, 400, 200, 300),
                    new Rectangle(700, 90, 210, 630),
                    new Rectangle(390, 0, 220, 420),
                    new Rectangle(90, 510, 650, 200)
                }, new List<Cell>()
                {
                    new Cell(new Vector2(40), 10, Color.Red),
                    new Cell(new Vector2(40, 650), 10, Color.Cyan)
                }, new Rectangle(910, 650, 80, 80)),

                new Level("Minor inconvenience", new List<Rectangle>()
                {
                    new Rectangle(920, 640, 20, 60),
                    new Rectangle(250, 520, 60, 20),
                    new Rectangle(50, 330, 60, 20),
                    new Rectangle(450, 290, 60, 20),
                    new Rectangle(650, 450, 60, 20),
                }, new List<Cell>()
                {
                    new Cell(new Vector2(280, 150), 10, Color.Cyan),
                    new Cell(new Vector2(280, 510), 10, Color.Magenta),
                }, new Rectangle(940, 650, 50, 50), new List<Rectangle>(){
                    new Rectangle(10, 10, 980, 30),
                    new Rectangle(10, 670, 910, 30)
                }),
            };


            for (int i = 0; i < Levels.Length; i++)
                _buttons.Add(new LevelButton(i + 1, new Rectangle(new Point(250 * (i % 4) + 25, 250 * (i / 4) + 25), new Point(200))));
            _bestTimes = new float[_levels.Length];
        }
        public Level[] Levels
        {
            get { return _levels; }
        }
        public List<LevelButton> Buttons
        {
            get { return _buttons; }
        }
        public float[] Times
        {
            get { return _bestTimes; }
            set { _bestTimes = value; }
        }
    }
}
/* new Level("", new List<Rectangle>()
                {
                    new Rectangle(),
                }, new List<Ball>()
                {
                    new Ball(new Vector2(), 10, Color.Red),
                }, new Rectangle()),
*/
