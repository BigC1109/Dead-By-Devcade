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
    public class TrialLogic
    {
        public enum gamemode
        {
            NORMAL,
            NOFAIL,
            ONLYGREAT
        }

        public gamemode gamemodeSelected {  get; set; }

        private int genRemaining;

        public TrialLogic(gamemode type)
        {
            this.gamemodeSelected = type;
            this.genRemaining = 5;
        }

        public static void LoadContent(ContentManager contentManager)
        {

        }

        public void Update(GameTime gameTime)
        {
            if (this.gamemodeSelected == gamemode.NORMAL)
            {

            } else if (this.gamemodeSelected == gamemode.NOFAIL)
            {

            } else if (this.gamemodeSelected == gamemode.ONLYGREAT)
            {

            }
        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {

        }
    }
}
