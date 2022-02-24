using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace wastelands.src.map
{
    public static class TileLoader
    {
        public static void LoadAll(ContentManager manager)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/tiles");

            foreach (string a in files)
            {
                string[] split = a.Replace("\\", "/").Replace(".xnb", "").Split("/");
                string name = split[split.Length - 1];
                Console.WriteLine(name);
                Vars.tilePool.Add(name, new Tile(name != "F", manager.Load<Texture2D>("tiles/" + name)));
            }
        }
    }
}
