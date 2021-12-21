using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace wastelands.src.graphics
{
    public class TextureAtlas
    {
        public List<Texture2D> textures = new List<Texture2D>();
        public Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();

        public Texture2D Find(String name)
        {
            return new Texture2D(null, 0, 0);
        }
    }
}
