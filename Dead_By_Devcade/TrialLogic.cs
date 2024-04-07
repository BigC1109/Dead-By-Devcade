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
        private GeneratorLogic currentGenerator;

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
            if (currentGenerator == null || currentGenerator.state == GeneratorLogic.State.COMPLETE)
            {
                currentGenerator = new GeneratorLogic();
                genRemaining--;
            }

            if (this.gamemodeSelected == gamemode.NORMAL)
            {
                // Doesnt fail, only shows win screen when all gens done
            } else if (this.gamemodeSelected == gamemode.NOFAIL)
            {
                // Can fail, shows lose screen if a failed skill check occurs
            } else if (this.gamemodeSelected == gamemode.ONLYGREAT)
            {
                // Can fail, shows a lose screen if a good or failed skill check occurs
            }
        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            // Show amount of generators left
        }
    }
}