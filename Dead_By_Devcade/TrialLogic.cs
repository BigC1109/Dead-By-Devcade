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
            ONLYGREAT,
            TEST
        }

        public enum statusTypes
        {
            WIN,
            LOSS,
            INPROGRESS
        }

        public gamemode gamemodeSelected {  get; set; }

        private Texture2D generator;
        private Texture2D escaped;
        private Texture2D sacrificed;
        private SpriteFont genLeft;
        private SpriteFont totalGens;
        private SpriteFont resultText;

        private int genRemaining;
        private GeneratorLogic currentGenerator;
        private statusTypes status;

        private float resultReveal;

        public TrialLogic(gamemode type)
        {
            this.gamemodeSelected = type;
            this.genRemaining = 5;
            this.status = statusTypes.INPROGRESS;
        }

        public void LoadContent(ContentManager contentManager)
        {
            generator = contentManager.Load<Texture2D>("Generator");
            escaped = contentManager.Load<Texture2D>("Escaped");
            sacrificed = contentManager.Load<Texture2D>("Sacrificed");
            genLeft = contentManager.Load<SpriteFont>("genLeft");
            totalGens = contentManager.Load<SpriteFont>("totalGens");
            resultText = contentManager.Load<SpriteFont>("resultText");
        }

        public void Update(GameTime gameTime)
        {
            if (currentGenerator == null || currentGenerator.state == GeneratorLogic.State.COMPLETE && genRemaining > 0)
            {
                currentGenerator = new GeneratorLogic();

                if (currentGenerator.state == GeneratorLogic.State.COMPLETE)
                {
                    genRemaining--;
                }
            }

            if (this.gamemodeSelected == gamemode.NORMAL)
            {
                // Doesnt fail, only shows win screen when all gens 
            } else if (this.gamemodeSelected == gamemode.NOFAIL)
            {
                // Can fail, shows lose screen if a failed skill check occurs
                if (currentGenerator.skillie.state == SkillCheckLogic.Result.FAIL)
                {
                    //END SCREEN
                    this.status = statusTypes.LOSS;
                }
            } else if (this.gamemodeSelected == gamemode.ONLYGREAT)
            {
                // Can fail, shows a lose screen if a good or failed skill check occurs
                if (currentGenerator.skillie.state == SkillCheckLogic.Result.FAIL || currentGenerator.skillie.state == SkillCheckLogic.Result.GOOD)
                {
                    //END SCREEN
                    this.status = statusTypes.LOSS;
                }
            }

            if (currentGenerator.state == GeneratorLogic.State.COMPLETE)
            {
                this.status = statusTypes.WIN;
            }

            if (this.status != statusTypes.INPROGRESS)
            {
                if (resultReveal < 1)
                {
                    resultReveal += (float)gameTime.ElapsedGameTime.TotalSeconds / 5f;
                }
            }

        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            if (this.status == statusTypes.INPROGRESS)
            {
                sb.Draw(generator, new Vector2(
                    windowSize.Right - 100f,
                    windowSize.Bottom - 100f),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    0.8f,
                    SpriteEffects.None,
                    0);
                sb.DrawString(totalGens, "/5", new Vector2(
                    windowSize.Right - 120f,
                    windowSize.Bottom - 40f),
                    Color.White * 0.50f);
                sb.DrawString(genLeft, String.Format("{0}", 5 - genRemaining), new Vector2(
                    windowSize.Right - 145f,
                    windowSize.Bottom - 65f),
                    Color.White);
            } else if (this.status == statusTypes.WIN)
            {
                sb.Draw(escaped, new Vector2(
                    windowSize.Center.X - escaped.Width * 1.5f,
                    windowSize.Center.Y - 50f),
                    null,
                    Color.White * resultReveal,
                    0,
                    Vector2.Zero,
                    3f,
                    SpriteEffects.None,
                    0);
                sb.DrawString(resultText, "ESCAPED", new Vector2(
                    windowSize.Left + 80f,
                    windowSize.Center.Y - 100f),
                    Color.White * resultReveal);
            } else if (this.status == statusTypes.LOSS)
            {
                sb.Draw(sacrificed, new Vector2(
                    windowSize.Center.X - sacrificed.Width / 2f,
                    windowSize.Center.Y),
                    null,
                    Color.White * resultReveal,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0);
                sb.DrawString(resultText, "SACRIFICED", new Vector2(
                    windowSize.Left + 40f,
                    windowSize.Center.Y - 100f),
                    Color.White * resultReveal);
            }
        }
    }
}