using wastelands.src.graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace wastelands.src
{
    public static class Vars
    {
        public static Camera camera = new Camera();
        public static Vector2 mousePosition = Vector2.Zero;
        public static Vector2 screenSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static Vector2 relativeMousePosition = screenSize;
    }
}
