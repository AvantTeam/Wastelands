using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using wastelands.src.graphics;
using wastelands.src.map;
using wastelands.src.utils;

namespace wastelands.src
{
    public static class Vars
    {
        public static Random random = new Random();
        public static string avantPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AvantGames");
        public static string gamePath = Path.Combine(avantPath, "Wastelands");
        public static string savePath = Path.Combine(gamePath, "save.json");
        public static string logPath = Path.Combine(gamePath, "log.txt");
        public static Camera camera = new Camera();
        public static Vector2 mousePosition = Vector2.Zero;
        public static Vector2 screenSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static Vector2 relativeMousePosition = screenSize;
        public static Vector2 mapTileSize = new Vector2(25, 18);
        public static Settings settings = new Settings();
        public static SaveManager saveManager = new SaveManager();
        public static Dictionary<string, Tile> tilePool = new Dictionary<string, Tile>();
        public static RandomDictionary<string, List<Tile>> mapTilePool = new RandomDictionary<string, List<Tile>>();
    }
}
