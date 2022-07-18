using System;
using System.Collections.Generic;
using System.Text;

namespace wastelands.src.game
{
    public static class GameManager
    {
        public static GameMode gameMode = GameModes.Get("main-menu");
        public static bool paused = false;

        public static void ChangeGameMode(GameMode newGameMode)
        {
            gameMode.Unload();
            newGameMode.Initialize();

            gameMode = newGameMode;
        }
    }
}
