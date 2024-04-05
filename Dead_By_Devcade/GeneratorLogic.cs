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

        private int totalTime;
        private float progress;
        private int progressTime;
        private Rectangle progressImage;
        private SkillCheckLogic skillie;


        public GeneratorLogic()
        {
            this.state = State.INCOMPLETE;
            this.skillie = new SkillCheckLogic();
            this.totalTime = 60; // Total amount of time to complete the gen.
            this.progress = 0;
        }

        public void LoadContent(ContentManager contentManager)
        {
            underBar = contentManager.Load<Texture2D>("UnderBar");
            loading = contentManager.Load<Texture2D>("Loading");
            this.progressImage = new Rectangle(0, 0, loading.Width, loading.Height);
        }

        public void Update(GameTime gametime)
        {
            if (progress <= totalTime && gametime.TotalGameTime.Seconds > progressTime)
            {
                progressImage.Width = (int)(progress / totalTime * loading.Width);
                progress++;
                progressTime = gametime.TotalGameTime.Seconds + 1;
            } else if (progress >= totalTime) 
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


    }
}
