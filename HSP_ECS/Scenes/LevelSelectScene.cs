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
using System.IO;
using System.Runtime.CompilerServices;

namespace HSP_ECS
{
    public class LevelSelectScene : Scene
    {
        Entity cursor;
        ComponentCollisionPoint cursorPoint;
        ComponentPosition cursorPos;
        List<Entity> levelButtons;
        string[] mFiles;


        public LevelSelectScene(SceneManager pSceneManager) : base(pSceneManager)
        {
            mFiles = Directory.GetFiles("Maps/XML");
            
            levelButtons= new List<Entity>();

            Entity back = new Entity("background");
            back.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("white_background")));
            back.AddComponent(new ComponentPosition(new Vector2(0, 0)));
            mSceneManager.mEntityManager.AddEntity(back);

            for(int i = 0; i < mFiles.Length; i++)
            {
                Entity button = new Entity(i + " button");
                button.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("button")));
                button.AddComponent(new ComponentPosition(new Vector2(448, 210 * i)));
                button.AddComponent(new ComponentCollisionAABB(200, 150));
                button.AddComponent(new ComponentButton());
                button.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
                mSceneManager.mEntityManager.AddEntity(button);
                levelButtons.Add(button);
            }

            Entity up = new Entity("up");
            up.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("forward")));
            up.AddComponent(new ComponentPosition(new Vector2(900, 300)));
            up.AddComponent(new ComponentCollisionAABB(64, 64));
            up.AddComponent(new ComponentButton());
            up.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(up);

            Entity down = new Entity("down");
            down.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("back")));
            down.AddComponent(new ComponentPosition(new Vector2(900, 400)));
            down.AddComponent(new ComponentCollisionAABB(64, 64));
            down.AddComponent(new ComponentButton());
            down.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(down);

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

                if(e.Name == "down" && b.LeftClick)
                {
                    foreach (Entity l in levelButtons)
                    {
                        ComponentPosition pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", l);
                        pos.SetY(pos.Position.Y + 10);
                    }
                }
                else if (e.Name == "up" && b.LeftClick)
                {
                    foreach (Entity l in levelButtons)
                    {
                        ComponentPosition pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", l);
                        pos.SetY(pos.Position.Y - 10);
                    }
                }
                else if(e.Name.Contains("button") && b.LeftClick)
                {
                    int index =int.Parse(e.Name[0].ToString());
                    string file = mFiles[index];

                    mSceneManager.ChangeScene(SceneType.GameScene, file);
                }

                b.LeftClick = false;
                b.RightClick = false;
            }
        }
    }
}
