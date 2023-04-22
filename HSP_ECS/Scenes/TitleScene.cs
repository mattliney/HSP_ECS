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
using Microsoft.Xna.Framework.Input;

namespace HSP_ECS
{
    public class TitleScene : Scene
    {
        Entity cursor;
        ComponentCollisionPoint cursorPoint;
        ComponentPosition cursorPos;


        public TitleScene(SceneManager pSceneManager) : base(pSceneManager)
        {
            Entity title = new Entity("title");
            title.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("title")));
            title.AddComponent(new ComponentPosition(new Vector2(0, 0)));
            mSceneManager.mEntityManager.AddEntity(title);

            Entity start = new Entity("start");
            start.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("start")));
            start.AddComponent(new ComponentPosition(new Vector2(448, 500)));
            start.AddComponent(new ComponentCollisionAABB(200, 150));
            start.AddComponent(new ComponentButton());
            start.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(start);

            cursor = new Entity("cursor");
            cursorPoint = new ComponentCollisionPoint(new Vector2(0, 0));
            cursorPos = new ComponentPosition(new Vector2(0, 0));
            cursor.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            cursor.AddComponent(cursorPoint);
            cursor.AddComponent(cursorPos);

            mSceneManager.mEntityManager.AddEntity(cursor);

            GetButtons();
            mSceneManager.mSystemCollisionAABBPoint.GetPhysicsObjects(mSceneManager.mEntityManager.Entities);
        }

        public override void Draw()
        {

        }

        public override void Update(GameTime pGameTime)
        {
            MouseState ms = Mouse.GetState();
            Vector2 pos = new Vector2(ms.X, ms.Y);
            cursorPos.Position= pos;

            ButtonAction(mSceneButtons);
        }

        public override void ButtonAction(List<Entity> pButtons)
        {
            foreach(Entity e in mSceneButtons)
            {
                ComponentButton b = (ComponentButton)GetComponentHelper.GetComponent("ComponentButton", e);

                // check if the button has been left or right clicked
                if(b.LeftClick)
                {
                    b.LeftClick = false;
                    b.RightClick = false;
                    mSceneManager.ChangeScene(SceneType.MenuScene, "");
                }

                b.LeftClick = false;
                b.RightClick = false;
            }
        }
    }
}
