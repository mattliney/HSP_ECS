using HSP_ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class ComponentCollisionPoint : Component
    {
        private Vector2 mPoint;

        public ComponentCollisionPoint(Vector2 pPoint)
        {
            mPoint= pPoint;
        }

        public Vector2 Point
        {
            get { return mPoint; }
            set { mPoint = value; }
        }
    }
}
