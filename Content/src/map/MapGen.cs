using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace wastelands.src.map
{
    public class MapGen
    {
        private static List<string> dirs = new List<string>(new string[] { "D", "L", "R", "U" });

        public class VecAndCon
        {
            public Vector2 pos { get; set; }
            public string con { get; set; }
        }

        public class Vec2Comparer : IComparer<VecAndCon>
        {
            public int Compare(VecAndCon a, VecAndCon b)
            {
                Vector2 x = a.pos;
                Vector2 y = b.pos;

                if (x.Y > y.Y)
                {
                    return 1;
                }
                else if (x.Y < y.Y)
                {
                    return -1;
                }
                else
                {
                    if (x.X > y.X)
                    {
                        return 1;
                    }
                    else if (x.X < y.X)
                    {
                        return -1;
                    }
                }

                return 0;
            }
        }

        public static string SortDir(string sort)
        {
            string o = "";

            if (sort.Contains("D"))
            {
                o += "D";
            }
            if (sort.Contains("L"))
            {
                o += "L";
            }
            if (sort.Contains("U"))
            {
                o += "U";
            }
            if (sort.Contains("R"))
            {
                o += "R";
            }

            return o;
        }

        public static int ChangeBy(List<Vector2> vecMap, List<string> conMap, int randID, string dir, int x, int y)
        {
            Vector2 newPos = new Vector2(vecMap[randID].X + x, vecMap[randID].Y + y);

            if (!vecMap.Contains(newPos))
            {
                vecMap.Add(newPos);
                conMap.Add(dir);

                conMap[randID] = SortDir(conMap[randID] + dirs[3 - dirs.IndexOf(dir)]);
                return 1;
            }

            return 0;
        }

        //Generate a map in connection data, not rooms
        public static Dictionary<Vector2, string> Generate(int rooms)
        {

            List<Vector2> vecMap = new List<Vector2>();
            List<string> conMap = new List<string>();
            
            vecMap.Add(Vector2.Zero);
            conMap.Add("");

            int i = 1;
            while (i < rooms)
            {
                int randID = Vars.random.Next(0, vecMap.Count);

                int nextPos = Vars.random.Next(0, 4);
                if (nextPos == 0)
                {
                    i += ChangeBy(vecMap, conMap, randID, "L", 1, 0);
                }
                if (nextPos == 1)
                {
                    i += ChangeBy(vecMap, conMap, randID, "U", 0, 1);
                }
                if (nextPos == 2)
                {
                    i += ChangeBy(vecMap, conMap, randID, "R", -1, 0);
                }
                if (nextPos == 3)
                {
                    i += ChangeBy(vecMap, conMap, randID, "D", 0, -1);
                }
            }

            List<VecAndCon> allMap = new List<VecAndCon>();

            for (int k = 0; k < vecMap.Count; k++)
            {
                allMap.Add(new VecAndCon { pos = vecMap[k], con = conMap[k] });
            }

            allMap.Sort(new Vec2Comparer());

            Dictionary<Vector2, string> completeMap = new Dictionary<Vector2, string>();

            foreach (VecAndCon vc in allMap)
            {
                completeMap.Add(vc.pos, vc.con);
            }

            completeMap = MapCleaner.CleanWorld(completeMap);

            return completeMap;
        }

        public static void AddChunkToDict(Dictionary<Vector2, string> map, Dictionary<Vector2, string> chunk, Vector2 pos)
        {
            foreach (Vector2 key in chunk.Keys)
            {
                Console.WriteLine(chunk[key]);
                map.Add(new Vector2(key.X + pos.X * Vars.mapTileSize.X, key.Y + pos.Y * Vars.mapTileSize.Y), chunk[key]);
            }
        }

        // TODO add biome generation, default: brick
        public static Dictionary<Vector2, string> PlaceRooms(Dictionary<Vector2, string> map)
        {
            Dictionary<Vector2, string> output = new Dictionary<Vector2, string>();

            foreach (Vector2 pos in map.Keys)
            {
                AddChunkToDict(output, Vars.mapTilePool["brick"][map[pos]], pos);
            }

            output = MapCleaner.SetTileConnections(output);

            return output;
        }
    }
}