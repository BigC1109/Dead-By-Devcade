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
    public class GeneratorLogic
    {
        public enum State
        {
            Complete,
            Incomplete,
            Regressing // Might not get to use this, but maybe in the future!
        }

        public State state;

        private float progress;
        private SkillCheckLogic skillie;


        public GeneratorLogic()
        {
            this.state = State.Incomplete;
            this.skillie = new SkillCheckLogic();
        }

        public void LoadContent(ContentManager contentManager)
        {

        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            //sb.DrawUserPrimatives(PrimitiveType.LineStrip, new Rectangle(new Point(), new Point()), null, Color.DarkGray);
        }


    }
}
