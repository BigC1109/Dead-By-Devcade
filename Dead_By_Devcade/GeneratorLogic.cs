using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
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

        private Texture2D underBar;
        private Texture2D loading;

        private float progress;
        private SkillCheckLogic skillie;


        public GeneratorLogic()
        {
            this.state = State.Incomplete;
            this.skillie = new SkillCheckLogic();
        }

        public void LoadContent(ContentManager contentManager)
        {
            underBar = contentManager.Load<Texture2D>("UnderBar");
            loading = contentManager.Load<Texture2D>("Loading");
        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            sb.Draw(underBar, new Vector2(
                windowSize.Left + 10,
                windowSize.Bottom - underBar.Height),
                null,
                Color.White,
                0,
                Vector2.Zero,
                0.207f,
                SpriteEffects.None,
                0);
            sb.Draw(loading, new Vector2(
                windowSize.Left + 10,
                windowSize.Bottom - underBar.Height),
                null,
                Color.White,
                0,
                Vector2.Zero,
                0.207f,
                SpriteEffects.None,
                0);
        }


    }
}
