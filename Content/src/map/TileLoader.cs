using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using wastelands.src.graphics;

namespace wastelands.src.map
{
    public static class TileLoader
    {
        public static void LoadAll(ContentManager manager, GraphicsDevice device)
        {
            TileSetSplitter.AddToVars(manager, device, "default");
            TileSetSplitter.AddToVars(manager, device, "brick");
        }
    }
}
