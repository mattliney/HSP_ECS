using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public abstract class Scene
    {
        protected SceneManager mSceneManager;
        protected string mName;

        public Scene(SceneManager pSceneManager, string pName)
        {
            mSceneManager = pSceneManager;
            mName = pName;
        }

        // Scenes need a draw and update method. These are assigned as delegates in the scene manager class.
        // Takes in a scene manager as a member as it will need some of the variables from that class.

        public abstract void Draw();

        public abstract void Update();

        public string Name
        { 
            get 
            { 
                return mName; 
            } 
        }
    }
}
