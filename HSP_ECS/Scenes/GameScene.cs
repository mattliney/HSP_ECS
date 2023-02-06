using HSP_ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HSP_ECS.Scenes
{
    public class GameScene : Scene
    {
        Entity player;

        public GameScene(SceneManager pSceneManager, string pName) : base(pSceneManager, pName)
        {
            player = new Entity("player");
            ComponentPosition pos = new ComponentPosition(new Vector2(100,100));
            player.mComponents.Add(pos);
        }

        public override void Draw()
        {
        }

        public override void Update()
        {
        }
    }
}
