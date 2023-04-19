﻿using HSP_ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class SystemCollisionAABBPoint : System
    {
        private string mAABBCollision = "ComponentCollisionAABB";
        private string mPointCollision = "ComponentCollisionPoint";

        private CollisionManager mCollisionManager;
        private ComponentCollisionPoint mCollisionPoint;
        private ComponentCollisionAABB mCollisionAABB;

        // get list of player and enemies
        List<Entity> mPhysObjs = new List<Entity>();

        public SystemCollisionAABBPoint(CollisionManager pCollisionManager)
        {
            mName = "SystemCollisionAABB_Point";
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

        public override void SystemAction(List<Entity> pEntities, GameTime pGameTime)
        {
            foreach(Entity physObj in mPhysObjs)
            {
                foreach (Entity e in pEntities)
                {
                    mCollisionAABB = (ComponentCollisionAABB)GetComponentHelper.GetComponent(mAABBCollision, e);

                    if (mCollisionAABB != null && e != physObj)
                    {
                        mCollisionPoint = (ComponentCollisionPoint)GetComponentHelper.GetComponent(mPointCollision, e);

                        if (mCollisionPoint != null)
                        {
                            Collide(physObj, e);
                        }
                    }
                }
            }
        }

        private void Collide(Entity pEntity1, Entity pEntity2)
        {
            List<ComponentCollisionPoint> points = new List<ComponentCollisionPoint>();

            foreach(Component c in pEntity1.mComponents)
            {
                if(c.Name == "ComponentCollisionPoint")
                {
                    points.Add((ComponentCollisionPoint)c);
                }
            }

            ComponentCollisionAABB e2coll = (ComponentCollisionAABB)GetComponentHelper.GetComponent(mAABBCollision, pEntity2);
            ComponentPosition e2pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", pEntity2);

            Vector2 collisionTopLeft = e2pos.Position;
            Vector2 collisionBottomRight = new Vector2(collisionTopLeft.X + e2coll.Width, collisionTopLeft.Y + e2coll.Height);

            foreach(ComponentCollisionPoint p in points)
            {
                Vector2 point = p.Point;

                if (point.X >= collisionTopLeft.X && point.X <= collisionBottomRight.X)
                {
                    if (point.Y >= collisionTopLeft.Y && point.Y <= collisionBottomRight.Y)
                    {
                        Collision col = new Collision();
                        col.entity1 = pEntity1;
                        col.entity2 = pEntity2;
                        col.type = CollisionType.POINT_AABB;
                        mCollisionManager.RegisterCollision(col);
                        return;
                    }
                }
            }
        }
    }
}