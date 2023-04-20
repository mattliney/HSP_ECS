using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public class ComponentParallax:Component
    {
        private float mDistance;
        public ComponentParallax(float pDistance)
        {
            mName = "ComponentParallax";
            mDistance = pDistance;
        }

        public float Distance
        {
            get { return mDistance; }
        }
    }
}
