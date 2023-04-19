using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public class ComponentPosition:Component
    {
        private Vector2 mPosition;

        public ComponentPosition(Vector2 pPos)
        {
            mPosition= pPos;
            mName = "ComponentPosition";
        }

        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public void SetY(float pY)
        {
            mPosition.Y = pY;
        }
        
        public void SetX(float pX)
        {
            mPosition.X = pX;
        }
    }
}
