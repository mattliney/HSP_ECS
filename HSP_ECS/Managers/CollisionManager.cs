using HSP_ECS.Components;
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
        AABB_AABB_LEFT,
        AABB_AABB_RIGHT,
        AABB_AABB_TOP,
        AABB_AABB_BOTTOM,
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
                if(col.type == CollisionType.AABB_AABB_LEFT)
                {
                    RespondAABB_AABB_Left(col);
                }
                else if(col.type == CollisionType.AABB_AABB_RIGHT)
                {
                    RespondAABB_AABB_Right(col);
                }
                else if (col.type == CollisionType.AABB_AABB_BOTTOM)
                {
                    RespondAABB_AABB_Bottom(col);
                }
                else if (col.type == CollisionType.AABB_AABB_TOP)
                {
                    RespondAABB_AABB_Top(col);
                }
                else if(col.type == CollisionType.POINT_AABB)
                {
                    RespondPoint_AABB(col);
                }
            }

            ClearManifold();
        }

        private void RespondAABB_AABB_Top(Collision pCol)
        {
            ComponentPosition e1pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity1);
            ComponentPosition e2pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity2);

            ComponentCollisionAABB e2coll = (ComponentCollisionAABB)GetComponentHelper.GetComponent("ComponentCollisionAABB", pCol.entity2);

            e1pos.SetY(e2pos.Position.Y - (e2coll.Height + 1));
        }

        private void RespondAABB_AABB_Right(Collision pCol)
        {
            ComponentPosition e1pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity1);

            ComponentPosition e2pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity2);
            ComponentCollisionAABB e2coll = (ComponentCollisionAABB)GetComponentHelper.GetComponent("ComponentCollisionAABB", pCol.entity2);

            e1pos.SetX(e2pos.Position.X + (e2coll.Width + 3));
        }

        private void RespondAABB_AABB_Bottom(Collision pCol)
        {
            ComponentPosition e1pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity1);
            ComponentPhysics e1Phys = (ComponentPhysics)GetComponentHelper.GetComponent("ComponentPhysics", pCol.entity1);

            ComponentPosition e2pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity2);
            ComponentCollisionAABB e2coll = (ComponentCollisionAABB)GetComponentHelper.GetComponent("ComponentCollisionAABB", pCol.entity2);

            //e1Phys.SetVelY(0);
            e1pos.SetY(e2pos.Position.Y + (e2coll.Height));
        }

        private void RespondAABB_AABB_Left(Collision pCol)
        {
            ComponentPosition e1pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity1);
            ComponentPosition e2pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pCol.entity2);

            ComponentCollisionAABB e2coll = (ComponentCollisionAABB)GetComponentHelper.GetComponent("ComponentCollisionAABB", pCol.entity2);

            e1pos.SetX(e2pos.Position.X - (e2coll.Width + 3));
        }

        private void RespondPoint_AABB(Collision pCol)
        {
            if(pCol.entity1.Name == "cursor")
            {

            }
            else
            {
                ComponentPhysics phys = (ComponentPhysics)GetComponentHelper.GetComponent("ComponentPhysics", pCol.entity1);
                phys.StopAccel();
            }
        }

        private void ClearManifold()
        {
            mCollisionManifold.Clear();
        }

        public void RegisterCollision(Collision pCol)
        {
            mCollisionManifold.Add(pCol);
        }
    }
}
