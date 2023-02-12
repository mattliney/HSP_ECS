using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public class ComponentSprite:Component
    {
        private Texture2D mSprite;

        public ComponentSprite(Texture2D pSprite)
        {
            mSprite= pSprite;
        }

        public Texture2D Sprite
        {
            get
            {
                return mSprite;
            }
        }
    }
}
