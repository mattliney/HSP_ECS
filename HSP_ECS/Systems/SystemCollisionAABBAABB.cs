using HSP_ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class SystemCollisionAABBAABB : System
    {
        private string mPosComponent = "ComponentPosition";
        private string mCollisionComponent = "ComponentCollisionAABB";

        private CollisionManager mCollisionManager;
        private ComponentCollisionAABB mCollision;
        private ComponentPosition mPosition;

        // get list of player and enemies
        List<Entity> mPhysObjs = new List<Entity>();

        public SystemCollisionAABBAABB(CollisionManager pCollisionManager)
        {
            mName = "SystemCollisionAABB_AABB";
            mCollisionManager = pCollisionManager;
        }

        public void GetPhysicsObjects(List<Entity> plist)
        {
            foreach(Entity e in plist)
            {
                ComponentPhysics phys = (ComponentPhysics)GetComponentHelper.GetComponent("ComponentPhysics", e);
                if (phys != null)
                {
                    mPhysObjs.Add(e);
                }
            }
        }

        public void ClearPhysicsObjects()
        {
            mPhysObjs.Clear();
            mPhysObjs = new List<Entity>();
        }

        public override void SystemAction(List<Entity> pEntities, GameTime pGameTime)
        {
            foreach(Entity physObj in mPhysObjs)
            {
                foreach (Entity e in pEntities)
                {
                    mPosition = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, e);

                    if (mPosition != null && e != physObj)
                    {
                        mCollision = (ComponentCollisionAABB)GetComponentHelper.GetComponent(mCollisionComponent, e);

                        if (mCollision != null)
                        {
                            Collide(physObj, e);
                        }
                    }
                }
            }
        }

        private void Collide(Entity pEntity1, Entity pEntity2)
        {

            // Variables for first entity

            ComponentCollisionAABB entity1col = (ComponentCollisionAABB)GetComponentHelper.GetComponent(mCollisionComponent, pEntity1);
            ComponentPosition entity1pos = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, pEntity1);
            int rect1width = entity1col.Width;
            int rect1height = entity1col.Height;
            Vector2 rect1pos = entity1pos.Position;

            // Variables for second entity

            ComponentCollisionAABB entity2col = (ComponentCollisionAABB)GetComponentHelper.GetComponent(mCollisionComponent, pEntity2);
            ComponentPosition entity2pos = (ComponentPosition)GetComponentHelper.GetComponent(mPosComponent, pEntity2);
            int rect2width = entity2col.Width;
            int rect2height = entity2col.Height;
            Vector2 rect2pos = entity2pos.Position;

            if (rect1pos.X < rect2pos.X + rect2width &&
                rect1pos.X + rect1width > rect2pos.X &&
                rect1pos.Y < rect2pos.Y + rect2height &&
                rect1height + rect1pos.Y > rect2pos.Y)
            {
                //Calculate by how much the squares are overlapping.

                Vector2 rect1Centre = new Vector2(rect1pos.X + (rect1width / 2), rect1pos.Y + (rect1height / 2));
                Vector2 rect2Centre = new Vector2(rect2pos.X + (rect2width / 2), rect2pos.Y + (rect2height / 2));
                Vector2 distance = rect1Centre - rect2Centre;

                float xExtentRect1 = rect1width / 2;
                float xExtentRect2 = rect2width / 2;
                float xOverlap = (xExtentRect1 - xExtentRect2) - Math.Abs(distance.X);

                float yExtentRect1 = rect1height / 2;
                float yExtentRect2 = rect2height / 2;
                float yOverlap = (yExtentRect1 - yExtentRect2) - Math.Abs(distance.Y);

                if (yOverlap > xOverlap)
                {
                    if (distance.X > 0)
                    {
                        //life side of player (right side of block)
                        Collision col = new Collision();
                        col.entity1 = pEntity1;
                        col.entity2 = pEntity2;
                        col.type = CollisionType.AABB_AABB_RIGHT;
                        mCollisionManager.RegisterCollision(col);
                    }
                    else
                    {
                        //right side of player (left side of block)
                        Collision col = new Collision();
                        col.entity1 = pEntity1;
                        col.entity2 = pEntity2;
                        col.type = CollisionType.AABB_AABB_LEFT;
                        mCollisionManager.RegisterCollision(col);
                    }
                }
                else
                {
                    if (distance.Y > 0)
                    {
                        //bottom
                        Collision col = new Collision();
                        col.entity1 = pEntity1;
                        col.entity2 = pEntity2;
                        col.type = CollisionType.AABB_AABB_BOTTOM;
                        mCollisionManager.RegisterCollision(col);
                    }
                    else
                    {
                        //top
                        Collision col = new Collision();
                        col.entity1 = pEntity1;
                        col.entity2 = pEntity2;
                        col.type = CollisionType.AABB_AABB_TOP;
                        mCollisionManager.RegisterCollision(col);
                    }
                }
            }
        }
    }
}
