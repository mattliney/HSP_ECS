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

        List<Entity> mControlButtons;
        bool mUp;

        Texture2D grid;

        public EditorScene(SceneManager pSceneManager) : base(pSceneManager)
        {
            mControlButtons = new List<Entity>();
            mUp= false;

            cursor = new Entity("cursor");
            cursorPoint = new ComponentCollisionPoint(new Vector2(0, 0));
            cursorPos = new ComponentPosition(new Vector2(0, 0));
            cursor.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            cursor.AddComponent(cursorPoint);
            cursor.AddComponent(cursorPos);
            mSceneManager.mEntityManager.AddEntity(cursor);

            MakeButtons();
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

                if(e.Name == "plyr" && b.LeftClick)
                {
                   
                }
                else if (e.Name == "terrain" && b.LeftClick)
                {
                  
                }
                else if (e.Name == "end" && b.LeftClick)
                {
              
                }
                else if (e.Name == "enemy" && b.LeftClick)
                {
                
                }
                else if (e.Name == "save" && b.LeftClick)
                {
               
                }
                else if (e.Name == "back" && b.LeftClick)
                {
                   
                }
                else if (e.Name == "forward" && b.LeftClick)
                {
                 
                }
                else if (e.Name.Contains("gridButton") && b.LeftClick)
                {

                }
                else if(e.Name == "screen" && b.LeftClick)
                {
                    foreach(Entity button in mControlButtons)
                    {
                        ComponentPosition pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", button);
                        Vector2 newPos = pos.Position;
                        float newY = 0;

                        if (mUp)
                        {
                            newY = newPos.Y + 200;
                            pos.SetY(newY);
                        }
                        else
                        {
                            newY = newPos.Y - 200;
                            pos.SetY(newY);
                        }
                    }

                    mUp = !mUp;
                }

                b.LeftClick = false;
                b.RightClick = false;
            }
        }

        private void MakeButtons()
        {
            Entity screen = new Entity("screen");
            screen.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("screen")));
            screen.AddComponent(new ComponentPosition(new Vector2(0, 735)));
            screen.AddComponent(new ComponentCollisionAABB(1088, 100));
            screen.AddComponent(new ComponentButton());
            screen.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(screen);
            mControlButtons.Add(screen);

            Entity plyr = new Entity("plyr");
            plyr.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("player_static")));
            plyr.AddComponent(new ComponentPosition(new Vector2(50, 835)));
            plyr.AddComponent(new ComponentCollisionAABB(64, 64));
            plyr.AddComponent(new ComponentButton());
            plyr.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(plyr);
            mControlButtons.Add(plyr);

            Entity terrain = new Entity("terrain");
            terrain.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("terrain_symbol")));
            terrain.AddComponent(new ComponentPosition(new Vector2(150, 835)));
            terrain.AddComponent(new ComponentCollisionAABB(64, 64));
            terrain.AddComponent(new ComponentButton());
            terrain.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(terrain);
            mControlButtons.Add(terrain);

            Entity enemy = new Entity("enemy");
            enemy.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("enemy_static")));
            enemy.AddComponent(new ComponentPosition(new Vector2(250, 835)));
            enemy.AddComponent(new ComponentCollisionAABB(64, 64));
            enemy.AddComponent(new ComponentButton());
            enemy.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(enemy);
            mControlButtons.Add(enemy);

            Entity end = new Entity("end");
            end.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("endflag_static")));
            end.AddComponent(new ComponentPosition(new Vector2(350, 835)));
            end.AddComponent(new ComponentCollisionAABB(64, 64));
            end.AddComponent(new ComponentButton());
            end.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(end);
            mControlButtons.Add(end);

            Entity save = new Entity("save");
            save.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("save")));
            save.AddComponent(new ComponentPosition(new Vector2(450, 835)));
            save.AddComponent(new ComponentCollisionAABB(64, 64));
            save.AddComponent(new ComponentButton());
            save.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(save);
            mControlButtons.Add(save);

            Entity back = new Entity("back");
            back.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("back")));
            back.AddComponent(new ComponentPosition(new Vector2(550, 835)));
            back.AddComponent(new ComponentCollisionAABB(64, 64));
            back.AddComponent(new ComponentButton());
            back.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(back);
            mControlButtons.Add(back);

            Entity forward = new Entity("forward");
            forward.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("forward")));
            forward.AddComponent(new ComponentPosition(new Vector2(650, 835)));
            forward.AddComponent(new ComponentCollisionAABB(64, 64));
            forward.AddComponent(new ComponentButton());
            forward.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            mSceneManager.mEntityManager.AddEntity(forward);
            mControlButtons.Add(forward);

            for(int x = 0; x < 17; x++)
            {
                for(int y = 0; y < 12; y++)
                {
                    Entity gridButton = new Entity("gridButton_" + x + "_" + y);
                    gridButton.AddComponent(new ComponentPosition(new Vector2(x * 64, y * 64)));
                    gridButton.AddComponent(new ComponentCollisionAABB(64, 64));
                    gridButton.AddComponent(new ComponentButton());
                    gridButton.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
                    mSceneManager.mEntityManager.AddEntity(gridButton);
                    mSceneButtons.Add(gridButton);
                }
            }
        }
    }
}
