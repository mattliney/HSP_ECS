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

        public virtual void SystemAction(List<Entity> pEntities)
        {
            // Every System Must Implement This!
        }

        public string Name
        {
            get
            {
                return mName;
            }
        }
    }
}
