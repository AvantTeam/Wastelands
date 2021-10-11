using System;
using System.Diagnostics;
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

            if (mapData[x + 1][y] == null) // Yandere
            {
                c++;
            }
            if (mapData[x - 1][y] == null)
            {
                c++;
            }
            if (mapData[x][y + 1] == null)
            {
                c++;
            }
            if (mapData[x][y + 1] == null)
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

                int j = 0;
                /*for (int j = 0; j < roomN; j++)*/
                while(j < roomN)
                {
                    Vector2 newPos = table[random.Next(0, 3)];

                    if(mapData[(int)newPos.X][(int)newPos.Y] == null)
                    {
                        mapData[(int)newPos.X][(int)newPos.Y] = "-1";
                        j++;
                    }
                }

                pogress = i / rooms;
            }

            foreach(List<string> strList in mapData)
            {
                Debug.WriteLine(strList);
            }
        }
    }
}
