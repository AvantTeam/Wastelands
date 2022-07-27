using Microsoft.Xna.Framework;
using System.Collections.Generic;
using wastelands.src.utils;

namespace wastelands.src.map
{
    public static class MapCleaner
    {
        // Thanks @GlennFolker
        public static int[] connections = new int[]{
            39, 36, 39, 36, 27, 16, 27, 24, 39, 36, 39, 36, 27, 16, 27, 24,
            38, 37, 38, 37, 17, 41, 17, 43, 38, 37, 38, 37, 26, 21, 26, 25,
            39, 36, 39, 36, 27, 16, 27, 24, 39, 36, 39, 36, 27, 16, 27, 24,
            38, 37, 38, 37, 17, 41, 17, 43, 38, 37, 38, 37, 26, 21, 26, 25,
             3,  4,  3,  4, 15, 40, 15, 20,  3,  4,  3,  4, 15, 40, 15, 20,
             5, 28,  5, 28, 29, 10, 29, 23,  5, 28,  5, 28, 31, 11, 31, 32,
             3,  4,  3,  4, 15, 40, 15, 20,  3,  4,  3,  4, 15, 40, 15, 20,
             2, 30,  2, 30,  9, 46,  9, 22,  2, 30,  2, 30, 14, 44, 14,  6,
            39, 36, 39, 36, 27, 16, 27, 24, 39, 36, 39, 36, 27, 16, 27, 24,
            38, 37, 38, 37, 17, 41, 17, 43, 38, 37, 38, 37, 26, 21, 26, 25,
            39, 36, 39, 36, 27, 16, 27, 24, 39, 36, 39, 36, 27, 16, 27, 24,
            38, 37, 38, 37, 17, 41, 17, 43, 38, 37, 38, 37, 26, 21, 26, 25,
             3,  0,  3,  0, 15, 42, 15, 12,  3,  0,  3,  0, 15, 42, 15, 12,
             5,  8,  5,  8, 29, 35, 29, 33,  5,  8,  5,  8, 31, 34, 31,  7,
             3,  0,  3,  0, 15, 42, 15, 12,  3,  0,  3,  0, 15, 42, 15, 12,
             2,  1,  2,  1,  9, 45,  9, 19,  2,  1,  2,  1, 14, 18, 14, 13
        };

        public static bool Exists(List<Vector2> positions, Vector2 pos)
        {
            return positions.Contains(pos);
        }

        // Remove empty pathways from a Generated Room Set
        public static Dictionary<Vector2, string> CleanWorld(Dictionary<Vector2, string> map)
        {
            Dictionary<Vector2, string> newMap = new Dictionary<Vector2, string>();
            List<Vector2> positions = new List<Vector2>(map.Keys);

            foreach(Vector2 pos in positions)
            {
                string con = "";

                Vector2 nP;
                if (map[pos].Contains("D"))
                {
                    nP = pos + new Vector2(0, 1);
                    if (Exists(positions, nP) && map[nP].Contains("U")) con += "D";
                }
                if (map[pos].Contains("L"))
                {
                    nP = pos + new Vector2(-1, 0);
                    if (Exists(positions, nP) && map[nP].Contains("R")) con += "L";
                }
                if (map[pos].Contains("U"))
                {
                    nP = pos + new Vector2(0, -1);
                    if (Exists(positions, nP) && map[nP].Contains("D")) con += "U";
                }
                if (map[pos].Contains("R"))
                {
                    nP = pos + new Vector2(1, 0);
                    if (Exists(positions, nP) && map[nP].Contains("L")) con += "R";
                }

                newMap.Add(pos, con);
            }

            return newMap;
        }

        // Set the connections and shadows for a W/F based map
        public static Dictionary<Vector2, string> SetTileConnections(Dictionary<Vector2, string> map)
        {
            Dictionary<Vector2, string> output = new Dictionary<Vector2, string>();
            List<Vector2> positions = new List<Vector2>(map.Keys);

            foreach (Vector2 pos in positions)
            {
                if (map[pos] != "W") {
                    Vector2 np = pos + new Vector2(0, -1);

                    if(Exists(positions, np) && map[np] == "W")
                    {
                        output.Add(pos, "S");
                    } else if (map[pos] == "F")
                    {
                        output.Add(pos, "F");
                    }
                } else
                {
                    output.Add(pos, "W");
                }
            }

            Dictionary<Vector2, string> output2 = new Dictionary<Vector2, string>();
            positions = new List<Vector2>(output.Keys);
            foreach (Vector2 pos in positions)
            {
                Vector2 np;
                if (output[pos] == "W")
                {
                    int index = 0;
                    
                    np = pos + new Vector2(1, 0);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 0;
                    np = pos + new Vector2(1, -1);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 1;
                    np = pos + new Vector2(0, -1);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 2;
                    np = pos + new Vector2(-1, -1);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 3;
                    np = pos + new Vector2(-1, 0);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 4;
                    np = pos + new Vector2(-1, 1);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 5;
                    np = pos + new Vector2(0, 1);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 6;
                    np = pos + new Vector2(1, 1);
                    if (Exists(positions, np) && output[np] == "W") index |= 1 << 7;

                    Log.Write(index);
                    output2.Add(pos, "brick;" + connections[index]);
                }
                else if (output[pos] == "S")
                {
                    //Pseudo bitmask
                    int con = 0;

                    np = pos + new Vector2(-1, 0);
                    if (Exists(positions, np) && (output[np] == "W" || output[np] == "S")) con += 3;
                    np = pos + new Vector2(1, 0);
                    if (Exists(positions, np) && (output[np] == "W" || output[np] == "S")) con += 1;

                    if (con == 4) con = 2;
                    if (con == 0) con = 4;

                    output2.Add(pos, "brick;s" + (con - 1).ToString());
                }
                else if(output[pos] == "F") output2.Add(pos, "brick;-1");
            }

            return output2;
        }
    }
}
