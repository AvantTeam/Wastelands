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

            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i == 3 && j == 11) continue;

                    List<Color> newCol = new List<Color>();
                    Texture2D newTex = new Texture2D(device, 16, 16);

                    for (int q = 0; q < 16; q++)
                    {
                        newCol.AddRange(texData.Skip(i * 16 * 12 * 16 + j * 16 + q * 16 * 12).Take(16).ToArray());
                    }

                    newTex.SetData(newCol.ToArray(), 0, 16 * 16);

                    output.Add(new Tile(name + ";" + (j + i * 12).ToString(), true, newTex));
                }
            }

            return output;
        }

        public static List<Tile> SplitShadow(string name, GraphicsDevice device, Texture2D tex)
        {
            List<Tile> output = new List<Tile>();
            Color[] texData = new Color[tex.Width * tex.Height];
            tex.GetData(texData);

            for (int i = 0; i < 4; i++)
            {
                List<Color> newCol = new List<Color>();
                Texture2D newTex = new Texture2D(device, 16, 16);

                for (int q = 0; q < 16; q++)
                {
                    newCol.AddRange(texData.Skip(i * 16 + q * 16 * 4).Take(16).ToArray());
                }

                newTex.SetData(newCol.ToArray(), 0, 16 * 16);

                output.Add(new Tile(name + ";s" + i.ToString(), false, newTex));
            }

            return output;
        }

        public static List<Tile> SplitFloor(string name, GraphicsDevice device, Texture2D tex)
        {
            List<Tile> output = new List<Tile>();
            Color[] texData = new Color[tex.Width * tex.Height];
            tex.GetData(texData);

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    List<Color> newCol = new List<Color>();
                    Texture2D newTex = new Texture2D(device, 16, 16);

                    for (int q = 0; q < 16; q++)
                    {
                        newCol.AddRange(texData.Skip(i * 16 + j * 16 * 12 * 16 + q * 16 * 12).Take(16).ToArray());
                    }

                    newTex.SetData(newCol.ToArray(), 0, 16 * 16);

                    output.Add(new Tile(name + ";Floor" + i + j * 12, false, newTex));
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
