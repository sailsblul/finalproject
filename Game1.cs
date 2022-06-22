using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace cellescape
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont font;
        SpriteFont bigFont;
        Texture2D cellTexture;
        Texture2D circle;
        Texture2D lava;
        MouseState mouseState;
        MouseState oldState;
        KeyboardState keyboardState;
        KeyboardState oldKState;
        Texture2D wallTexture;
        List<Rectangle> borders;
        List<Rectangle> walls = new List<Rectangle>();
        readonly Rectangle gameBounds = new Rectangle(10, 10, 980, 690);
        Level currentLevel;
        LevelManager levelManager;
        Cursor cursor;
        float seconds;
        float timeStamp;
        enum Screen
        {
            Title,
            Level,
            LevelSelect,
            LevelComplete
        }
        Screen screen;
        Texture2D buttonTexture;
        int levelNumber;
        bool record;
        Rectangle nextLevel = new Rectangle(20, 650, 470, 80);
        Rectangle mainMenu = new Rectangle(510, 650, 470, 80);
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 750;
            Window.Title = "Cell Escape";
            _graphics.ApplyChanges();
            base.Initialize();
            borders = new List<Rectangle>
            {
                new Rectangle(0, 700, 1000, 50),
                new Rectangle(0, 0, 10, 700),
                new Rectangle(0, 0, 1000, 10),
                new Rectangle(990, 0, 10, 700)
            };
            walls = borders;
            screen = Screen.Title;
            levelManager = new LevelManager();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            circle = Content.Load<Texture2D>("circle");
            cellTexture = Content.Load<Texture2D>("cell");
            wallTexture = Content.Load<Texture2D>("rectangle");
            font = Content.Load<SpriteFont>("mainfont");
            bigFont = Content.Load<SpriteFont>("bigfont");
            buttonTexture = Content.Load<Texture2D>("buttonicon");
            lava = Content.Load<Texture2D>("magma");
            cursor = new Cursor(new Point(), circle);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            // TODO: Add your update logic here
            if (screen == Screen.Level)
            {
                if (currentLevel.Balls.TrueForAll(x => x.Intersects(currentLevel.Goal)))
                    FinishLevel(seconds);
                bool dead = false;
                foreach (Cell ball in currentLevel.Balls)
                {
                    ball.Move(walls);
                    foreach (Rectangle danger in currentLevel.Dangers)
                        if (ball.Intersects(danger))
                            dead = true;
                }
                if (dead)
                    LoadLevel(levelNumber - 1, gameTime);

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    foreach (Cell ball in currentLevel.Balls)
                    {
                        Vector2 speedChange = (cursor.Position.ToVector2() - ball.Center) / Vector2.Distance(cursor.Position.ToVector2(), ball.Center) * new Vector2((float)0.65);
                        if (ball.Colour == Color.Cyan)
                            ball.Speed -= speedChange;
                        else
                            ball.Speed += speedChange;
                    }
                }
                else
                    if (currentLevel.Objects.TrueForAll(x => !x.Contains(mouseState.Position)))
                    cursor.Position = mouseState.Position;
                foreach (Cell ball in currentLevel.Balls)
                {
                    ball.Speed /= (float)1.05;
                    if (ball.Colour == Color.Magenta && walls.TrueForAll(x => !x.Contains(new Point((int)ball.Center.X, (int)ball.Center.Y + ball.Radius + 1))))
                        ball.Speed += new Vector2(0, (float)0.4);
                }


                if (keyboardState.IsKeyDown(Keys.R) && oldKState.IsKeyUp(Keys.R))
                    LoadLevel(levelNumber - 1, gameTime);
                if (keyboardState.IsKeyDown(Keys.Q) && oldKState.IsKeyUp(Keys.Q))
                    screen = Screen.LevelSelect;

                oldKState = Keyboard.GetState();
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;
            }
            else
            {
                cursor.Position = mouseState.Position;
                if (screen == Screen.Title)
                {
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        screen = Screen.LevelSelect;
                    }
                }
                else if (screen == Screen.LevelSelect)
                {
                    foreach (LevelButton button in levelManager.Buttons)
                        if (button.IsPressed(mouseState))
                            LoadLevel(button.Number - 1, gameTime);
                }
                else if (screen == Screen.LevelComplete)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                        if (nextLevel.Contains(mouseState.Position) && levelNumber < levelManager.Levels.Length)
                            LoadLevel(levelNumber, gameTime);
                        else if (mainMenu.Contains(mouseState.Position))
                            screen = Screen.LevelSelect;
                }
            }
            oldState = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PapayaWhip);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap);
            if (screen == Screen.Level)
            {
                currentLevel.DrawLevel(_spriteBatch, wallTexture, circle, lava);
                for (int i = 0; i < borders.Count; i++)
                    if (i == 0)
                        _spriteBatch.Draw(wallTexture, borders[i], Color.DarkViolet);
                    else
                        _spriteBatch.Draw(wallTexture, borders[i], Color.Black);
                _spriteBatch.DrawString(font, $"Level {levelNumber} - {currentLevel.Name}", new Vector2(10, 710), Color.White);
                _spriteBatch.DrawString(font, seconds.ToString("F1") + "s", new Vector2(990 - font.MeasureString(seconds.ToString("F1") + "s").X, 710), Color.White);
                foreach (Cell ball in currentLevel.Balls)
                    _spriteBatch.Draw(cellTexture, ball.Rect, ball.Colour);
            }
            else if (screen == Screen.Title)
            {
                _spriteBatch.DrawString(bigFont, "Cell Escape!", new Vector2(500 - bigFont.MeasureString("Cell Escape!").X / 2, 30), Color.DarkGray);
                _spriteBatch.DrawString(font, "press enter to start", new Vector2(750, 370), Color.Gray);
            }
            else if (screen == Screen.LevelSelect)
            {
                foreach (LevelButton button in levelManager.Buttons)
                    button.Draw(_spriteBatch, buttonTexture, font);
                _spriteBatch.DrawString(font, "How to play: \n - Hold the mouse button to make the cells move \n - Guide them to the goal (green area) \n - Press R to restart a level \n - Press Q to return to this screen", new Vector2(25, 555), Color.DarkGray);
            }
            else if (screen == Screen.LevelComplete)
            {
                _spriteBatch.DrawString(bigFont, "Level Complete!", new Vector2(500 - bigFont.MeasureString("Level Complete!").X / 2, 30), Color.Teal);
                _spriteBatch.DrawString(font, $"Your time: {seconds:F1}s", new Vector2(20, 250), Color.Gray);
                if (record)
                    _spriteBatch.DrawString(font, "New record!", new Vector2(20, 300), Color.DarkRed);
                else
                    _spriteBatch.DrawString(font, $"Best time: {levelManager.Times[levelNumber - 1]:F1}s", new Vector2(20, 300), Color.DarkRed);
                if (levelNumber < levelManager.Levels.Length)
                {
                    _spriteBatch.Draw(buttonTexture, nextLevel, Color.Aquamarine);
                    _spriteBatch.DrawString(font, "Next level", nextLevel.Center.ToVector2() - font.MeasureString("Next level") / 2, Color.Black);
                }
                _spriteBatch.Draw(buttonTexture, mainMenu, Color.Aquamarine);
                _spriteBatch.DrawString(font, "Level select", mainMenu.Center.ToVector2() - font.MeasureString("Level select") / 2, Color.Black);
            }
            cursor.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        void LoadLevel(int num, GameTime gameTime)
        {
            currentLevel = levelManager.Levels[num];
            currentLevel.Reset();
            levelNumber = num + 1;
            walls = new List<Rectangle>(borders);
            walls.AddRange(currentLevel.Walls);
            screen = Screen.Level;
            timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
        }
        void FinishLevel(float time)
        {
            screen = Screen.LevelComplete;
            if (levelManager.Times[levelNumber - 1] == 0 || time < levelManager.Times[levelNumber - 1])
            {
                record = true;
                levelManager.Times[levelNumber - 1] = time;
            }
            else
                record = false;
        }
    }
}
