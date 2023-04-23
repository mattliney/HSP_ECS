using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS
{
    public class ResourceLoaderHelper
    {
        private ContentManager cm;
        private Dictionary<string, Texture2D> mTextureDictionary = new Dictionary<string, Texture2D>();
        public ResourceLoaderHelper(SceneManager pSceneManager)
        {
            cm = pSceneManager.Content;
        }

        public void LoadTexture(string pFileName)
        {
            Texture2D tex = cm.Load<Texture2D>(pFileName);
            mTextureDictionary.Add(pFileName, tex);
        }

        public Texture2D GetTexture(string pName)
        {
            return mTextureDictionary[pName];
        }

        public Song LoadAudio(string pFileName)
        {
            return cm.Load<Song>(pFileName);
        }

        public void LoadAllTextures()
        {
            this.LoadTexture("grass");
            this.LoadTexture("grassbottom");
            this.LoadTexture("grasstop");
            this.LoadTexture("playerTemp");
            this.LoadTexture("terrain_grass");
            this.LoadTexture("terrain_moon");
            this.LoadTexture("terrain_sand");
            this.LoadTexture("background_grass_far");
            this.LoadTexture("background_grass_near");
            this.LoadTexture("title");
            this.LoadTexture("start");
            this.LoadTexture("white_background");
            this.LoadTexture("levels");
            this.LoadTexture("editor");
            this.LoadTexture("grid");
            this.LoadTexture("player_static");
            this.LoadTexture("endflag_static");
            this.LoadTexture("terrain_symbol");
            this.LoadTexture("back");
            this.LoadTexture("forward");
            this.LoadTexture("screen");
            this.LoadTexture("save");
            this.LoadTexture("enemy_static");
        }
    }
}
