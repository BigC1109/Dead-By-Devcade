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
        private static SpriteFont font;
        private static SpriteFont justX;
        
        private int mainMenu;
        private int genMenu;
        private Color[] iconColor = new Color[3] {Color.Yellow, Color.White, Color.White};

        public MainMenu()
        {
            this.mainMenu = 1;
            this.genMenu = 0;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            devcade = contentManager.Load<Texture2D>("Devcade");
            deadByDaylight = contentManager.Load<Texture2D>("DeadByDaylight");
            font = contentManager.Load<SpriteFont>("X");
            justX = contentManager.Load<SpriteFont>("X");
        }

        public void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (mainMenu == 1)
                {
                    // This will run the main game
                } else if (mainMenu == 2)
                {
                    this.mainMenu = 0;
                    this.genMenu = 1;
                }

                if (genMenu == 1)
                {
                    // This runs Normal
                    this.genMenu = 0;
                } else if (genMenu == 2)
                {
                    // This runs No Fail
                    this.genMenu = 0;
                } else if (mainMenu == 3)
                {
                    // This runs Only Great
                    this.genMenu = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (genMenu != 0)
                {
                    this.mainMenu = 1;
                    this.genMenu = 0;
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
                sb.DrawString(font, "PLAY", new Vector2(windowSize.Center.X, windowSize.Bottom/3f), iconColor[0]);
                sb.DrawString(font, "GENERATOR PRACITCE", new Vector2(windowSize.Center.X / 4f, windowSize.Bottom/1.5f), iconColor[1]);
            }

            if (genMenu != 0)
            {
                sb.DrawString(font, "NORMAL", new Vector2(windowSize.Center.X / 4f, windowSize.Bottom / 3f), iconColor[0]);
                sb.DrawString(font, "NO FAIL", new Vector2(windowSize.Center.X / 4f, windowSize.Bottom/2f), iconColor[1]);
                sb.DrawString(font, "ONLY GREAT", new Vector2(windowSize.Center.X / 4f, windowSize.Bottom / 1.5f), iconColor[2]);
            }
        }

    }
}
