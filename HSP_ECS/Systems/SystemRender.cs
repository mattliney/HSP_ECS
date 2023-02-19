using HSP_ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Systems
{
    public class SystemRender : System
    {
        private string mPosComponent = "ComponentPosition";
        private string mSpriteComponent = "ComponentSprite";
        private SpriteBatch sb;

        private ComponentPosition pos;
        private ComponentSprite sprite;

        public SystemRender(SpriteBatch pSpriteBatch)
        {
             mName = "SystemRender";
            sb= pSpriteBatch;
        }

        public override void SystemAction(List<Entity> pEntities)
        {
            foreach(Entity e in pEntities)
            {
                pos = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, e);
                if(pos != null)
                {
                    sprite = (ComponentSprite)GetComponentHelper.GetComponent(mSpriteComponent, e);
                    if(sprite != null)
                    {
                        sb.Draw(sprite.Sprite, pos.Position, Color.White);
                    }
                }
            }
        }
    }
}
