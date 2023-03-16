using HSP_ECS.Components;
using HSP_ECS.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class SystemPhysics : System
    {
        private string mPosComponent = "ComponentPosition";
        private string mVelComponent = "ComponentVelocity";
        private SpriteBatch sb;

        private ComponentPosition pos;
        private ComponentVelocity vel;

        public SystemPhysics()
        {
            mName = "SystemPhysics";
        }

        public override void SystemAction(List<Entity> pEntities, GameTime pGameTime)
        {
            foreach(Entity e in pEntities)
            {
                pos = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, e);
                if (pos != null)
                {
                    vel = (ComponentVelocity)GetComponentHelper.GetComponent(mVelComponent, e);
                    if (vel != null)
                    {
                        pos.Position += TimeStepHelper.CalculateStepVector(vel.Velocity, pGameTime);
                    }
                }
            }
        }
    }
}
