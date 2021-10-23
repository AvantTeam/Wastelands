using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using wastelands.src.graphics;

namespace wastelands.src.map
{
    public class MapTile
    {
        public List<List<string>> tiles;

        public void Draw(Vector2 pos, Dictionary<string, Texture2D> biome)
        {
            int x = 0, y = 0;
            foreach(List<string> list in tiles)
            {
                foreach(string st in list)
                {
                    Draww.DrawSprite(Wastelands.spriteBatch, biome[st], pos + new Vector2(x * 32, y * 32));
                }
            }
        }
    }
}
