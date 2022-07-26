using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using wastelands.src.graphics;
using wastelands.src.map;
using wastelands.src.utils;
using Microsoft.Xna.Framework.Input;

namespace wastelands.src
{
    public static class Vars
    {
        public static string
            avantPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AvantGames"),
            gamePath = Path.Combine(avantPath, "Wastelands"),
            savePath = Path.Combine(gamePath, "save.json"),
            logPath = Path.Combine(gamePath, "log.txt");

        public static Vector2
            mousePosition = Vector2.Zero,
            screenSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height),
            relativeMousePosition = screenSize,
            mapTileSize = new Vector2(30, 20);

        public static KeyboardState kbCurrentState, kbPreviousState;

        public static Camera camera = new Camera();

        public static Random random = new Random();
        public static SimplexNoise simplexNoise = new SimplexNoise();

        public static Settings settings = new Settings();
        public static SaveManager saveManager = new SaveManager();

        public static Dictionary<string, Dictionary<string, Tile>> tilePool = new Dictionary<string, Dictionary<string, Tile>>();
        public static Dictionary<string, List<Tile>> floorPool = new Dictionary<string, List<Tile>>();

        // Biome, Connections -> Room
        public static Dictionary<string, RandomDictionary<string, Dictionary<Vector2, string>>> mapTilePool = new Dictionary<string, RandomDictionary<string, Dictionary<Vector2, string>>>();
        
        public static bool InBounds(Vector2 pos, Vector2 size)
        {
            return pos.X + size.X >= 0 && pos.Y + size.Y >= 0 && pos.X - size.X <= screenSize.X && pos.Y - size.Y <= screenSize.Y;
        }
    }
}