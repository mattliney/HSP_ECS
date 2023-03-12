using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public  class EntityManager
    {
        private List<Entity> mEntities;

        public EntityManager()
        {
            mEntities = new List<Entity>();
        }

        public Entity GetEntity(string pEntityName)
        {
            foreach(Entity e in mEntities)
            {
                if(e.Name == pEntityName)
                {
                    return e;
                }
            }

            return null;
        }

        public void ClearList()
        {
            mEntities.Clear();
        }

        public void AddEntity(Entity pEntity)
        {
            mEntities.Add(pEntity);
        }

        public void RemoveEntity(Entity pEntity)
        {
            mEntities.Remove(pEntity);
        }

        public List<Entity> Entities
        {
            get
            {
                return mEntities;
            }
        }
    }
}
