using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using wastelands.src.graphics;

namespace wastelands.src.map
{
    public class Tilemap
    {
        private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
        public Tile def;

        public Tilemap(Tile def)
        {
            this.def = def;
        }

        public void AddTile(bool solid, Vector2 position, Texture2D texture)
        {
            tiles.Add(position, new Tile(solid, texture));
        }

        public void SetTile(bool solid, Vector2 position, Texture2D texture)
        {
            tiles.Remove(position);
            AddTile(solid, position, texture);
        }

        public void RemoveTile(Vector2 position)
        {
            tiles.Remove(position);
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
                    Draww.DrawSprite(Wastelands.spriteBatch, tile.texture, pos * 32);
                }
            }
        }
    }
}
