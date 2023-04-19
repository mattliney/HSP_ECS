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


        public GameScene(SceneManager pSceneManager, string pLevelName) : base(pSceneManager)
        {
            // game scene will need to get the file name for the map and then load it.
            MapLoaderHelper.LoadTextMap(pLevelName, mSceneManager.mEntityManager, mSceneManager.mResourceLoader);

            // input manager needs to find the player in the list in order to move him.
            mSceneManager.mInputManager.GetPlayer(mSceneManager.mEntityManager);

            mPlayer = mSceneManager.mEntityManager.GetEntity("player");
        }

        public override void Draw()
        {

        }

        public override void Update(GameTime pGameTime)
        {
        }
    }
}
