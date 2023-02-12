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

namespace HSP_ECS
{
    public class GameScene : Scene
    {
        Entity player;

        public GameScene(SceneManager pSceneManager) : base(pSceneManager)
        {
            mName = "GameScene";
            player = new Entity("playor");

            // Make GetComponentHelper

        }

        public override void Draw()
        {
            mSceneManager.SpriteBatch.Begin();



            mSceneManager.SpriteBatch.End();
        }

        public override void Update()
        {
        }
    }
}
