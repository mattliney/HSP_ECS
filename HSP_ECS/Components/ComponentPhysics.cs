using Microsoft.Xna.Framework;

namespace HSP_ECS.Components
{
    public class ComponentPhysics : Component
    {
        private Vector2 mVelocity;
        private float mCurrentGravity;
        private float mGravityDefault;

        public ComponentPhysics(Vector2 pVel, float pGravity)
        {
            mVelocity = pVel;
            mName = "ComponentPhysics";
            mGravityDefault= pGravity;
            mCurrentGravity= pGravity;
        }

        public Vector2 Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
        }

        public void SetVelX(float pX)
        {
            mVelocity.X = pX;
        }
        public void SetVelY(float pY)
        {
            mVelocity.Y = pY;
        }

        // if this entity has touched the ground, set the current gravity to 0
        public void StopAccel()
        {
            mCurrentGravity = 0;
            mVelocity.Y = 0;
        }

        // if the entity is in the air, the gravity value needs to be reset to the default
        public void RestartAccel()
        {
            mCurrentGravity = mGravityDefault;
        }

        public float Gravity
        {
            get { return mCurrentGravity; }
        }
    }
}
