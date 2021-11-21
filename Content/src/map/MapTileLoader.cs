using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.map
{
    public class MapTileLoader
    {
        public void LoadAll(ContentManager manager)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/rooms");
            string contents = "";

            foreach(string a in files){
                using (Stream stream = TitleContainer.OpenStream(a))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        contents = reader.ReadToEnd();
                        string[] roomContent = contents.Split(";");

                        string connections = roomContent[2];
                        int x = 0, y = 0;
                        List<Tile> tiles = new List<Tile>();
                        for(int i = 3; i < roomContent.Length; i++)
                        {
                            foreach (string str in roomContent[i].Split(".")) {
                                if (str == "") continue;
                                Tile tile = Vars.tilePool[str];
                                Tile tile2 = new Tile(tile.solid, new Vector2(x, y), tile.texture);
                                tiles.Add(tile2);
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
