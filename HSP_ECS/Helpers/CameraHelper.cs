using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HSP_ECS
{
    public static class CameraHelper
    {
        public static Vector2 leftSideOfScreen;
        public static int leftSideBounds;
        public static int rightSideBounds;
        public static Vector2 cameraMovement;

        public static void CameraInit(int pScreenWidth)
        {
            leftSideOfScreen = new Vector2(0, 0);
            cameraMovement= new Vector2(0, 0);

            int halfWidth = pScreenWidth / 2;
            leftSideBounds = halfWidth - 50;
            rightSideBounds = halfWidth;
        }

        public static void CameraUpdate()
        {
            leftSideOfScreen -= cameraMovement;
        }
    }
}
