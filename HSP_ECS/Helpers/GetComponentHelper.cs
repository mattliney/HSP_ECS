using HSP_ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    static public class GetComponentHelper
    {
        public static Component GetComponent(string pName, Entity pEntity)
        {
            foreach(Component c in pEntity.mComponents)
            {
                if(c.Name == pName)
                {
                    return c;
                }
            }

            return null;
        }
    }
}
