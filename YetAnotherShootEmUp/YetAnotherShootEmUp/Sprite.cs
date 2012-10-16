using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherShootEmUp
{
    class Sprite
    {
        //The size of the Sprite

        public Rectangle Size;
        public Random random;


        //Used to size the Sprite up or down from the original image

        public float Scale = 1.0f;
        public float Speed = 1.0f;

        //The current position of the Sprite
        public Vector2 Position;
        
        //The texture object used when drawing the sprite
        public Texture2D mSpriteTexture;
        

        //Load the texture for the sprite using the Content Pipeline
        public virtual void LoadContent(ContentManager theContentManager)
        {
            
            //mSpriteTexture = theContentManager.Load<Texture2D>();

            //Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }
        public void Update(GameTime theGameTime, Vector2 theDirection)
        {
            Position += (theDirection * Speed) * (float)theGameTime.ElapsedGameTime.TotalSeconds;

        }
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
