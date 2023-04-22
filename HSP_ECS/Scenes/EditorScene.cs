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
    public class EditorScene : Scene
    {
        Entity cursor;
        ComponentCollisionPoint cursorPoint;
        ComponentPosition cursorPos;

        Texture2D grid;

        public EditorScene(SceneManager pSceneManager) : base(pSceneManager)
        {
            cursor = new Entity("cursor");
            cursorPoint = new ComponentCollisionPoint(new Vector2(0, 0));
            cursorPos = new ComponentPosition(new Vector2(0, 0));
            cursor.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            cursor.AddComponent(cursorPoint);
            cursor.AddComponent(cursorPos);

            mSceneManager.mEntityManager.AddEntity(cursor);

            GetButtons();
            mSceneManager.mSystemCollisionAABBPoint.GetPhysicsObjects(mSceneManager.mEntityManager.Entities);

            grid = mSceneManager.mResourceLoader.GetTexture("grid");
        }

        public override void Draw()
        {
            mSceneManager.SpriteBatch.Draw(grid, Vector2.Zero, Color.White);
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

                b.LeftClick = false;
                b.RightClick = false;
            }
        }
    }
}
