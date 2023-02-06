using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Components
{
    public abstract class Component
    {
        protected string mName;

        public string Name
        {
            get { return mName; }
        }

        public virtual void Close()
        {
            return;
        }

    }
}
