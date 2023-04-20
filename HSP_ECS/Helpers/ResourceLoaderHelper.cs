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
        }
    }
}
