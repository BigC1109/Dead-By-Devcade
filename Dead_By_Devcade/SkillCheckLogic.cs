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

        private Texture2D ring;
        private Texture2D red;
        private Texture2D skill;
        private Texture2D objective;


        private bool active { get; set; }
        private float SkillLocation;
        private float RedLocation;
        private Random RNG = new Random();

        private double resultTime;

        public SkillCheckLogic()
        {
            active = false;
            this.state = Result.NONE;
            this.resultTime = 0;
        }

        public void SkillCheck()
        {
            active = true;
            this.state = Result.NONE;
            this.resultTime = 0;
        }

        public void LoadContent(ContentManager contentManager)
        {
            ring = contentManager.Load<Texture2D>("Ring");
            red = contentManager.Load<Texture2D>("Red");
            skill = contentManager.Load<Texture2D>("Skill");
            objective = contentManager.Load<Texture2D>("Objective");
        }

        public void Update(GameTime gameTime)
        {
            if (active == true)
            {
                if (RedLocation < SkillLocation + 0.945f) // + 0.945f
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
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
                    RedLocation += 0.08f;
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
                sb.Draw(ring, new Vector2(windowSize.Center.X / 2f, 
                    windowSize.Center.Y / 2f), 
                    null, 
                    Color.White, 
                    0, 
                    Vector2.Zero, 
                    0.5f, 
                    SpriteEffects.None, 
                    0);
                sb.Draw(skill, new Vector2(
                    (float)(((windowSize.Center.X / 2f) + (ring.Height / 4f)) + ((ring.Height / 4f) - 10)* (Math.Cos(SkillLocation))),
                    (float)(((windowSize.Center.Y / 2f) + (ring.Width / 4f)) + ((ring.Width / 4f) - 10)* (Math.Sin(SkillLocation)))),
                    null, 
                    Color.White, 
                    SkillLocation + 119.97f, 
                    Vector2.Zero, 
                    0.5f, 
                    SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 
                    0);
                sb.Draw(red, new Vector2(
                    (float)(((windowSize.Center.X / 2f) + (ring.Height / 4f)) + ((ring.Height / 4f) + 55) * (Math.Cos(RedLocation))),
                    (float)(((windowSize.Center.Y / 2f) + (ring.Width / 4f)) + ((ring.Width / 4f) + 55) * (Math.Sin(RedLocation)))),
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
                windowSize.Center.X,
                windowSize.Top),
                Color.White);
            } else if (this.state == Result.GOOD)
            {
                sb.Draw(objective, new Vector2(
                windowSize.Center.X,
                windowSize.Top),
                Color.White);
            } else if (this.state == Result.FAIL)
            {
                sb.Draw(objective, new Vector2(
                windowSize.Center.X,
                windowSize.Top),
                Color.White);
            }
            Debug.Write(this.state);
        }
    }
}
