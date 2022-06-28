using Microsoft.Xna.Framework;
using System.Collections.Generic;
using wastelands.src.graphics;
using System;

namespace wastelands.src.map
{
    public class Tilemap
    {
        public Vector2 position;
        public Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

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
                string biome = ts[key].Split(";")[0];
                string tile = ts[key].Split(";")[1];

                if (tile == "F")
                {
                    int tileID = (int)Math.Abs(Math.Round(Vars.simplexNoise.noise(key.X / 16f, key.Y / 16f, 0) * 4f)) * 9 + Vars.random.Next(0, 9);
                    if (tileID >= Vars.floorPool[biome].Count) tileID = Vars.floorPool[biome].Count - 1;
                    AddTile(Vars.floorPool[biome][tileID], new Vector2(key.X + pos.X * Vars.mapTileSize.X, key.Y + pos.Y * (Vars.mapTileSize.Y - 2)));
                }
                else
                {
                    AddTile(Vars.tilePool[biome][tile], new Vector2(key.X + pos.X * Vars.mapTileSize.X, key.Y + pos.Y * (Vars.mapTileSize.Y - 2)));
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

        public void Draw()
        {
            foreach (Vector2 pos in tiles.Keys)
            {
                Tile tile = tiles[pos];
                if (tile != null)
                {
                    Vector2 relPos = pos * 32 - Vars.camera.position;

                    if (Vars.InBounds(relPos, Vector2.One * 32))
                    {
                        Draww.DrawTile(Wastelands.spriteBatch, tile.texture, relPos);
                    }
                }
            }
        }
    }
}
