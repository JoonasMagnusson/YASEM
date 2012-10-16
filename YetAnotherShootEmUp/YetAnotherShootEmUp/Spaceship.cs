using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace YetAnotherShootEmUp
{
    class Spaceship : Sprite
    {
        private List<Shot> shots = new List<Shot>();
        public Boolean alive = true;
        public int boomcounter = 0;
        private int cannon = 0;
        public int boommultiplier = 2;
        ContentManager mContentManager;
        private Texture2D[] blowup;
        public Vector2 resolution;
        SoundEffect shotsound;

        public List<Shot> getShots()
        {
            return shots;
        }
        private float Vertical = 0f, Horizontal = 0f;
        private Boolean canShoot = true;
        public void Update(GameTime theGameTime)
        {
            Speed = 400;

            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            UpdateMovement(aCurrentKeyboardState);
//            Console.WriteLine(aCurrentKeyboardState);
  //          Console.WriteLine(Vertical + " " + Horizontal);
            //mPreviousKeyboardState = aCurrentKeyboardState;
            List<Shot> temp = new List<Shot>();
            foreach (Shot aShot in shots)
            {
                if (aShot.Position.Y < -20) temp.Add(aShot);
            }
            foreach (Shot aShot in temp)
            {
                shots.Remove(aShot);
            }
            foreach (Shot aShot in shots)
            {
                aShot.Update(theGameTime);
            }
            Position += (new Vector2(Horizontal, Vertical)* Speed) * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            Position.X = MathHelper.Clamp(Position.X, 0, resolution.X - mSpriteTexture.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, resolution.Y - mSpriteTexture.Height);


        }

        public void removeShot(Shot s)
        {
            shots.Remove(s);
        }
        public override void LoadContent(ContentManager theContentManager)
        {
            shotsound = theContentManager.Load<SoundEffect>("ampu");
            blowup = new Texture2D[30];
            for (int i = 0; i < 30; i++)
            {
                blowup[i] = theContentManager.Load<Texture2D>("isopaukku/isopauk" + (i < 10 ? "0" : "") + i);
            }
            mContentManager = theContentManager;



            foreach (Shot aShot in shots)
            {

                aShot.LoadContent(theContentManager);

            }


            mSpriteTexture = theContentManager.Load<Texture2D>("alus");
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));

        }



        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {

            Vertical = 0f;
            Horizontal = 0f;
            if (!alive) return;
            if (aCurrentKeyboardState.IsKeyDown(Keys.Space))
            {
                if (canShoot)
                {
                    shotsound.CreateInstance().Play();

                    Shot ammus = new Shot();
                    ammus.LoadContent(mContentManager);
                    ammus.Position = Position;
                    ammus.Position.X += 8 + cannon*60;
                    ammus.Maxposition = new Vector2(1600, 1200);

                    shots.Add(ammus);
                    canShoot = false;
                    cannon = 1 - cannon;
                }
            }
            else canShoot = true;
                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {

                    Horizontal = -1f;

                }

                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {

                    Horizontal = 1f;

                }



                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                {


                    Vertical = -1f;
                }

                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                {

                    Vertical = 1f;

                }

        }
        public override void Draw(SpriteBatch theSpriteBatch)
        {

            foreach (Shot aShot in shots)
            {

                aShot.Draw(theSpriteBatch);

            }

            if (!alive && boomcounter < 30 * boommultiplier)
            {
                Vector2 Boomposition = Position;
                Boomposition.X = Boomposition.X - (float)(blowup[boomcounter / boommultiplier].Width * 0.5 - mSpriteTexture.Width * 0.5);
                Boomposition.Y = Boomposition.Y - (float)(blowup[boomcounter / boommultiplier].Height * 0.5 - mSpriteTexture.Height * 0.5);
                theSpriteBatch.Draw(blowup[boomcounter / boommultiplier], Boomposition, new Rectangle(0, 0, blowup[boomcounter / boommultiplier].Width, blowup[boomcounter / boommultiplier].Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
                boomcounter++;
            }
            else if (alive) theSpriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);

        }




    }
}
