using HSP_ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class ComponentCollisionAABB : Component
    {
        private int mWidth;
        private int mHeight;

        public ComponentCollisionAABB(int pWidth, int pHeight)
        {
            mWidth= pWidth;
            mHeight= pHeight;
            mName = "ComponentCollisionAABB";
        }

        public int Width
        { 
            get
            {
                return mWidth; 
            }
        }

        public int Height
        {
            get
            {
                return mHeight;
            }
        }
    }
}
