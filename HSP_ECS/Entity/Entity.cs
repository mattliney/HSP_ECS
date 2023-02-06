using HSP_ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class Entity
    {
        public string mName;
        public List<Component> mComponents;

        public Entity(string pName)
        {
            mName= pName;
            mComponents = new List<Component>();
        }
    }
}
