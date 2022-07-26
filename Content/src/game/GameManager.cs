using System;
using System.Collections.Generic;
using System.Text;

namespace wastelands.src.game
{
    public static class GameManager
    {
        public static GameMode gameMode = GameModes.Get("test");
        public static bool paused = false;

        public static void ChangeGameMode(string newGameModeKey)
        {
            GameMode newGameMode = GameModes.Get(newGameModeKey);

            gameMode.Unload();
            newGameMode.Initialize();

            gameMode = newGameMode;
        }
    }
}
