using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class InputManager
    {
        private SceneManager mSceneManager;

        public InputManager(SceneManager pSceneManager)
        {
            mSceneManager = pSceneManager;
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
                CameraHelper.cameraMovement = new Vector2(500, 0);
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                CameraHelper.cameraMovement = new Vector2(-500, 0);
            }
            else
            {
                CameraHelper.cameraMovement = new Vector2(0, 0);
            }
        }
    }
}
