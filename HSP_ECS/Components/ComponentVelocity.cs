using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public class ComponentVelocity : Component
    {
        private Vector2 mVelocity;

        public ComponentVelocity(Vector2 pVel)
        {
            mVelocity = pVel;
            mName = "ComponentVelocity";
        }

        public Vector2 Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
        }
    }
}
