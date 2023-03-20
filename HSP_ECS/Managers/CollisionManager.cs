using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public struct Collision
    {
        public Entity entity1;
        public Entity entity2;
        public CollisionType type;
    }

    public enum CollisionType
    {
        AABB_AABB,
        POINT_AABB
    }

    public class CollisionManager
    {
        private EntityManager mEntityManager;
        private List<Collision> mCollisionManifold;

        public CollisionManager(EntityManager pEntityManager)
        {
            mEntityManager = pEntityManager;
            mCollisionManifold= new List<Collision>();
        }

        public void ProcessCollisions()
        {
            foreach(Collision col in mCollisionManifold)
            {
                if(col.type == CollisionType.AABB_AABB)
                {
                    RespondAABB_AABB(col);
                }
                else if(col.type == CollisionType.POINT_AABB)
                {
                    RespondPoint_AABB(col);
                }
            }

            ClearManifold();
        }

        private void RespondAABB_AABB(Collision pCol)
        {

        }

        private void RespondPoint_AABB(Collision pCol)
        {

        }

        private void ClearManifold()
        {
            mCollisionManifold.Clear();
        }
    }
}
