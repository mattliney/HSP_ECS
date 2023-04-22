using HSP_ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Net.Http.Headers;
using HSP_ECS.Helpers;
using HSP_ECS.Systems;

namespace HSP_ECS
{
    public class GameScene : Scene
    {
        Entity mPlayer;

        // parallax varaibles
        ComponentPosition mParallaxNear1;
        ComponentPosition mParallaxNear2;
        ComponentPosition mParallaxFar1;
        ComponentPosition mParallaxFar2;
        int mScreenWidth;

        public GameScene(SceneManager pSceneManager, string pLevelName) : base(pSceneManager)
        {
            // game scene will need to get the file name for the map and then load it.
            MapLoaderHelper.LoadTextMap(pLevelName, mSceneManager.mEntityManager, mSceneManager.mResourceLoader);

            // input manager needs to find the player in the list in order to move him.
            mSceneManager.mInputManager.GetPlayer(mSceneManager.mEntityManager);
            mPlayer = mSceneManager.mEntityManager.GetEntity("player");

            // get parallax variables
            GetParallax();
            mScreenWidth = mSceneManager.ScreenWidth;

            mSceneManager.mSystemCollisionAABBAABB.GetPhysicsObjects(mSceneManager.mEntityManager.Entities);
            mSceneManager.mSystemCollisionAABBPoint.GetPhysicsObjects(mSceneManager.mEntityManager.Entities);
        }

        public override void Draw()
        {
        }

        public override void Update(GameTime pGameTime)
        {
            Parallax(mParallaxFar1, mParallaxFar2);
            Parallax(mParallaxNear1, mParallaxNear2);
        }

        private void GetParallax()
        {
            foreach(Entity e in mSceneManager.mEntityManager.Entities)
            {
                if(e.Name.Contains("Near1"))
                {
                    mParallaxNear1 = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", e);
                }
                else if (e.Name.Contains("Near2"))
                {
                    mParallaxNear2 = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", e);
                }
                else if (e.Name.Contains("Far1"))
                {
                    mParallaxFar1 = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", e);
                }
                else if (e.Name.Contains("Far2"))
                {
                    mParallaxFar2 = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", e);
                }
            }
        }

        private void Parallax(ComponentPosition pBackground1, ComponentPosition pBackground2)
        {
            if (pBackground1.Position.X <= -mScreenWidth)
            {
                pBackground1.SetX(pBackground2.Position.X + mScreenWidth);
            }
            else if (pBackground2.Position.X <= -mScreenWidth)
            {
                pBackground2.SetX(pBackground1.Position.X + mScreenWidth);
            }
            else if (pBackground1.Position.X > 0 && pBackground1.Position.X < pBackground2.Position.X)
            {
                pBackground2.SetX(pBackground1.Position.X - mScreenWidth);
            }
            else if (pBackground2.Position.X > 0)
            {
                pBackground1.SetX(pBackground2.Position.X - mScreenWidth);
            }
        }

        public override void ButtonAction(List<Entity> pButtons)
        {

        }
    }
}
