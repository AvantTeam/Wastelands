using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using wastelands.src.graphics;
using System;

namespace wastelands.src.map
{
    public class Tilemap
    {
        public Vector2 position;
        public Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
        public Dictionary<Vector2, Tile> undertiles = new Dictionary<Vector2, Tile>();

        public void AddTile(Tile tile, Vector2 pos)
        {
            if (tiles.ContainsKey(pos))
            {
                tiles[pos] = tile;
            }
            else
            {
                tiles.Add(pos, tile);
            }
        }

        public void AddChunk(List<List<Tile>> ts, Vector2 pos)
        {
            int x = 0, y = 0;
            foreach (List<Tile> tileList in ts)
            {
                foreach (Tile tile in tileList)
                {
                    AddTile(tile, new Vector2(x + pos.X, y + pos.Y));
                    x++;
                }
                x = 0;
                y++;
            }
        }

        public void AddChunk(Dictionary<Vector2, string> ts, Vector2 pos)
        {
            foreach (Vector2 key in ts.Keys)
            {
                bool shadow = false;
                string tile;
                int tileId = 0;

                string biome = ts[key].Split(";")[0];
                tile = ts[key].Split(";")[1];

                if (tile.StartsWith("s"))
                {
                    shadow = true;
                } else tileId = int.Parse(tile);

                Vector2 pos2 = new Vector2(key.X + pos.X * Vars.mapTileSize.X, key.Y + pos.Y * (Vars.mapTileSize.Y - 2));
                float randID = Vars.random.Next(0, 21);
                // Cubic variation ID, 0 is the most common, 8 is the rarest.
                randID /= 20;
                randID *= randID *= randID;
                randID *= 30;
                if (randID >= 9) randID = 0;
                int floorID = (int)Math.Abs(Math.Round(Vars.simplexNoise.noise(key.X / 16f, key.Y / 16f, 0) * 4f)) * 9 + (int)randID;
                if (floorID >= Vars.floorPool[biome].Count) floorID = Vars.floorPool[biome].Count - 1;

                if (shadow) {
                    undertiles.Add(pos2, Vars.floorPool[biome][floorID]);
                    AddTile(Vars.shadowPool[biome][int.Parse(tile.Replace("s", ""))], pos2);
                }
                else
                {
                    if (tileId == -1)
                    {
                        AddTile(Vars.floorPool[biome][floorID], pos2);
                    }
                    else
                    {
                        undertiles.Add(pos2, Vars.floorPool[biome][floorID]);
                        AddTile(Vars.tilePool[biome][tileId], pos2);
                    }
                }
            }
        }

        public void SetTile(Tile tile, Vector2 pos)
        {
            tiles.Remove(pos);
            AddTile(tile, pos);
        }

        public void RemoveTile(Vector2 pos)
        {
            tiles.Remove(pos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2 pos in tiles.Keys)
            {
                Tile tile = tiles[pos];
                if (tile != null)
                {
                    Vector2 relPos = pos * 32 - Vars.camera.position;

                    if (Vars.InBounds(relPos, Vector2.One * 32))
                    {
                        if(undertiles.ContainsKey(pos)) Draww.DrawTile(spriteBatch, undertiles[pos].texture, relPos);
                        Draww.DrawTile(spriteBatch, tile.texture, relPos);
                    }
                }
            }
        }
    }
}
