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
        private string mPhysComponent = "ComponentPhysics";

        private ComponentPosition pos;
        private ComponentPhysics phys;

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
                    phys = (ComponentPhysics)GetComponentHelper.GetComponent(mPhysComponent, e);
                    if (phys != null)
                    {
                        GravityCalc();
                        pos.Position += TimeStepHelper.CalculateStepVector(phys.Velocity, pGameTime);
                    }
                }
            }
        }

        private void GravityCalc()
        {
            float newY = phys.Velocity.Y;
            newY += phys.Gravity;
            phys.SetVelY(newY);
        }
    }
}
