using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherShootEmUp
{
    class SinusEnemy : Enemies
    {
        public int dir = 1;
        public int max_x = 800;
        public int x_offset;
        public int density;
        public override void LoadContent(ContentManager theContentManager)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>("enemy2");
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            Speed = random.Next(300) + 200;
            max_x = 800 - mSpriteTexture.Height;
            x_offset = random.Next(max_x);
            density = random.Next(200) + 200;
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
            Position.X = (float)Math.Sin((x_offset + Position.Y)*(600/Speed) / density) * max_x / 2 + (max_x / 2);
        }
    }
}
