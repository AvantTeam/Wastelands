using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace wastelands.src.map
{
    public class MapGen
    {
        public List<Vector2> vecMap = new List<Vector2>();
        public List<string> conMap = new List<string>();
        public Tilemap tilemap = new Tilemap();
        private Random random = new Random();
        private List<string> dirs = new List<string>(new string[] { "D", "L", "R", "U" });

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

        public string SortDir(string sort)
        {
            string o = "";

            if (sort.Contains("R"))
            {
                o += "R";
            }
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

            return o;
        }

        public int ChangeBy(int randID, string dir, int x, int y)
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

        public void Generate(int rooms)
        {
            vecMap.Add(Vector2.Zero);
            conMap.Add("");

            int i = 1;
            while (i < rooms)
            {
                int randID = random.Next(0, vecMap.Count);

                int nextPos = random.Next(0, 4);
                if (nextPos == 0)
                {
                    i += ChangeBy(randID, "L", 1, 0);
                }
                if (nextPos == 1)
                {
                    i += ChangeBy(randID, "U", 0, 1);
                }
                if (nextPos == 2)
                {
                    i += ChangeBy(randID, "R", -1, 0);
                }
                if (nextPos == 3)
                {
                    i += ChangeBy(randID, "D", 0, -1);
                }
            }

            List<VecAndCon> allMap = new List<VecAndCon>();

            for (int k = 0; k < vecMap.Count; k++)
            {
                allMap.Add(new VecAndCon { pos = vecMap[k], con = conMap[k] });
            }

            allMap.Sort(new Vec2Comparer());

            foreach (VecAndCon vc in allMap)
            {
                tilemap.AddChunk(Vars.mapTilePool[vc.con], vc.pos);
            }

            Console.WriteLine(tilemap.tiles.Count);
        }
    }
}