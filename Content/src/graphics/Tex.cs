using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace wastelands.src.graphics
{
    public static class Tex
    {
        private static Dictionary<string, NinePatch> textures = new Dictionary<string, NinePatch>();
        private static NinePatch def;

        public static void Load(ContentManager manager)
        {
            def = new NinePatch(manager.Load<Texture2D>("sprites/error"), 1, 1, 1, 1);

            textures.Add("hud1", new NinePatch(manager.Load<Texture2D>("sprites/UI/hud1.9"), 0, 8, 0, 8));
            textures.Add("bar", new NinePatch(manager.Load<Texture2D>("sprites/UI/bar.9"), 1, 1, 1, 1));
        }

        public static NinePatch Get(string name)
        {
            if (textures.ContainsKey(name)) return textures[name];
            return def;
        }
    }
}
