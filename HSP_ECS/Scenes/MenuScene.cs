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
    public class MenuScene : Scene
    {
        Entity cursor;
        ComponentCollisionPoint cursorPoint;
        ComponentPosition cursorPos;


        public MenuScene(SceneManager pSceneManager) : base(pSceneManager)
        {

            Entity back = new Entity("background");
            back.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("white_background")));
            back.AddComponent(new ComponentPosition(new Vector2(0, 0)));
            mSceneManager.mEntityManager.AddEntity(back);

            Entity levels = new Entity("levels");
            levels.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("levels")));
            levels.AddComponent(new ComponentPosition(new Vector2(448, 100)));
            levels.AddComponent(new ComponentCollisionAABB(200, 150));
            levels.AddComponent(new ComponentButton());
            levels.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(levels);

            Entity editor = new Entity("editor");
            editor.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("editor")));
            editor.AddComponent(new ComponentPosition(new Vector2(448, 500)));
            editor.AddComponent(new ComponentCollisionAABB(200, 150));
            editor.AddComponent(new ComponentButton());
            editor.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(editor);

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
                if(b.LeftClick && e.Name == "levels")
                {
                    b.LeftClick = false;
                    b.RightClick = false;
                    mSceneManager.ChangeScene(SceneType.GameScene, "Maps/XML/testmap.xml");
                }
                else if(b.LeftClick && e.Name == "editor")
                {
                    b.LeftClick = false;
                    b.RightClick = false;
                    mSceneManager.ChangeScene(SceneType.EditorScene, "");
                }

                b.LeftClick = false;
                b.RightClick = false;
            }
        }
    }
}
