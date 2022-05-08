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
            string contents = "";

            foreach (string a in files)
            {
                using (Stream stream = TitleContainer.OpenStream(a))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        contents = reader.ReadToEnd();
                        string[] roomContent = contents.Split(";");

                        string connections = roomContent[2];
                        int x = 0, y = 0;
                        Dictionary<Vector2, string> tiles = new Dictionary<Vector2, string>();
                        for (int i = 3; i < roomContent.Length; i++)
                        {
                            foreach (string str in roomContent[i].Split("."))
                            {
                                if (str == "") continue;

                                tiles.Add(new Vector2(x, y), str == "F" ? "F" : str.Contains("s") ? "s" : "W");

                                x += 1;
                            }

                            y += 1;
                            x = 0;
                        }
                        Vars.mapTilePool.Add(connections, tiles);
                    }
                }
            }
        }
    }
}
