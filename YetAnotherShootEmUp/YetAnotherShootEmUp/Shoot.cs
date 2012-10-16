using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherShootEmUp
{
    class Shoot : Enemies
    {
        public Shoot(float speed, Vector2 direction)
        {
            this.Speed = speed;
            this.direction = direction;
        }
        public override void LoadContent(ContentManager theContentManager)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>("fireball");
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            boom = new Texture2D[6];
            boom[0] = theContentManager.Load<Texture2D>("pum1");
            boom[1] = theContentManager.Load<Texture2D>("pum2");
            boom[2] = theContentManager.Load<Texture2D>("pum3");
            boom[3] = theContentManager.Load<Texture2D>("pum4");
            boom[4] = theContentManager.Load<Texture2D>("pum5");
            boom[5] = theContentManager.Load<Texture2D>("pum6");


        }
        public override void move()
        {
        }
        public override void Draw(SpriteBatch theSpriteBatch)
        { 
            if(alive) theSpriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, (float)Math.Atan2(direction.X, -1*direction.Y), new Vector2(mSpriteTexture.Width/2, mSpriteTexture.Height/2), Scale, SpriteEffects.None, 0);
        }
    }
}
