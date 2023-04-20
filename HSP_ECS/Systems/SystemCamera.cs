using HSP_ECS.Components;
using HSP_ECS.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class SystemCamera : System
    {
        private string mPosComponent = "ComponentPosition";
        private string mParallaxComponent = "ComponentParallax";
        private ComponentPosition pos;
        private ComponentParallax par;

        public SystemCamera()
        {
            mName = "SystemCamera";
        }

        public override void SystemAction(List<Entity> pEntities, GameTime pGameTime)
        {
            CameraHelper.CameraUpdate();
            foreach(Entity e in pEntities)
            {
                if(e.Name != "player")
                {
                    pos = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, e);
                    if (pos != null)
                    {
                        par = (ComponentParallax)GetComponentHelper.GetComponent(mParallaxComponent, e);

                        if (par != null)
                        {
                            Vector2 next = CameraHelper.cameraMovement;
                            next.X = next.X * par.Distance;

                            pos.Position += TimeStepHelper.CalculateStepVector(next, pGameTime);
                        }
                        else
                        {
                            pos.Position += TimeStepHelper.CalculateStepVector(CameraHelper.cameraMovement, pGameTime);
                        }
                    }
                }
            }
        }
    }
}
