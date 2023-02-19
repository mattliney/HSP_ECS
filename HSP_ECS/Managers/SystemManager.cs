using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class SystemManager
    {
        private List<System> mSystems;
        public SystemManager()
        {
            mSystems = new List<System>();
        }

        public void Action(List<Entity> pEntities)
        {
            foreach (System s in mSystems)
            {
                s.SystemAction(pEntities);
            }
        }

        public void AddSystem(System pSys)
        {
            foreach (System s in mSystems)
            {
                if (s.Name == pSys.Name)
                {
                    throw new Exception("DO NOT add 2 identical systems. Try clearing the list with SystemManager.Clear().");
                }
            }
            mSystems.Add(pSys);
        }

        public void RemoveSystem(System pSys) 
        {
            mSystems.Remove(pSys);
        }

        public void Clear()
        {
            mSystems.Clear();
        }

        public System GetSystem(System pSys)
        {
            foreach(System s in mSystems)
            {
                if(s.Name == pSys.Name)
                {
                    return s;
                }
            }

            return null;
        }
    }
}
