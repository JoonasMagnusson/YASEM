using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace YetAnotherShootEmUp 
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        SpriteFont font;
        public Random random;
        Spaceship alus;
        Background background;
        List<Enemies> enemies, newenemies;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int spawnrate;
        private int points;
        private Boolean paused, escpressed;
        Menu menu;
        Rectangle mSelectionBox;
        MouseState mPreviousMouseState;
        Song song;
                SoundEffect enemydying, death;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            spawnrate = 0;
            enemydying = Content.Load<SoundEffect>("posa");
            death = Content.Load<SoundEffect>("nyyh");
            points = 0;
            paused = false;
            // TODO: Add your initialization logic here
            random = new Random();
            alus = new Spaceship();
            alus.Scale = 1.0f;
            alus.resolution = new Vector2(GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Width);
            background = new Background();
            background.Scale = 1.0f;

            enemies = new List<Enemies>();
            newenemies = new List<Enemies>();
            mSelectionBox = new Rectangle(-1, -1, 0, 0);
            mPreviousMouseState = Mouse.GetState();

          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            if (song == null)
            {
                song = Content.Load<Song>("music");
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;

            }

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("myFont");
            // TODO: use this.Content to load your game content here
            alus.LoadContent(this.Content);
            alus.Position.X = (GraphicsDevice.Viewport.Width-86) / 2;
            alus.Position.Y = GraphicsDevice.Viewport.Height - 82;

            background.LoadContent(this.Content, "background");
            background.Position.X = 0f;
            background.Position.Y = 0f;
            background.Maxposition.Y = GraphicsDevice.Viewport.Height;
            background.Maxposition.X = GraphicsDevice.Viewport.Width;
            menu = new Menu(this.Content);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();
            menu.setHilight(new Vector2(aMouse.X, aMouse.Y));
            if (paused && aMouse.LeftButton == ButtonState.Pressed && mPreviousMouseState.LeftButton == ButtonState.Released)
            {
                int menuselection = menu.menuSelection(new Vector2(aMouse.X, aMouse.Y));
                if (menuselection == 0)
                {
                    this.Initialize();
                    this.IsMouseVisible = false;
                    paused = false;
                }
                else if (menuselection == 3)
                {
                    this.Exit();
                }
                //mSelectionBox = new Rectangle(aMouse.X, aMouse.Y, 0, 0);
            }
            if (paused && aMouse.LeftButton == ButtonState.Released)
            {
                
                //Reset the selection square to no position with no height and width
                //mSelectionBox = new Rectangle(-1, -1, 0, 0);
            }
            mPreviousMouseState = aMouse;


            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            if (escpressed && aCurrentKeyboardState.IsKeyUp(Keys.Escape))
            {
                escpressed = false;

            }
            if (!escpressed && aCurrentKeyboardState.IsKeyDown(Keys.Escape))
            {
                if (paused && !alus.alive)
                {
                }
                else
                {
                    escpressed = true;
                    paused = !paused;
                    this.IsMouseVisible = paused;
                }

            }
            else if (aCurrentKeyboardState.IsKeyDown(Keys.R))
            {
                Initialize();
            }

            if (paused) return;
            spawnrate++;
            if (alus.boomcounter >= 30 * alus.boommultiplier)
            {
                endGame();
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (random.NextDouble() < (float)spawnrate/(float)(20000+spawnrate))
            {
                Double d = random.NextDouble();
                Enemies enemy;
                if (d < 0.2)
                {
                    enemy = new SinusEnemy();
                    enemy.random = random;
                    enemy.LoadContent(this.Content);
                    enemy.Position.Y = -50;
                    enemy.Position.X = random.Next(0, GraphicsDevice.Viewport.Width - enemy.mSpriteTexture.Width);
                    enemies.Add(enemy);
                }
                else if (d < 0.9)
                {
                    enemy = new BasicEnemy();
                    enemy.random = random;
                    enemy.LoadContent(this.Content);
                    enemy.Position.Y = -50;
                    enemy.Position.X = random.Next(0, GraphicsDevice.Viewport.Width - enemy.mSpriteTexture.Width);
                    enemies.Add(enemy);
                }
                else if (d >= 0.9)
                {
                    enemy = new ShootingEnemy();
                    enemy.alus = alus;
                    enemy.enemies = newenemies;
                    enemy.random = random;
                    enemy.LoadContent(this.Content);
                    enemy.Position.Y = -50;
                    enemy.Position.X = random.Next(0, GraphicsDevice.Viewport.Width - enemy.mSpriteTexture.Width);
                    enemies.Add(enemy);
                }

            }
            List<Enemies> enemiesdelete = new List<Enemies>();
            List<Shot> shots = alus.getShots();
            Rectangle alusr = new Rectangle((int)alus.Position.X+30, (int)alus.Position.Y, alus.mSpriteTexture.Width-60, alus.mSpriteTexture.Height);
            foreach (Enemies e in enemies)
            {
                if (!e.alive)
                {
                    if (e.boomnumber >= 6*e.boommultiplier) enemiesdelete.Add(e);
                    e.Update(gameTime);
                }
                else
                {
                    Rectangle enemyr = new Rectangle((int)e.Position.X, (int)e.Position.Y, e.mSpriteTexture.Width, e.mSpriteTexture.Height);
                    if (enemyr.Intersects(alusr))
                    {
                        Color[] enemydata = new Color[e.mSpriteTexture.Width * e.mSpriteTexture.Height];
                        e.mSpriteTexture.GetData(enemydata);
                        Color[] alusdata = new Color[alus.mSpriteTexture.Width * alus.mSpriteTexture.Height];
                        alus.mSpriteTexture.GetData(alusdata);
                        if (IntersectPixels(enemyr, enemydata, alusr, alusdata))
                        {
                            //enemydying.CreateInstance().Play();
                            if(alus.alive) death.CreateInstance().Play();
                            alus.alive = false;
                            e.alive = false;

                        }

                    }
                    e.Update(gameTime);
                    if (e.Position.Y > GraphicsDevice.Viewport.Height + 10) enemiesdelete.Add(e);
                    foreach (Shot shot in shots)
                    {
                        Rectangle shotr = new Rectangle((int)shot.Position.X, (int)shot.Position.Y, shot.sprites[0].Width, shot.sprites[0].Height);
                        if (shotr.Intersects(enemyr))
                        {
                            Color[] enemydata = new Color[e.mSpriteTexture.Width * e.mSpriteTexture.Height];
                            e.mSpriteTexture.GetData(enemydata);
                            Color[] shotdata = new Color[shot.sprites[0].Width * shot.sprites[0].Height];
                            shot.sprites[0].GetData(shotdata);

                            if (IntersectPixels(shotr, enemydata, enemyr, enemydata))
                            {
                                points = points + 10;
                                enemydying.CreateInstance().Play();
                                e.alive = false;
                                alus.removeShot(shot);
                                break;
                            }

                        }
                    }
                }
            }
            foreach (Enemies e in newenemies) enemies.Add(e);
            newenemies.Clear();
            foreach (Enemies e in enemiesdelete)
            {
                enemies.Remove(e);
            }
            alus.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            background.Draw(this.spriteBatch);
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your game logic here.
            background.Update(elapsed * 100);


            foreach (Enemies e in enemies)
            {
                e.Draw(this.spriteBatch);
            }
            alus.Draw(this.spriteBatch);
            spriteBatch.DrawString(font, "Points: " + this.points + " rate: " + (float)spawnrate/(float)(spawnrate+5000), new Vector2(5, 5), Color.White);
            if (paused)
            {
                menu.Draw(spriteBatch);


            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                            Rectangle rectangleB, Color[] dataB)
        {
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }
        public void endGame()
        {
            paused = true;
            this.IsMouseVisible = true;
        }
    }
}
