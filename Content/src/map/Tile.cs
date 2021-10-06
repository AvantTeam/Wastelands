using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.map
{
    public class Tile
    {
        public bool solid = false;
        public Texture2D texture;

        public Tile(bool solid, Texture2D texture)
        {
            this.solid = solid;
            this.texture = texture;
        }
    }
}
