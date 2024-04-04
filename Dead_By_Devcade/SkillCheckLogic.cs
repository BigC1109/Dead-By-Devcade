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
        private float SkillLocation;
        private float RedLocation;
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
                if (RedLocation < SkillLocation + 0.945f)
                {
                    RedLocation += 0.08f;
                }
            } else
            {
                SkillLocation = RNG.Next(-500, 3000) * 0.001f; // Lowest Possible Value: -0.5, Max Value: 3.00
                RedLocation = -1.7f;
            }
        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            if (active == true)
            {
                sb.Draw(ring, new Vector2(windowSize.Center.X / 2f, 
                    windowSize.Center.Y / 2f), 
                    null, 
                    Color.White, 
                    0, 
                    Vector2.Zero, 
                    0.5f, 
                    SpriteEffects.None, 
                    0);
                sb.Draw(skill, new Vector2(
                    (float)(((windowSize.Center.X / 2f) + (ring.Height / 4f)) + ((ring.Height / 4f) - 10)* (Math.Cos(SkillLocation))),
                    (float)(((windowSize.Center.Y / 2f) + (ring.Width / 4f)) + ((ring.Width / 4f) - 10)* (Math.Sin(SkillLocation)))),
                    null, 
                    Color.White, 
                    SkillLocation + 119.97f, 
                    Vector2.Zero, 
                    0.5f, 
                    SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 
                    0);
                sb.Draw(red, new Vector2(
                    (float)(((windowSize.Center.X / 2f) + (ring.Height / 4f)) + ((ring.Height / 4f) + 55) * (Math.Cos(RedLocation))),
                    (float)(((windowSize.Center.Y / 2f) + (ring.Width / 4f)) + ((ring.Width / 4f) + 55) * (Math.Sin(RedLocation)))),
                    null,
                    Color.White,
                    RedLocation + 1.65f,
                    Vector2.Zero,
                    0.45f,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
