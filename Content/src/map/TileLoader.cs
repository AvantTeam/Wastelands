using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.map
{
    public class TileLoader
    {
        public void LoadAll(ContentManager manager)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/tiles");
            string contents = "";

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
