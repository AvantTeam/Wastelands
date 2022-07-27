using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using wastelands.src.map;
using wastelands.src.utils;

namespace wastelands.src.graphics
{
    public static class TileSetSplitter
    {
        public static List<Tile> SplitTex(string name, GraphicsDevice device, Texture2D tex)
        {
            List<Tile> output = new List<Tile>();
            Color[] texData = new Color[tex.Width * tex.Height];
            tex.GetData(texData);

            for(int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    if (y == 3 && x == 11) continue;

                    List<Color> newCol = new List<Color>();
                    Texture2D newTex = new Texture2D(device, 16, 16);

                    for (int line = 0; line < 16; line++)
                    {
                        newCol.AddRange(texData.Skip(y * 16 * 12 * 16 + x * 16 + line * 16 * 12).Take(16).ToArray());
                    }

                    newTex.SetData(newCol.ToArray(), 0, 16 * 16);

                    output.Add(new Tile(name + ";" + (x + y * 12).ToString(), true, newTex));
                }
            }

            return output;
        }

        public static List<Tile> SplitShadow(string name, GraphicsDevice device, Texture2D tex)
        {
            List<Tile> output = new List<Tile>();
            Color[] texData = new Color[tex.Width * tex.Height];
            tex.GetData(texData);

            for (int x = 0; x < 4; x++)
            {
                List<Color> newCol = new List<Color>();
                Texture2D newTex = new Texture2D(device, 16, 16);

                for (int line = 0; line < 16; line++)
                {
                    newCol.AddRange(texData.Skip(x * 16 + line * 16 * 4).Take(16).ToArray());
                }

                newTex.SetData(newCol.ToArray(), 0, 16 * 16);

                output.Add(new Tile(name + ";s" + x.ToString(), false, newTex));
            }

            return output;
        }

        public static List<Tile> SplitFloor(string name, GraphicsDevice device, Texture2D tex)
        {
            List<Tile> output = new List<Tile>();
            Color[] texData = new Color[tex.Width * tex.Height];
            tex.GetData(texData);

            for (int stainLevel = 0; stainLevel < 4; stainLevel++)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        List<Color> newCol = new List<Color>();
                        Texture2D newTex = new Texture2D(device, 16, 16);

                        for (int line = 0; line < 16; line++)
                        {
                            newCol.AddRange(texData.Skip(x * 16 + y * 16 * 3 * 16 + line * 16 * 3).Take(16).ToArray());
                        }

                        if (stainLevel > 0)
                        {
                            for(int q = 0; q < newCol.Count; q++)
                            {
                                newCol[q] = GraphicUtils.MultiplyColor(newCol[q], 1f - stainLevel * 0.1f, 1f);
                            }
                        }

                        newTex.SetData(newCol.ToArray(), 0, 16 * 16);

                        output.Add(new Tile(name + ";Floor" + x + y * 12, false, newTex));
                    }
                }
            }

            return output;
        }

        public static void AddToVars(ContentManager manager, GraphicsDevice device, string name)
        {
            Vars.mapTilePool.Add(name, new RandomDictionary<string, Dictionary<Vector2, string>>());
            Vars.tilePool.Add(name, SplitTex(name, device, manager.Load<Texture2D>("sprites/tiles/" + name)));
            Vars.shadowPool.Add(name, SplitShadow(name, device, manager.Load<Texture2D>("sprites/tiles/" + name + "-shadow")));
            Vars.floorPool.Add(name, SplitFloor(name, device, manager.Load<Texture2D>("sprites/tiles/" + name + "-ground")));
        }
    }
}
