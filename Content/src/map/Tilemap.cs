using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using wastelands.src.graphics;

namespace wastelands.src.map
{
    public class Tilemap
    {
        private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

        public void AddTile(bool solid, Vector2 position, Texture2D texture)
        {
            tiles.Add(position, new Tile(solid, texture));
        }

        public void AddTile(Vector2 position, Tile tile)
        {
            tiles.Add(position, tile);
        }

        public void SetTile(bool solid, Vector2 position, Texture2D texture)
        {
            tiles.Remove(position);
            AddTile(solid, position, texture);
        }

        public void SetTile(Vector2 position, Tile tile)
        {
            tiles.Remove(position);
            AddTile(position, tile);
        }

        public void RemoveTile(Vector2 position)
        {
            tiles.Remove(position);
        }

        public Tile GetTile(Vector2 position)
        {
            return tiles[position];
        }

        public void Sort()
        {
            IEnumerable<Vector2> sorted = (from key in tiles.Keys orderby key.Y descending select key).AsEnumerable();
            Dictionary<Vector2, Tile> newDict = new Dictionary<Vector2, Tile>();

            foreach (Vector2 pos in sorted)
            {
                newDict.Add(pos, tiles[pos]);
            }

            tiles = newDict;
        }

        public void Draw()
        {
            foreach(Vector2 pos in tiles.Keys)
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
