using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dead_By_Devcade
{
    public class SkillCheckLogic
    {
        public enum Result {
            GOOD,
            GREAT,
            FAIL
        }

        private Texture2D ring;
        private Texture2D red;
        private Texture2D skill;


        private bool active { get; set; }
        private Vector2 curPostion;
        private Random RNG = new Random();

        public SkillCheckLogic()
        {
            active = false;
        }

        public void SkillCheck()
        {
            active = true;
        }

        public void LoadContent(ContentManager contentManager)
        {
            ring = contentManager.Load<Texture2D>("Ring");
            red = contentManager.Load<Texture2D>("Red");
            skill = contentManager.Load<Texture2D>("Skill");
        }

        public void Update(GameTime gameTime)
        {
            if (active == true)
            {

            } else
            {
                int val = RNG.Next(0, 360);
                curPostion = new Vector2();
            }
        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            if (active == true)
            {
                sb.Draw(ring, new Vector2(windowSize.Center.X / 2f, windowSize.Center.Y / 2f), null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
                int val = RNG.Next(0, 360);
                sb.Draw(skill, new Vector2(
                    (float)(((windowSize.Center.X / 2f) + (ring.Height / 4f)) + ((ring.Height / 4f) - 10)* (Math.Cos(val))),
                    (float)(((windowSize.Center.Y / 2f) + (ring.Width / 4f)) + ((ring.Width / 4f) - 10)* (Math.Sin(val)))),
                    null, 
                    Color.White, 
                    val + 119.9f, 
                    Vector2.Zero, 
                    0.5f, 
                    SpriteEffects.FlipHorizontally, 
                    0);
            }
        }
    }
}
