using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace testing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont font;
        Texture2D circleTexture;
        MouseState mouseState;
        KeyboardState keyboardState;
        KeyboardState oldKState;
        Texture2D wallTexture;
        List<Rectangle> borders;
        List<Rectangle> walls = new List<Rectangle>();
        Level currentLevel;
        LevelManager levelManager;
        Cursor cursor = new Cursor(new Point(0));

        
        enum Screen
        {
            Title,
            Level,
            LevelSelect
        }
        Screen screen;
        Texture2D buttonTexture;
        int levelNumber;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 750;
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
            circleTexture = Content.Load<Texture2D>("circle");
            wallTexture = Content.Load<Texture2D>("rectangle");
            font = Content.Load<SpriteFont>("mainfont");
            buttonTexture = Content.Load<Texture2D>("buttonicon");
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
                    if (levelNumber < levelManager.Levels.Length)
                        LoadLevel(levelNumber);
                    else
                        screen = Screen.Title;
                bool dead = false;
                foreach (Ball ball in currentLevel.Balls)
                {
                    ball.Move();
                    foreach (Rectangle wall in walls)
                        if (ball.Intersects(wall))
                            ball.Bounce(wall);
                    foreach (Rectangle danger in currentLevel.Dangers)
                        if (ball.Intersects(danger))
                        {
                            dead = true;
                        }
                    if (ball.Center.Y > borders[0].Bottom)
                        ball.UndoMove();

                }
                if (dead)
                    LoadLevel(levelNumber - 1);
                
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    foreach (Ball ball in currentLevel.Balls)
                    {
                        Vector2 speedChange = (cursor.Position.ToVector2() - ball.Center) / Vector2.Distance(cursor.Position.ToVector2(), ball.Center) * new Vector2((float)0.65);
                        if (ball.Colour == Color.Cyan)
                            ball.Speed -= speedChange;
                        else
                            ball.Speed += speedChange;
                    }
                }
                else
                    if(currentLevel.Objects.TrueForAll(x => !x.Contains(mouseState.Position)))
                        cursor.Position = mouseState.Position;
                foreach (Ball ball in currentLevel.Balls)
                {
                    ball.Speed /= (float)1.05;
                    if (ball.Colour == Color.Blue && walls.TrueForAll(x => !x.Contains(new Point((int)ball.Center.X, (int)ball.Center.Y + ball.Radius + 1))))
                        ball.Speed += new Vector2(0, (float)0.5);
                }

                
                if (keyboardState.IsKeyDown(Keys.R) && oldKState.IsKeyUp(Keys.R))
                    LoadLevel(levelNumber - 1);

                oldKState = Keyboard.GetState();
            }
            else if (screen == Screen.Title)
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
                        LoadLevel(button.Number - 1);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PapayaWhip);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Level)
            {
                currentLevel.DrawLevel(_spriteBatch, wallTexture);
                for (int i = 0; i < borders.Count; i++)
                    if (i == 0)
                        _spriteBatch.Draw(wallTexture, borders[i], Color.DarkViolet);
                    else
                        _spriteBatch.Draw(wallTexture, borders[i], Color.Black);
                _spriteBatch.DrawString(font, $"Level {levelNumber} - {currentLevel.Name}", new Vector2(10, 710), Color.White);
                foreach (Ball ball in currentLevel.Balls)
                    _spriteBatch.Draw(circleTexture, ball.Rect, ball.Colour);
                _spriteBatch.Draw(circleTexture, cursor.Rect, Color.Plum);
            }
            else if (screen == Screen.Title)
            {
                _spriteBatch.DrawString(font, "help what do i call this", new Vector2(30, 30), Color.DarkGray);
            }
            else if (screen == Screen.LevelSelect)
            {
                foreach (LevelButton button in levelManager.Buttons)
                    button.Draw(_spriteBatch, buttonTexture, font);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        void LoadLevel(int num)
        {
            currentLevel = levelManager.Levels[num];
            currentLevel.Reset();
            levelNumber = num + 1;
            walls = new List<Rectangle>(borders);
            walls.AddRange(currentLevel.Walls);
            screen = Screen.Level;
        }
    }
}
