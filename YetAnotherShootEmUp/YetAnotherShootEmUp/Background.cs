using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherShootEmUp
{
    class Background
    {
        //The size of the Sprite

        public Rectangle Size;



        //Used to size the Sprite up or down from the original image

        public float Scale = 1.0f;
        

        //The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Maxposition = new Vector2(0, 0);
        //The texture object used when drawing the sprite
        public Texture2D mSpriteTexture;

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }
        public void Update(float deltaY)
        {
            Position.Y += deltaY;
            Position.Y = Position.Y % mSpriteTexture.Height;
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            {
                Vector2 newpos = Position;

                for (int j = (int)Position.Y - mSpriteTexture.Height; j < Maxposition.Y; j += mSpriteTexture.Height)
                {
                    newpos.Y = j;
                    for (int i = 0; i < Maxposition.X; i += mSpriteTexture.Width)
                    {
                        newpos.X = i;
                        theSpriteBatch.Draw(mSpriteTexture, newpos, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
                    }
                }
            }
        }
    }
}
