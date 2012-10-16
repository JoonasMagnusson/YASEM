using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetAnotherShootEmUp
{
    class Shot : Sprite
    {
        public Vector2 Maxposition;
        public Texture2D[] sprites;
        private int spritenumber = 0;
        public override void LoadContent(ContentManager theContentManager)
        {
            sprites = new Texture2D[3];
            sprites[0] = theContentManager.Load<Texture2D>("missile3");
            sprites[1] = theContentManager.Load<Texture2D>("missile2");
            sprites[2] = theContentManager.Load<Texture2D>("missile1");
            Scale = 0.5f;
            Speed = 500;

        }
        public void Update(GameTime theGameTime)
        {
            base.Update(theGameTime, new Vector2(0, -1));
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            spritenumber++;
            spritenumber %= 9;
            theSpriteBatch.Draw(sprites[spritenumber / 3], Position, new Rectangle(0, 0, sprites[spritenumber / 3].Width, sprites[spritenumber / 3].Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
