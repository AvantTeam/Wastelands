using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.map
{
    public class Tile
    {
        public Vector2 position;
        public bool solid = false;
        public Texture2D texture;

        public Tile(bool solid, Vector2 position)
        {
            this.solid = solid;
            this.position = position;
            texture = null;
        }

        public Tile(bool solid, Vector2 position, Texture2D texture)
        {
            this.solid = solid;
            this.position = position;
            this.texture = texture;
        }

        public Tile(bool solid)
        {
            this.solid = solid;
            texture = null;
        }

        public Tile(bool solid, Texture2D texture)
        {
            this.solid = solid;
            this.texture = texture;
        }

        public void Set(Vector2 position)
        {
            this.position = position;
        }
    }
}
