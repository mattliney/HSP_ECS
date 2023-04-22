using HSP_ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public enum SceneType
    {
        TitleScene,
        GameScene,
        MenuScene,
        EditorScene
    }

    public abstract class Scene
    {
        protected List<Entity> mSceneButtons;
        protected SceneManager mSceneManager;
        protected string mName;

        public Scene(SceneManager pSceneManager)
        {
            mSceneManager = pSceneManager;
            mSceneButtons= new List<Entity>();
        }

        // Scenes need a draw and update method. These are assigned as delegates in the scene manager class.
        // Takes in a scene manager as a member as it will need some of the variables from that class.

        public abstract void Draw();

        public abstract void Update(GameTime pGameTime);

        public string Name
        { 
            get 
            { 
                return mName; 
            } 
        }

        public void GetButtons()
        {
            foreach(Entity e in mSceneManager.mEntityManager.Entities)
            {
                ComponentButton b = (ComponentButton)GetComponentHelper.GetComponent("ComponentButton", e);
                if(b != null)
                {
                    mSceneButtons.Add(e);
                }
            }
        }

        public abstract void ButtonAction(List<Entity> pButtons);
    }
}
