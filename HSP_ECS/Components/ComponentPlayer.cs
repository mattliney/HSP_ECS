using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public class ComponentPlayer:Component
    {
        private bool mHasJumped;
        private int mHealth;

        public ComponentPlayer(int pHealth)
        {
            mName = "ComponentPlayer";
            mHealth = pHealth;
            mHasJumped = false;
        }

        public bool HasJumped
        {
            get { return mHasJumped;}
            set { mHasJumped = value;}
        }

        public int Health
        {
            get { return mHealth; } 
            set { mHealth = value;}
        }
    }
}
