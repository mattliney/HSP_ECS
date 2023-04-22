using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public class ComponentButton:Component
    {
        private bool mLeftClick;
        private bool mRightClick;

        public ComponentButton()
        {
            mName = "ComponentButton";
            mLeftClick = false;
            mRightClick = false;
        }

        public bool LeftClick
        { 
            get 
            { 
                return mLeftClick; 
            }

            set
            {
                mLeftClick = value;
            }
        }

        public bool RightClick
        { 
            get
            { 
                return mRightClick;
            } 

            set
            {
                mRightClick = value;
            }
        }
    }
}
