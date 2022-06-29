using wastelands.src.utils;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.map
{
    public class Tile
    {
        public bool solid = false;
        public Texture2D texture;
        public string name; // Used for serializing
        private string id;

        public Tile(string name, bool solid)
        {
            this.name = name;
            this.solid = solid;
            texture = null;
            id = IdGenerator.FromString(name);
        }

        public Tile(string name, bool solid, Texture2D texture)
        {
            this.name = name;
            this.solid = solid;
            this.texture = texture;
            id = IdGenerator.FromString(name);
        }
    }
}
