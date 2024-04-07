using Devcade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Dead_By_Devcade
{
    public class SkillCheckLogic
    {
        public enum Result {
            GOOD,
            GREAT,
            FAIL,
            NONE
        }
        public Result state { get; set; }
        public bool resultTaken { get; set; }

        private static Texture2D ring;
        private static Texture2D red;
        private static Texture2D skill;
        private static Texture2D objective;
        private static SpriteFont font;


        public bool active { get; set; }
        private float SkillLocation;
        private float RedLocation;
        private Random RNG = new Random();

        private double resultTime;

        public SkillCheckLogic()
        {
            active = false;
            this.state = Result.NONE;
            this.resultTime = 0;
            this.resultTaken = false;
        }

        public void SkillCheck()
        {
            active = true;
            this.state = Result.NONE;
            this.resultTime = 0;
            this.resultTaken = false;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            ring = contentManager.Load<Texture2D>("Ring");
            red = contentManager.Load<Texture2D>("Red");
            skill = contentManager.Load<Texture2D>("Skill");
            objective = contentManager.Load<Texture2D>("Objective");
            font = contentManager.Load<SpriteFont>("scoreFont");
        }

        public void Update(GameTime gameTime)
        {
            if (active == true)
            {
                if (RedLocation < SkillLocation + 0.945f) // + 0.945f
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || Input.GetButton(1, Input.ArcadeButtons.A1))
                    {
                        if (RedLocation < SkillLocation - 0.08f)
                        {
                            this.state = Result.FAIL;
                            active = false;
                        } else if (RedLocation < SkillLocation + 0.16f)
                        {
                            this.state = Result.GREAT;
                            active = false;
                        } else
                        {
                            this.state = Result.GOOD;
                            active = false;
                        }
                    }
                    RedLocation += 0.08f; // FIX THIS, it should work in the way that amount of gametime effects location, used ElapsedTime
                } else
                {
                    this.state = Result.FAIL;
                    active = false;
                }
            } else
            {
                SkillLocation = RNG.Next(-500, 3000) * 0.001f; // Lowest Possible Value: -0.5, Max Value: 3.00
                RedLocation = -1.7f;

                if (this.state != Result.NONE && resultTime == 0)
                {
                    resultTime = gameTime.TotalGameTime.TotalSeconds;
                }

                if (resultTime + 1d <= gameTime.TotalGameTime.TotalSeconds)
                {
                    this.state = Result.NONE;
                    resultTime = 0;
                }

            }
        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            if (active == true)
            {
                sb.Draw(ring, new Vector2((windowSize.Center.X / 2f)-12, 
                    windowSize.Center.Y - 200f), 
                    null, 
                    Color.White, 
                    0, 
                    Vector2.Zero, 
                    0.5f, 
                    SpriteEffects.None, 
                    0);
                sb.Draw(skill, new Vector2(
                    (float)(((windowSize.Center.X / 2f) - 12 + (ring.Height / 4f)) + ((ring.Height / 4f) - 10)* (Math.Cos(SkillLocation))),
                    (float)(((windowSize.Center.Y - 200f) + (ring.Width / 4f)) + ((ring.Width / 4f) - 10)* (Math.Sin(SkillLocation)))),
                    null, 
                    Color.White, 
                    SkillLocation + 119.97f, 
                    Vector2.Zero, 
                    0.5f, 
                    SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 
                    0);
                sb.Draw(red, new Vector2(
                    (float)(((windowSize.Center.X / 2f) - 12 + (ring.Height / 4f)) + ((ring.Height / 4f) + 55) * (Math.Cos(RedLocation))),
                    (float)(((windowSize.Center.Y - 200f) + (ring.Width / 4f)) + ((ring.Width / 4f) + 55) * (Math.Sin(RedLocation)))),
                    null,
                    Color.White,
                    RedLocation + 1.65f,
                    Vector2.Zero,
                    0.45f,
                    SpriteEffects.None,
                    0);
            }

            if (this.state == Result.GREAT)
            {
                sb.Draw(objective, new Vector2(
                windowSize.Center.X - objective.Width / 2f,
                windowSize.Top),
                Color.White);
                sb.DrawString(font, "GREAT SKILL CHECK",
                    new Vector2(windowSize.Center.X - font.MeasureString("GREAT SKILL CHECK").X / 2f, windowSize.Top + objective.Height), Color.White);
            } else if (this.state == Result.GOOD)
            {
                sb.Draw(objective, new Vector2(
                windowSize.Center.X - objective.Width / 2f,
                windowSize.Top),
                Color.White);
                sb.DrawString(font, "GOOD SKILL CHECK",
                    new Vector2(windowSize.Center.X - font.MeasureString("GOOD SKILL CHECK").X / 2f, windowSize.Top + objective.Height), Color.White);
            } else if (this.state == Result.FAIL)
            {
                sb.Draw(objective, new Vector2(
                windowSize.Center.X - objective.Width/2f,
                windowSize.Top),
                Color.White);
                sb.DrawString(font, "FAILED SKILL CHECK",
                    new Vector2(windowSize.Center.X - font.MeasureString("FAILED SKILL CHECK").X / 2f, windowSize.Top + objective.Height), Color.White);
            }
        }
    }
}
