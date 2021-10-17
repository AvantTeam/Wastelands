using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wastelands.src.map
{
    public class MapGen
    {
        private Tilemap map = new Tilemap();
        private List<List<string>> mapData = new List<List<string>>();
        private Vector2[] table = new Vector2[] { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };
        private int rooms = 20;
        public float pogress = 0f; // Pog

        public MapGen(int rooms)
        {
            this.rooms = rooms;
        }

        public int GetAvailable(Vector2 pos)
        {
            int x = (int)pos.X; // C# weak
            int y = (int)pos.Y;

            int c = 0;

            if (x + 1 >= 0 && x + 1 < mapData.Count && mapData[x + 1][y] == null)
            {
                c++;
            }

            if (x - 1 >= 0 && x - 1 < mapData.Count && mapData[x - 1][y] == null)
            {
                c++;
            }

            if (y + 1 >= 0 && y + 1 < mapData.Count && mapData[x][y + 1] == null)
            {
                c++;
            }

            if (y - 1 >= 0 && y - 1 < mapData.Count && mapData[x][y + 1] == null)
            {
                c++;
            }

            return c;
        }

        public void InitData()
        {
            for(int i = 0; i < rooms * 2; i++)
            {
                mapData.Add(new List<string>());

                for (int j = 0; j < rooms * 2; j++)
                {
                    mapData[i].Add(null);
                }
            }
        }

        public void Generate()
        {
            Random random = new Random();
            Vector2 initPos = new Vector2(0, 0);


            mapData[rooms][rooms] = "-1";

            for(int i = 0; i < rooms; i++)
            {
                Vector2 point = Vector2.Zero;
                int roomN = random.Next(0, 4);

                int available = GetAvailable(point);

                if (available < roomN) roomN = available;

                int j = 0, c = 0;

                while (j < roomN && c < 600) // Failsafe
                {
                    Vector2 newPos = point + table[random.Next(0, 3)];

                    if (newPos.X >= 0 && newPos.Y >= 0 && newPos.X < mapData.Count && newPos.Y < mapData.Count && mapData[(int)newPos.X][(int)newPos.Y] == null)
                    {
                        mapData[(int)newPos.X][(int)newPos.Y] = "-1";
                        point = newPos;
                        j++;
                    }

                    c++;
                }

                pogress = i / rooms;
            }

            string a = "";
            foreach(List<string> strList in mapData)
            {
                a = "";
                foreach (string str in strList)
                {
                    a += str;
                }
                Console.WriteLine(a);
            }
        }
    }
}
