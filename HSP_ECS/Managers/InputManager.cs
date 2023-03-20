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
        private ComponentVelocity mPlayerVel;
        private ComponentPosition mPlayerPos;

        public InputManager(SceneManager pSceneManager)
        {
            mSceneManager = pSceneManager;
        }

        public void GetPlayer(EntityManager pEntityManager)
        {
            foreach(Entity e in pEntityManager.Entities)
            {
                if(e.Name == "player")
                {
                    mPlayer = e;
                    mPlayerVel = (ComponentVelocity)GetComponentHelper.GetComponent("ComponentVelocity", e);
                    mPlayerPos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", e);
                    return;
                }
            }
        }

        public void ProcessInputs()
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W))
            {
                // jump!
            }

            if (ks.IsKeyDown(Keys.A))
            {
                if (mPlayerPos.Position.X > CameraHelper.leftSideBounds || CameraHelper.leftSideOfScreen.X <= 0)
                {
                    mPlayerVel.Velocity = new Vector2(-200, 0);
                    CameraHelper.cameraMovement = new Vector2(0, 0);
                }
                else
                {
                    mPlayerVel.Velocity = new Vector2(0, 0);
                    CameraHelper.cameraMovement = new Vector2(500, 0);
                }
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                if (mPlayerPos.Position.X < CameraHelper.rightSideBounds)
                {
                    mPlayerVel.Velocity = new Vector2(200, 0);
                    CameraHelper.cameraMovement = new Vector2(0, 0);
                }
                else
                {
                    mPlayerVel.Velocity = new Vector2(0, 0);
                    CameraHelper.cameraMovement = new Vector2(-500, 0);
                }
            }
            else
            {
                CameraHelper.cameraMovement = new Vector2(0, 0);
                mPlayerVel.Velocity = new Vector2(0, 0);
            }
        }
    }
}
