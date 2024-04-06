using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            COMPLETE,
            INCOMPLETE,
            REGRESSING // Might not get to use this, but maybe in the future!
        }

        public State state;

        private Texture2D underBar;
        private Texture2D loading;

        private Random RNG = new Random();
        private int genCompleteTime;
        private float progress;
        private float testProgress; // Rename this to replace progress (if it works)
        private int progressTime;
        private Rectangle progressImage;
        private SkillCheckLogic skillie = new SkillCheckLogic();


        public GeneratorLogic()
        {
            this.state = State.INCOMPLETE;
            this.genCompleteTime = 60; // Total amount of time to complete the gen.
            this.progress = 0;
            this.progressTime = 0;
        }

        public void LoadContent(ContentManager contentManager)
        {
            underBar = contentManager.Load<Texture2D>("UnderBar");
            loading = contentManager.Load<Texture2D>("Loading");
            this.progressImage = new Rectangle(0, 0, loading.Width, loading.Height);
        }

        public void Update(GameTime gametime)
        {
            skillie.Update(gametime);

            if (this.state == State.INCOMPLETE)
            {

            }

            progressImage.Width = (int)(progress / genCompleteTime * loading.Width);
            if (progress <= genCompleteTime && gametime.TotalGameTime.Seconds > progressTime)
            {
                progressImage.Width = (int)(progress / genCompleteTime * loading.Width);
                progress++;
                progressTime = gametime.TotalGameTime.Seconds + 1;

                //Debug.Write(RNG.Next(0, 7));

                if (2 == 2)
                {
                    skillie.SkillCheck();
                }

            } else if (progress >= genCompleteTime) 
            {
                progressImage.Width = loading.Width;
                this.state = State.COMPLETE;
            }
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
            if (progress != 0)
            {
                sb.Draw(loading, new Vector2(
                    windowSize.Left + 10,
                    windowSize.Bottom - underBar.Height),
                    progressImage,
                    Color.White,
                    0,
                    Vector2.Zero,
                    0.207f,
                    SpriteEffects.None,
                    0);
            }
            skillie.Draw(sb, windowSize);
        }


    }
}
