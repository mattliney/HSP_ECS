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
        Entity player;
        Texture2D t;

        public GameScene(SceneManager pSceneManager) : base(pSceneManager)
        {

        }

        public override void Draw()
        {

        }

        public override void Update(GameTime pGameTime)
        {
        }
    }
}
