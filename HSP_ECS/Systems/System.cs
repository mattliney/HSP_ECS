using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public abstract class System
    {
        protected string mName;

        public System()
        {

        }

        public abstract void SystemAction(List<Entity> pEntities, GameTime pGameTime);

        public string Name
        {
            get
            {
                return mName;
            }
        }
    }
}
