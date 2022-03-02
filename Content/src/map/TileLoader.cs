using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using wastelands.src.graphics;

namespace wastelands.src.map
{
    public static class TileLoader
    {
        public static void LoadAll(ContentManager manager, GraphicsDevice device)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/tiles");

            foreach (string a in files)
            {
                string[] split = a.Replace("\\", "/").Replace(".xnb", "").Split("/");
                string name = split[split.Length - 1];
                Vars.tilePool.Add(name, new Tile(name != "F", manager.Load<Texture2D>("sprites/tiles/" + name)));
            }

            TileSetSplitter.SplitTex(device, manager.Load<Texture2D>("sprites/tiles/brick"));
        }
    }
}
