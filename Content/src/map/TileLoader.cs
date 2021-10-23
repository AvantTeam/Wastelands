using System.IO;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.map
{
    public class TileLoader
    {
        public List<MapTile> loadedTiles = new List<MapTile>();

        public MapTile LoadAll(ContentManager manager)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/rooms");
            string contents = "";

            foreach(string a in files){
                using (Stream stream = TitleContainer.OpenStream(a))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        contents = reader.ReadToEnd();
                        Console.WriteLine(contents);
                    }
                }
            }


            return null;
        }
    }
}
