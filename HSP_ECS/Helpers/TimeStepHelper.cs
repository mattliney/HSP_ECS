using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HSP_ECS.Helpers
{
    public static class TimeStepHelper
    {
        //Helps to calaulate new values based on the time step.
        //Returns new vectors based on this value for movement in game.

        public static Vector2 CalculateStepVector(Vector2 pVector, GameTime pGameTime)
        {
            float time = (float)pGameTime.ElapsedGameTime.TotalSeconds;

            float newX = pVector.X * time;
            float newY = pVector.Y * time;

            return new Vector2(newX, newY);
        }
    }
}
