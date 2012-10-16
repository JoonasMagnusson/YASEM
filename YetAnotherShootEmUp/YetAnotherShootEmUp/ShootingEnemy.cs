using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherShootEmUp
{
    class ShootingEnemy : Enemies
    {
        private ContentManager theContentManager;
        public override void LoadContent(ContentManager theContentManager)
        {
            this.theContentManager = theContentManager;
            mSpriteTexture = theContentManager.Load<Texture2D>("enemy3");
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            Speed = random.Next(400) + 200;
            direction = new Vector2(0, 1);
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
            if (random.NextDouble() < 0.01)
            {
                Vector2 direction = alus.Position - Position;
                direction.Normalize();
                Shoot shoot = new Shoot(Speed * 1.5f, direction);
                shoot.Position = Position + new Vector2(mSpriteTexture.Height/2 +3, mSpriteTexture.Width);
                shoot.Scale = 0.5f;
                shoot.LoadContent(theContentManager);
                enemies.Add(shoot);
                
            }
            Vector2 x = alus.Position - Position;
            x.Normalize();
            if(alus.alive) Position.X += x.X * 5;


        }
    }
}
