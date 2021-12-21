using Microsoft.Xna.Framework;
using System.Collections.Generic;
using wastelands.src.graphics;

namespace wastelands.src.map
{
    public class Tilemap
    {
        public Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

        public void AddTile(Tile tile)
        {
            if (tiles.ContainsKey(tile.position))
            {
                tiles[tile.position] = tile;
            }
            else
            {
                tiles.Add(tile.position, tile);
            }
        }

        public void AddChunk(List<Tile> ts, int x, int y)
        {
            foreach (Tile tile in ts)
            {
                Tile t = new Tile(tile.solid, new Vector2(tile.position.X + x * Vars.mapTileSize.X, tile.position.Y + y * (Vars.mapTileSize.Y - 1)), tile.texture);
                AddTile(t);
            }
        }

        public void AddChunk(List<Tile> ts, Vector2 pos)
        {
            AddChunk(ts, (int)pos.X, (int)pos.Y);
        }

        public void SetTile(Tile tile)
        {
            tiles.Remove(tile.position);
            AddTile(tile);
        }

        public void RemoveTile(Vector2 position)
        {
            tiles.Remove(position);
        }

        public void Draw()
        {
            foreach (Vector2 pos in tiles.Keys)
            {
                Tile tile = tiles[pos];
                if (tile != null)
                {
                    Vector2 relPos = pos * 32 - Vars.camera.position;

                    if (relPos.X + 32 >= 0 && relPos.Y + 32 >= 0 && relPos.X - 32 <= Vars.screenSize.X && relPos.Y - 32 <= Vars.screenSize.Y)
                    {
                        Draww.DrawSprite(Wastelands.spriteBatch, tile.texture, relPos);
                    }
                }
            }
        }
    }
}
