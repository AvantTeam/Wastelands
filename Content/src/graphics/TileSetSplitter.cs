using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using wastelands.src.map;

namespace wastelands.src.graphics
{
    public static class TileSetSplitter
    {
        // This hurts my soul - DLUR
        private static string[] names = new string[]{
            "DR", "DU", "UR", "R", "sR",
            "DLR", "DLUR", "LUR", "LR", "sLR",
            "DL", "DLU", "LU", "L", "sL",
            "D", "DU", "U", "C", "sC"
        };

        public static Dictionary<string, Tile> SplitTex(GraphicsDevice device, Texture2D tex)
        {
            Dictionary<string, Tile> output = new Dictionary<string, Tile>();
            Color[] texData = new Color[tex.Width * tex.Height];
            tex.GetData(texData);

            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    List<Color> newCol = new List<Color>();
                    Texture2D newTex = new Texture2D(device, 16, 16);

                    for (int q = 0; q < 16; q++)
                    {
                        newCol.AddRange(texData.Skip(i * 16 + j * 16 * 4 * 16 + q * 16 * 4).Take(16).ToArray());
                    }

                    newTex.SetData(newCol.ToArray(), 0, 16 * 16);

                    Vars.tilePool[names[i*5+j]] = new Tile(true, newTex);
                }
            }

            return output;
        }
    }
}
