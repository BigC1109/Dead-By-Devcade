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
                curPostion = new Vector2()
            }
        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            if (active == true)
            {
                sb.Draw(ring, new Vector2(windowSize.Center.X / 2, windowSize.Center.Y / 2), null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
                sb.Draw(skill, new Vector2((windowSize.Center.X + (ring.Width/2)/ 2) - (), ((windowSize.Center.Y + (ring.Height / 2)) / 2)), null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            }
        }
    }
}
