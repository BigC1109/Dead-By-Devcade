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
        private static SpriteFont wipFont;
        
        private int mainMenu;
        private int genMenu;
        private Color[] iconColor = new Color[3] {Color.Yellow, Color.White, Color.White};
        private bool menuMove;

        private TrialLogic trial;

        private int counter;

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
            wipFont = contentManager.Load<SpriteFont>("wipFont");
        }

        public void Update(GameTime gameTime)
        {
            counter++;
            if (menuMove)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    if (mainMenu == 1)
                    {
                        // This will run the main game
                        this.mainMenu = 0;
                    }
                    else if (mainMenu == 2)
                    {
                        this.mainMenu = 0;
                        this.genMenu = 1;
                    } else if (genMenu == 1)
                    {
                        // This runs Normal
                        this.genMenu = 0;
                        trial = new TrialLogic(TrialLogic.gamemode.NORMAL);
                    }
                    else if (genMenu == 2)
                    {
                        // This runs No Fail
                        this.genMenu = 0;
                        trial = new TrialLogic(TrialLogic.gamemode.NOFAIL);
                    }
                    else if (genMenu == 3)
                    {
                        // This runs Only Great
                        this.genMenu = 0;
                        trial = new TrialLogic(TrialLogic.gamemode.ONLYGREAT);
                    }
                    menuMove = false;
                } else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    if (genMenu != 0)
                    {
                        this.mainMenu = 1;
                        this.genMenu = 0;
                    } else if (genMenu == 0 && mainMenu == 0)
                    {
                        this.mainMenu = 1;
                    }
                    menuMove = false;
                } else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (mainMenu != 0)
                    {
                        if (this.mainMenu - 1 != 0)
                        {
                            this.mainMenu--;
                        }
                    } else if (genMenu != 0)
                    {
                        if (this.genMenu - 1 != 0)
                        {
                            this.genMenu--;
                        }
                    }
                    menuMove = false;
                } else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (mainMenu != 0)
                    {
                        if (this.mainMenu + 1 != 3)
                        {
                            this.mainMenu++;
                        }
                    } else if (genMenu != 0)
                    {
                        if (this.genMenu + 1 != 4)
                        {
                            this.genMenu++;
                        }
                    }
                    menuMove = false;
                }
            } else
            {
                if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
                {
                    menuMove = true;
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
                sb.DrawString(menuFont, "NORMAL",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("NORMAL").X / 2f, windowSize.Center.Y - 100f), iconColor[0]);
                sb.DrawString(menuFont, "NO FAIL",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("NO FAIL").X / 2f, windowSize.Center.Y), iconColor[1]);
                sb.DrawString(menuFont, "ONLY GREAT",
                    new Vector2(windowSize.Center.X - menuFont.MeasureString("ONLY GREAT").X / 2f, windowSize.Center.Y + 100f), iconColor[2]);
            }

            if (mainMenu == 0 && genMenu == 0 && trial == null)
            {
                float sizeCorrection = windowSize.Top + 300f;
                sb.DrawString(wipFont, "SORRY",
                    new Vector2(windowSize.Center.X - wipFont.MeasureString("SORRY").X / 2f, sizeCorrection), Color.White);
                sizeCorrection += wipFont.MeasureString("SORRY").Y + 3f;
                sb.DrawString(wipFont, "THE ENTITY IS STILL",
                    new Vector2(windowSize.Center.X - wipFont.MeasureString("THE ENTITY IS STILL").X / 2f, sizeCorrection), Color.White);
                sizeCorrection += wipFont.MeasureString("THE ENTITY IS STILL").Y + 3f;
                sb.DrawString(wipFont, "CREATING ITS REALM",
                    new Vector2(windowSize.Center.X - wipFont.MeasureString("CREATING ITS REALM").X / 2f, sizeCorrection), Color.White);
                sizeCorrection += wipFont.MeasureString("CREATING ITS REALM").Y + 3f;
                sb.DrawString(wipFont, "THERE ARE GENERATORS",
                    new Vector2(windowSize.Center.X - wipFont.MeasureString("THERE ARE GENERATORS").X / 2f, sizeCorrection), Color.White);
                sizeCorrection += wipFont.MeasureString("CREATING ITS REALM").Y + 3f;
                sb.DrawString(wipFont, "TO BE FIXED IN GENERATOR",
                    new Vector2(windowSize.Center.X - wipFont.MeasureString("TO BE FIXED IN GENERATOR").X / 2f, sizeCorrection), Color.White);
                sizeCorrection += wipFont.MeasureString("THERE ARE GENERATORS").Y + 3f;
                sb.DrawString(wipFont, "PRACTICE, GOOD LUCK",
                    new Vector2(windowSize.Center.X - wipFont.MeasureString("PRACTICE, GOOD LUCK").X / 2f, sizeCorrection), Color.White);
                sizeCorrection += wipFont.MeasureString("PRACTICE, GOOD LUCK").Y + 20f;
                sb.DrawString(wipFont, "-BIGC",
                    new Vector2((windowSize.Center.X - wipFont.MeasureString("-BIGC").X / 2f) + 100f, sizeCorrection), Color.White);
            }

        }

    }
}
