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
using System.Xml;

namespace HSP_ECS
{
    public class EditorScene : Scene
    {
        // Cursor variables
        Entity cursor;
        ComponentCollisionPoint cursorPoint;
        ComponentPosition cursorPos;

        // Lists and Array
        List<Entity> mControlButtons;
        List<Entity> mGridButtons;
        List<Entity> mArrayEntities;
        char[,] mArray;

        char mCurrentBlock;
        Texture2D mCurrentBlockTexture;
        int mCurrentScreen;
        bool mUp;
        bool mPlayer;
        bool mEnd;

        public EditorScene(SceneManager pSceneManager) : base(pSceneManager)
        {
            mPlayer = false;
            mEnd= false;
            mCurrentScreen= 0;
            mCurrentBlock = 'T';
            mCurrentBlockTexture = mSceneManager.mResourceLoader.GetTexture("terrain_grass");
            mControlButtons = new List<Entity>();
            mGridButtons = new List<Entity>();
            mArrayEntities = new List<Entity>();
            mArray = new char[68, 12];
            mUp= false;

            cursor = new Entity("cursor");
            cursorPoint = new ComponentCollisionPoint(new Vector2(0, 0));
            cursorPos = new ComponentPosition(new Vector2(0, 0));
            cursor.AddComponent(new ComponentPhysics(new Vector2(0, 0), 0));
            cursor.AddComponent(cursorPoint);
            cursor.AddComponent(cursorPos);
            mSceneManager.mEntityManager.AddEntity(cursor);

            Entity grid = new Entity("grid");
            grid.AddComponent(new ComponentPosition(new Vector2(0,0)));
            grid.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("grid")));
            mSceneManager.mEntityManager.AddEntity(grid);

            MakeButtons();
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

                if(e.Name == "plyr" && b.LeftClick)
                {
                    mCurrentBlock = 'P';
                    mCurrentBlockTexture = mSceneManager.mResourceLoader.GetTexture("player_static");
                }
                else if (e.Name == "terrain" && b.LeftClick)
                {
                    mCurrentBlock = 'T';
                    mCurrentBlockTexture = mSceneManager.mResourceLoader.GetTexture("terrain_grass");
                }
                else if (e.Name == "end" && b.LeftClick)
                {
                    mCurrentBlock = 'F';
                    mCurrentBlockTexture = mSceneManager.mResourceLoader.GetTexture("endflag_static");
                }
                else if (e.Name == "enemy" && b.LeftClick)
                {
                    mCurrentBlock = 'E';
                    mCurrentBlockTexture = mSceneManager.mResourceLoader.GetTexture("enemy_static");
                }
                else if (e.Name == "save" && b.LeftClick)
                {
                    SaveLevel();
                }
                else if (e.Name == "back" && b.LeftClick)
                {
                   if(mCurrentScreen > 0)
                    {
                        mCurrentScreen--;

                        foreach(Entity ae in mArrayEntities)
                        {
                            ComponentPosition pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", ae);
                            pos.SetX(pos.Position.X + 1088);
                        }

                    }
                }
                else if (e.Name == "forward" && b.LeftClick)
                {
                    if (mCurrentScreen < 3)
                    {
                        mCurrentScreen++;

                        foreach (Entity ae in mArrayEntities)
                        {
                            ComponentPosition pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", ae);
                            pos.SetX(pos.Position.X - 1088);
                        }
                    }
                }
                else if ((e.Name.Contains("gridButton") && b.LeftClick) || (e.Name.Contains("gridButton") && b.RightClick))
                {
                    ComponentPosition pos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", e);
                    Vector2 position = pos.Position;

                    float x = (position.X / 64);
                    float y = (position.Y / 64);

                    foreach (Entity arrayEntity in mArrayEntities)
                    {
                        ComponentPosition aePos = (ComponentPosition)GetComponentHelper.GetComponent("ComponentPosition", arrayEntity);
                        Vector2 aePosition = aePos.Position;

                        float aeX = (aePosition.X / 64);
                        float aeY = (aePosition.Y / 64);

                        if (x == aeX && y == aeY)
                        {
                            int xOffset = (int)x + (mCurrentScreen * 17);

                            ComponentSprite sprite = (ComponentSprite)GetComponentHelper.GetComponent("ComponentSprite", arrayEntity);
                            if(b.LeftClick)
                            {
                                sprite.Sprite = mCurrentBlockTexture;
                                mArray[xOffset ,(int)y] = mCurrentBlock;
                            }
                            else if(b.RightClick)
                            {
                                sprite.Sprite = mSceneManager.mResourceLoader.GetTexture("blank");
                                mArray[xOffset, (int)y] = 'O';
                            }
                        }
                    }

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
                    mGridButtons.Add(gridButton);
                }
            }

            for(int x = 0; x < 68; x++)
            {
                for(int y = 0; y < 12; y++)
                {
                    Entity arrayEntity = new Entity(x + " " + y);
                    arrayEntity.AddComponent(new ComponentPosition(new Vector2(x * 64, y * 64)));
                    arrayEntity.AddComponent(new ComponentSprite(mSceneManager.mResourceLoader.GetTexture("blank")));
                    mArrayEntities.Add(arrayEntity);
                    mSceneManager.mEntityManager.InsertEntity(arrayEntity, 0);
                    mArray[x, y] = 'O';
                }
            }
        }

        private void SaveLevel()
        {
            Random rand = new Random();
            string name = "";
            for (int i = 0; i < 10; i++)
            {
                string rng = rand.Next(0, 255).ToString();
                name += rng;
            }

            XmlWriter xmlW = XmlWriter.Create("Maps/XML/" + name + ".xml");
            xmlW.WriteStartElement("map");

            for (int y = 0; y < 12; y++)
            {
                xmlW.WriteStartElement("layer" + y);
                string layer = "";

                for (int x = 0; x < 68; x++)
                {
                    layer += mArray[x, y];
                }

                xmlW.WriteString(layer);
                xmlW.WriteEndElement();
            }

            xmlW.WriteStartElement("theme");
            xmlW.WriteStartElement("grass");
            xmlW.WriteEndElement();

            xmlW.WriteEndDocument();
            xmlW.Close();

            mSceneManager.ChangeScene(SceneType.TitleScene, "");
        }
    }
}
