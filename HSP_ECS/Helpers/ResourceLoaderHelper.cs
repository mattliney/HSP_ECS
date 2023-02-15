using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSP_ECS.Helpers
{
    public class ResourceLoaderHelper
    {
        private ContentManager cm;
        public ResourceLoaderHelper(SceneManager pSceneManager)
        {
            cm = pSceneManager.Content;
        }

        public Texture2D LoadTexture(string pFileName)
        {
            return cm.Load<Texture2D>(pFileName);
        }
    }
}
