using System;
using System.Collections.Generic;
using wastelands.src.graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using wastelands.src.map;

namespace wastelands.src
{
    public static class Vars
    {
        public static string aPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AvantGames");
        public static string path = System.IO.Path.Combine(aPath, "Wastelands");
        public static Camera camera = new Camera();
        public static Vector2 mousePosition = Vector2.Zero;
        public static Vector2 screenSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static Vector2 relativeMousePosition = screenSize;
        public static Vector2 mapTileSize = new Vector2(25, 18);
        public static Settings settings = new Settings();
        public static SaveManager saveManager = new SaveManager();
        public static Dictionary<string, Tile> tilePool = new Dictionary<string, Tile>();
    }
}
