using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherShootEmUp
{
    class Enemies : Sprite
    {
        public List<Enemies> enemies;
        public Spaceship alus;
        public Boolean alive = true;
        public int boomnumber = 0;
        public int boommultiplier = 3;
        public Vector2 direction;
        public Texture2D[] boom;
        public void Update(GameTime theGameTime)
        {
            move();
            base.Update(theGameTime, direction);
        }

        public virtual void move()
        {

        }
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (!alive && boomnumber <6*boommultiplier)
            {
                theSpriteBatch.Draw(boom[boomnumber/boommultiplier], Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
                boomnumber++;
            }
            else theSpriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
