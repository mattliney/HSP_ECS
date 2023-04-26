using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSP_ECS.Components;

namespace HSP_ECS
{
    public class InputManager
    {
        private SceneManager mSceneManager;
        private Entity mPlayer;
        private ComponentPhysics mPlayerPhys;
        private ComponentPosition mPlayerPos;
        private ComponentPlayer mPlayerComp;
        private int mSpeed;
        private bool mNoPlayer;

        public InputManager(SceneManager pSceneManager)
        {
            mSceneManager = pSceneManager;
            mSpeed = 400;
            mNoPlayer = true;
        }

        public void GetPlayer(EntityManager pEntityManager)
        {
            foreach(Entity e in pEntityManager.Entities)
            {
                if(e.Name == "player")
                {
                    mPlayer = e;
                    mPlayerPhys = (ComponentPhysics)GetComponentHelper.GetComponent("ComponentPhysics", e);
                    mPlayerPos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", e);
                    mPlayerComp = (ComponentPlayer)GetComponentHelper.GetComponent("ComponentPlayer", e);
                    mNoPlayer = false;
                    return;
                }
            }
        }

        public void ClearPlayer()
        {
            mPlayer = null;
            mNoPlayer = true;
        }

        public void ProcessInputs()
        {
            KeyboardState ks = Keyboard.GetState();
            if (mNoPlayer == false)
            {

                if (ks.IsKeyDown(Keys.W))
                {
                    if(mPlayerComp.HasJumped == false)
                    {
                        mPlayerPhys.SetVelY(-mSpeed);
                        mPlayerComp.HasJumped = true;
                    }
                }

                if (ks.IsKeyDown(Keys.A))
                {
                    if (mPlayerPos.Position.X > CameraHelper.leftSideBounds || CameraHelper.leftSideOfScreen.X <= 0)
                    {
                        mPlayerPhys.SetVelX(-mSpeed);
                        CameraHelper.cameraMovement = new Vector2(0, 0);
                    }
                    else
                    {
                        mPlayerPhys.SetVelX(0);
                        CameraHelper.cameraMovement = new Vector2(mSpeed, 0);
                    }
                }
                else if (ks.IsKeyDown(Keys.D))
                {
                    if (mPlayerPos.Position.X < CameraHelper.rightSideBounds)
                    {
                        mPlayerPhys.SetVelX(mSpeed);
                        CameraHelper.cameraMovement = new Vector2(0, 0);
                    }
                    else
                    {
                        mPlayerPhys.SetVelX(0);
                        CameraHelper.cameraMovement = new Vector2(-mSpeed, 0);
                    }
                }
                else
                {
                    CameraHelper.cameraMovement = new Vector2(0, 0);
                    mPlayerPhys.SetVelX(0);
                }
            }

            if (ks.IsKeyDown(Keys.P))
            {
                mSceneManager.ChangeScene(SceneType.TitleScene, "");
            }
        }
    }
}
