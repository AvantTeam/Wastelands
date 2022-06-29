using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System;

namespace wastelands.src.map
{
    public static class MapTileLoader
    {
        public static void LoadAll(ContentManager manager)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/rooms");
            string contents;

            foreach (string a in files)
            {
                using (Stream stream = TitleContainer.OpenStream(a))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        contents = reader.ReadToEnd();
                        string[] roomContent = contents.Split(";");

                        string biome = roomContent[1];
                        string connections = roomContent[2];

                        Dictionary<Vector2, string> tiles = new Dictionary<Vector2, string>();

                        for (int y = 3; y < roomContent.Length; y++)
                        {
                            string[] columns = roomContent[y].Split(".");

                            for (int x = 0; x < columns.Length; x++)
                            {
                                string str = columns[x];

                                if (str == "") continue;

                                tiles.Add(new Vector2(x, y), str);
                            }
                        }

                        if (biome == "any")
                        {
                            foreach (string str in Vars.mapTilePool.Keys)
                            {
                                Vars.mapTilePool[str].Add(connections, tiles);
                            }
                        }
                        else
                        {
                            Vars.mapTilePool[biome].Add(connections, tiles);
                        }
                    }
                }
            }
        }
    }
}
