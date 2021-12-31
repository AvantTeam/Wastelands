using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace wastelands.src.graphics
{
    public class Tex
    {
        private Dictionary<string, NinePatch> textures = new Dictionary<string, NinePatch>();
        private NinePatch def;

        public Tex(ContentManager manager)
        {
            def = new NinePatch(manager.Load<Texture2D>("sprites/error"), 1, 1, 1, 1);

            textures.Add("ninepatch-test", new NinePatch(manager.Load<Texture2D>("sprites/UI/ninepatch-test.9"), 5, 5, 5, 5));
        }

        public NinePatch Get(string name)
        {
            if (textures.ContainsKey(name)) return textures[name];
            return def;
        }
    }
}
