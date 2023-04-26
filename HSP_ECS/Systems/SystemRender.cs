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
        private string mSheetComponent = "ComponentSpriteSheet";
        private SpriteBatch sb;

        private ComponentPosition pos;
        private ComponentSprite sprite;
        private ComponentSpriteSheet sheet;

        public SystemRender(SpriteBatch pSpriteBatch)
        {
            mName = "SystemRender";
            sb= pSpriteBatch;
        }

        public override void SystemAction(List<Entity> pEntities, GameTime pGameTime)
        {
            foreach(Entity e in pEntities)
            {
                pos = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, e);
                if(pos != null)
                {
                    sprite = (ComponentSprite)GetComponentHelper.GetComponent(mSpriteComponent, e);
                    sheet = (ComponentSpriteSheet)GetComponentHelper.GetComponent(mSheetComponent, e);
                    if(sprite != null)
                    {
                        sb.Draw(sprite.Sprite, pos.Position, Color.White);
                    }
                    else if(sheet != null)
                    {
                        sheet.ChangeFrame();
                        sb.Draw(sheet.Sprite, pos.Position, sheet.Source, Color.White);
                    }

                }
            }
        }
    }
}
