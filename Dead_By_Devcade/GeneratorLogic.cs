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
        private Rectangle progressImage;

        private Random RNG = new Random();
        private int genCompleteTime;
        private float genProgress; // This value is in milliseconds, divide by 1000 to get total amount
        private int progressTime;
        private SkillCheckLogic skillie = new SkillCheckLogic();

        private double elapsedSkillieTime;
        private bool allowSkillie;


        public GeneratorLogic()
        {
            this.state = State.INCOMPLETE;
            this.genCompleteTime = 60; // Total amount of time to complete the gen.
            this.genProgress = 0;
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
            if (this.state == State.INCOMPLETE)
            {
                genProgress += gametime.ElapsedGameTime.Milliseconds;
                elapsedSkillieTime += gametime.ElapsedGameTime.TotalSeconds;
                progressImage.Width = (int)((genProgress / 1000) / genCompleteTime * loading.Width);

                Debug.Write(elapsedSkillieTime);

                if (elapsedSkillieTime > 1 && allowSkillie == false) // Checks each second if a skillie can happen
                {
                    if (RNG.Next(1, 5) == 1)
                    {
                        allowSkillie = true;
                    }
                    elapsedSkillieTime = 0;
                }

                if (elapsedSkillieTime > 1 && skillie.active == false && allowSkillie == true) // Requirements for skillcheck: no current skillcheck, 3 seconds after previous, RNG of 1/5 chance each second
                {
                    elapsedSkillieTime = 0;
                    allowSkillie = false;
                    skillie.SkillCheck();
                }
            }

            if (genProgress/1000 >= genCompleteTime)
            {
                this.state = State.COMPLETE;
                progressImage.Width = loading.Width;
            }

            skillie.Update(gametime);

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
            if (genProgress != 0)
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
