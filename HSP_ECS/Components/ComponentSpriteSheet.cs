using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public class ComponentSpriteSheet:Component
    {
        private Texture2D mSprite;
        private Rectangle mSource;

        private Stopwatch mTimer;

        private int mFrameWidth;
        private int mFrameHeight;
        private int mFrameCount;
        private int mCurrentFrame;

        private float mFrameRate;

        public ComponentSpriteSheet(Texture2D pSprite, int pFrameWidth, int pFrameHeight, int pFrameCount, float pFrameRate)
        {
            mName = "ComponentSpriteSheet";
            mTimer = new Stopwatch();
            mTimer.Start();

            mSprite = pSprite;
            mFrameCount= pFrameCount;
            mFrameWidth= pFrameWidth;
            mFrameHeight= pFrameHeight;
            mFrameRate = pFrameRate;
            mCurrentFrame= 0;

            mSource = new Rectangle(mCurrentFrame * mFrameWidth, 0, mFrameWidth, mFrameHeight);
        }

        public void UpdateSource()
        {
            if(mCurrentFrame >= mFrameCount)
            {
                mCurrentFrame= 0;
            }
            else
            {
                mCurrentFrame++;
            }

            mSource.X = mCurrentFrame * mFrameWidth;
        }

        public Texture2D Sprite
        {
            get
            {
                return mSprite;
            }
        }

        public Rectangle Source
        {
            get
            {
                return mSource;
            }
        }

        public void ChangeFrame()
        {
            if(mTimer.ElapsedMilliseconds >= mFrameRate)
            {
                mTimer.Restart();
                UpdateSource();
            }
        }
    }
}
