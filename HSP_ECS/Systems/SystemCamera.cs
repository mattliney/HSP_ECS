using HSP_ECS.Components;
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
        private ComponentPosition pos;

        public SystemCamera()
        {
            mName = "SystemCamera";
        }

        public override void SystemAction(List<Entity> pEntities)
        {
            foreach(Entity e in pEntities)
            {
                if(e.Name != "player")
                {
                    pos = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, e);
                    if (pos != null)
                    {
                        pos.Position += CameraHelper.cameraMovement;
                    }
                }
            }
        }
    }
}
