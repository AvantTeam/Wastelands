using Microsoft.Xna.Framework;
using System.Collections.Generic;
using wastelands.src.graphics;

namespace wastelands.src.map
{
    public class Tilemap
    {
        private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

        public void AddTile(Tile tile)
        {
            tiles.Add(tile.position, tile);
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
            foreach(Vector2 pos in tiles.Keys)
            {
                Tile tile = tiles[pos];
                if (tile != null)
                {
                    Vector2 relPos = pos * 16 - Vars.camera.position;

                    if (relPos.X + 16 >= 0 && relPos.Y + 16 >= 0 && relPos.X - 16 <= Vars.screenSize.X && relPos.Y - 16 <= Vars.screenSize.Y)
                    {
                        Draww.DrawSprite(Wastelands.spriteBatch, tile.texture, relPos);
                    }
                }
            }
        }
    }
}
