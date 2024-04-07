using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Dead_By_Devcade
{
    public class MainMenu
    {
        private static Texture2D devcade;
        private static Texture2D deadByDaylight;
        private static SpriteFont menuFont;
        private static SpriteFont justX;
        
        private int mainMenu;
        private int genMenu;
        private Color[] iconColor = new Color[3] {Color.Yellow, Color.White, Color.White};

        private TrialLogic trial;

        public MainMenu()
        {
            this.mainMenu = 1;
            this.genMenu = 0;
        }

        public void LoadContent(ContentManager contentManager)
        {
            devcade = contentManager.Load<Texture2D>("Devcade");
            deadByDaylight = contentManager.Load<Texture2D>("DeadByDaylight");
            menuFont = contentManager.Load<SpriteFont>("menuFont");
            justX = contentManager.Load<SpriteFont>("X");
        }

        public void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (mainMenu == 1)
                {
                    // This will run the main game
                    this.mainMenu = 0;
                } else if (mainMenu == 2)
                {
                    this.mainMenu = 0;
                    this.genMenu = 1;
                }

                if (genMenu == 1)
                {
                    // This runs Normal
                    this.genMenu = 0;
                    trial = new TrialLogic(TrialLogic.gamemode.NORMAL);
                } else if (genMenu == 2)
                {
                    // This runs No Fail
                    this.genMenu = 0;
                    trial = new TrialLogic(TrialLogic.gamemode.NOFAIL);
                } else if (mainMenu == 3)
                {
                    // This runs Only Great
                    this.genMenu = 0;
                    trial = new TrialLogic(TrialLogic.gamemode.ONLYGREAT);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (genMenu != 0)
                {
                    this.mainMenu = 1;
                    this.genMenu = 0;
                }
                if (genMenu == 0 && mainMenu == 0)
                {
                    this.mainMenu = 1;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (mainMenu != 0)
                {
                    if (this.mainMenu - 1 != 0)
                    {
                        this.mainMenu--;
                    }
                }
                if (genMenu != 0)
                {
                    if (this.genMenu - 1 != 0)
                    {
                        this.genMenu--;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (mainMenu != 0)
                {
                    if (this.mainMenu + 1 != 3)
                    {
                        this.mainMenu++;
                    }
                }
                if (genMenu != 0)
                {
                    if (this.genMenu + 1 != 4)
                    {
                        this.genMenu++;
                    }
                }
            }

            if (this.mainMenu != 0)
            {
                Color[] tempColors = new Color[3] { Color.White, Color.White, Color.White };
                tempColors[mainMenu - 1] = Color.Yellow;
                this.iconColor = tempColors;
            } else if (this.genMenu != 0)
            {
                Color[] tempColors = new Color[3] { Color.White, Color.White, Color.White };
                tempColors[genMenu - 1] = Color.Yellow;
                this.iconColor = tempColors;
            }

        }

        public void Draw(SpriteBatch sb, Rectangle windowSize)
        {
            if (mainMenu != 0)
            {
                sb.DrawString(menuFont, "PLAY",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("PLAY").X / 2f, windowSize.Center.Y - 50f), iconColor[0]);
                sb.DrawString(menuFont, "GENERATOR", 
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("GENERATOR").X / 2f, windowSize.Center.Y + 100f), iconColor[1]);
                sb.DrawString(menuFont, "PRACTICE",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("PRACTICE").X / 2f, (windowSize.Center.Y + 100f) + menuFont.MeasureString("GENERATOR").Y ), iconColor[1]);

                sb.Draw(deadByDaylight, new Vector2(
                    windowSize.Center.X - deadByDaylight.Width / 5f,
                    windowSize.Top + 30f),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    0.4f,
                    SpriteEffects.None,
                    0);
                sb.DrawString(justX, "x",
                    new Vector2(windowSize.Center.X - justX.MeasureString("x").X / 2f, windowSize.Top + 90f), Color.White);
                sb.Draw(devcade, new Vector2(
                    windowSize.Center.X - devcade.Width / (6f + (2f / 3f)),
                    windowSize.Top + 170f),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    0.3f,
                    SpriteEffects.None,
                    0);
            }

            if (genMenu != 0)
            {
                sb.DrawString(menuFont, "NORMAL", new Vector2(windowSize.Center.X / 4f, windowSize.Bottom / 3f), iconColor[0]);
                sb.DrawString(menuFont, "NO FAIL", new Vector2(windowSize.Center.X / 4f, windowSize.Bottom/2f), iconColor[1]);
                sb.DrawString(menuFont, "ONLY GREAT", new Vector2(windowSize.Center.X / 4f, windowSize.Bottom / 1.5f), iconColor[2]);
            }

            if (mainMenu == 0 && genMenu == 0)
            {
                sb.DrawString(menuFont, "SORRY",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("SORRY").X / 2f, windowSize.Top + 90f), Color.White);
                sb.DrawString(menuFont, "THE ENTITY IS STILL",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("THE ENTITY IS STILL").X / 2f, (windowSize.Top + 90f) + menuFont.MeasureString("SORRY").Y + 3f), Color.White);
                sb.DrawString(menuFont, "CREATING ITS REALM",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("CREATING ITS REALM").X / 2f, (windowSize.Top + 90f) + menuFont.MeasureString("THE ENTITY IS STILL").Y + 3f), Color.White);
                sb.DrawString(menuFont, "THERE ARE GENERATORS",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("THERE ARE GENERATORS").X / 2f, (windowSize.Top + 90f) + menuFont.MeasureString("CREATING ITS REALM").Y + 3f), Color.White);
                sb.DrawString(menuFont, "TO BE FIXED IN GENERATOR",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("TO BE FIXED IN GENERATOR").X / 2f, (windowSize.Top + 90f) + menuFont.MeasureString("THERE ARE GENERATORS").Y + 3f), Color.White);
                sb.DrawString(menuFont, "PRACTICE, GOOD LUCK",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("PRACTICE, GOOD LUCK").X / 2f, (windowSize.Top + 90f) + menuFont.MeasureString("PRACTICE, GOOD LUCK").Y + 3f), Color.White);
                sb.DrawString(menuFont, "-BIGC",
                    new Vector2((windowSize.Center.X - menuFont.MeasureString("-BIGC").X / 2f) + 80f, (windowSize.Top + 90f) + menuFont.MeasureString("-BIGC").Y + 3f), Color.White);
            }

        }

    }
}
