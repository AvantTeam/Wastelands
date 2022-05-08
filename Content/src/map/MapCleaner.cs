using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace wastelands.src.map
{
    public static class MapCleaner
    {
        private static string cons = "DLUR";
        public static Dictionary<Vector2, string> MoveCenterToCorner(Dictionary<Vector2, string> map)
        {
            Dictionary<Vector2, string> newMap = new Dictionary<Vector2, string>();

            int x, y;
            x = y = int.MaxValue;

            foreach (Vector2 key in map.Keys)
            {
                if (key.X < x) x = (int)key.X;
                if (key.Y < y) y = (int)key.Y;
            }

            bool subsX = x > 0;
            bool subsY = y > 0;

            x = Math.Abs(x);
            y = Math.Abs(y);

            foreach (Vector2 key in map.Keys)
            {
                newMap.Add(new Vector2(key.X + x * (subsX ? -1 : 1), key.Y + y * (subsY ? -1 : 1)), map[key]);
            }

            return newMap;
        }

        public static Dictionary<Vector2, string> Clean(Dictionary<Vector2, string> map)
        {
            Dictionary<Vector2, string> cmap = MoveCenterToCorner(map);

            Dictionary<Vector2, string> newMap = new Dictionary<Vector2, string>();

            Vector2 lPos = Vector2.Zero;
            foreach(Vector2 pos in cmap.Keys)
            {
                lPos = pos;
                string val = cmap[pos] == "s" ? "s" : "";

                if (cmap[pos] == "F") val = "F";
                else
                {
                    Vector2[] checks = new Vector2[] { new Vector2(pos.X, pos.Y + 1), new Vector2(pos.X - 1, pos.Y), new Vector2(pos.X, pos.Y - 1), new Vector2(pos.X + 1, pos.Y) };

                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 checkP = checks[i];
                        if (cmap.ContainsKey(checkP) && cmap[checkP] == (val.Contains("s") ? "s" : "W"))
                        {
                            val += cons[i];
                        }
                    }
                }

                newMap.Add(pos, val == "s" ? "sC" : val == "" ? "C" : val);
            }

            Vars.camera.position = lPos * 16;
            return newMap;
        }
    }
}
