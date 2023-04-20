using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HSP_ECS.Components;
using Microsoft.Xna.Framework;

namespace HSP_ECS
{
    public static class MapLoaderHelper
    {
        public static void LoadTextMap(string pFileName, EntityManager pEM, ResourceLoaderHelper pResources)
        {
            StreamReader sr = new StreamReader(pFileName);
            string line = sr.ReadLine();
            int xOffset = 0;
            int yOffset = 2;
            int terrainIndex = 0;

            char[,] array = new char[line.Length, 12];
            int length = line.Length;

            // Needs to go into an array first so we can check the coordinates later.

            for (int y = 0; y < 10 && line != null; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    if (line[x] == 'T')
                    {
                        array[x, y] = 'T';
                    }
                    else
                    {
                        array[x, y] = 'O';
                    }

                }
                line = sr.ReadLine();
            }

            // Create the entities based on what is above them.

            ComponentSprite sp;
            ComponentPosition pos;
            Entity e;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    if (array[x, y] == 'T')
                    {
                        sp = new ComponentSprite(pResources.GetTexture("terrain_grass"));
                        e = new Entity("terrain" + terrainIndex);

                        if (ValidCoordChecker(x, y - 1, length, 12))
                        {
                            if (array[x, y - 1] == 'T')
                            {
                                sp = new ComponentSprite(pResources.GetTexture("terrain_grass"));
                            }
                        }
                        pos = new ComponentPosition(new Vector2(xOffset * 64, yOffset * 64));
                        e.AddComponent(sp); e.AddComponent(pos);
                        e.AddComponent(new ComponentCollisionAABB(64, 64));
                        pEM.AddEntity(e);
                    }
                    else if (array[x,y] == 'O')
                    {
                        if (ValidCoordChecker(x, y + 1, length, 12))
                        {
                            if (array[x, y + 1] == 'T')
                            {
                                e = new Entity("grasstop" + terrainIndex);
                                sp = new ComponentSprite(pResources.GetTexture("grasstop"));
                                pos = new ComponentPosition(new Vector2(xOffset * 64, yOffset * 64));
                                e.AddComponent(sp); e.AddComponent(pos);
                                //pEM.AddEntity(e);
                            }
                        }
                    }
                    xOffset++;

                }
                yOffset++;
                xOffset = 0;
            }

            sr.Close();

            Entity en = new Entity("player");
            en.AddComponent(new ComponentSprite(pResources.GetTexture("playerTemp")));
            en.AddComponent(new ComponentPosition(new Vector2(0, 300)));
            en.AddComponent(new ComponentPhysics(new Vector2(0, 0), 10f));
            en.AddComponent(new ComponentCollisionAABB(64, 64));
            en.AddComponent(new ComponentCollisionPoint(new Vector2(32, 66)));
            en.AddComponent(new ComponentCollisionPoint(new Vector2(49, 66)));
            en.AddComponent(new ComponentCollisionPoint(new Vector2(10, 66)));

            pEM.AddEntity(en);
        }

        private static bool ValidCoordChecker(int pX, int pY, int pArrayWidth, int pArrayHeight)
        {
            if(pX >= pArrayWidth || pX < 0)
            {
                return false;
            }
            else if(pY >= pArrayHeight || pY < 0)
            {
                return false;
            }

            return true;
        }
    }
}
