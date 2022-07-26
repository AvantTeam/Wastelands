using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace wastelands.src.map
{
    public static class MapCleaner
    {
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
                    string con = "";

                    np = pos + new Vector2(0, 1);
                    if (Exists(positions, np) && output[np] == "W") con += "D";
                    np = pos + new Vector2(-1, 0);
                    if (Exists(positions, np) && output[np] == "W") con += "L";
                    np = pos + new Vector2(0, -1);
                    if (Exists(positions, np) && output[np] == "W") con += "U";
                    np = pos + new Vector2(1, 0);
                    if (Exists(positions, np) && output[np] == "W") con += "R";

                    if (con == "") con = "C";

                    output2.Add(pos, "brick;" + con);
                }
                else if (output[pos] == "S")
                {
                    string con = "s";

                    np = pos + new Vector2(-1, 0);
                    if (Exists(positions, np) && (output[np] == "W" || output[np] == "S")) con += "L";
                    np = pos + new Vector2(1, 0);
                    if (Exists(positions, np) && (output[np] == "W" || output[np] == "S")) con += "R";

                    if (con == "s") con = "sC";

                    output2.Add(pos, "brick;" + con);
                }
                else if(output[pos] == "F") output2.Add(pos, "brick;F");
            }

            return output2;
        }
    }
}
