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

        public State state { get; set; }
        public SkillCheckLogic skillie = new SkillCheckLogic();

        private static Texture2D underBar;
        private static Texture2D loading;
        private static Texture2D generator;
        private static Texture2D explosion;
        private static SpriteFont font;
        private Rectangle progressImage;

        private Random RNG = new Random();
        private int genCompleteTime;
        private float genProgress; // This value is in milliseconds, divide by 1000 to get total amount
        private bool skillieFailed;
        private float explosionOpacity;

        private double elapsedSkillieTime;
        private bool allowSkillie;


        public GeneratorLogic()
        {
            this.state = State.INCOMPLETE;
            this.genCompleteTime = 60; // Total amount of time to complete the gen.
            this.genProgress = 0;
            this.skillieFailed = false;
        }

        public void LoadContent(ContentManager contentManager)
        {
            // If found, look for hand icon for repairing. Couldn't find it so it's not there right now.
            underBar = contentManager.Load<Texture2D>("UnderBar");
            loading = contentManager.Load<Texture2D>("Loading");
            generator = contentManager.Load<Texture2D>("Generator");
            explosion = contentManager.Load<Texture2D>("Explosion");
            font = contentManager.Load<SpriteFont>("interactionFont");

            this.progressImage = new Rectangle(0, 0, loading.Width, loading.Height);
        }

        public void Update(GameTime gametime)
        {
            if (this.state == State.INCOMPLETE)
            {
                elapsedSkillieTime += gametime.ElapsedGameTime.TotalSeconds;
                progressImage.Width = (int)((genProgress / 1000) / genCompleteTime * loading.Width);

                if (this.skillieFailed)
                {
                    if (elapsedSkillieTime > 5)
                    {
                        this.skillieFailed = false;
                        elapsedSkillieTime = 0;
                    }
                    explosionOpacity -= (float)elapsedSkillieTime / 50f;
                }
                
                if (!this.skillieFailed)
                {
                    genProgress += gametime.ElapsedGameTime.Milliseconds;
                }

                if (elapsedSkillieTime > 1 && allowSkillie == false && !skillieFailed) // Checks each second if a skillie can happen
                {
                    if (RNG.Next(1, 5) == 1)
                    {
                        allowSkillie = true;
                    }
                    elapsedSkillieTime = 0;
                }

                if (elapsedSkillieTime > 1 && skillie.active == false && allowSkillie == true && !skillieFailed) // Requirements for skillcheck: no current skillcheck, 3 seconds after previous, RNG of 1/5 chance each second
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

            if (skillie.state != SkillCheckLogic.Result.NONE && !skillie.resultTaken)
            {
                if (skillie.state == SkillCheckLogic.Result.FAIL)
                {
                    genProgress -= (float)(genCompleteTime * 0.05) * 1000;
                    if (genProgress < 0) { genProgress = 0; }
                    skillieFailed = true;
                    explosionOpacity = 1f;
                } else if (skillie.state == SkillCheckLogic.Result.GREAT)
                {
                    genProgress += (float)(genCompleteTime * 0.03) * 1000;
                    if (genProgress/1000 > genCompleteTime) { genProgress = genCompleteTime * 1000; }
                }
                skillie.resultTaken = true;
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

            if (skillieFailed) //skillie.state == SkillCheckLogic.Result.FAIL
            {
                sb.Draw(explosion, new Vector2(
                    windowSize.Center.X - explosion.Width/10f,
                    (windowSize.Bottom / 2f) + 30f),
                    null,
                    Color.White * explosionOpacity,
                    0,
                    Vector2.Zero,
                    0.20f,
                    SpriteEffects.None,
                    0);
            }
            sb.Draw(generator, new Vector2(
                windowSize.Center.X - generator.Width,
                (windowSize.Bottom/2f) + 50f),
                null,
                Color.White,
                0,
                Vector2.Zero,
                2f,
                SpriteEffects.None,
                0);

            sb.DrawString(font, "REPAIR", new Vector2(
                windowSize.Left + 10f, 
                windowSize.Bottom - underBar.Height - 30f), 
                Color.White * 0.50f);

            skillie.Draw(sb, windowSize);
        }
    }
}
