using System;
using Myra;
using Myra.Graphics2D.UI;
using wastelands.src.utils;

namespace wastelands.src.game
{
    public class MainMenuGameMode : GameMode
    {
        private Desktop desktop;

        public override void Initialize()
        {
            desktop = new Desktop();
            Log.Write("Hey, i initialized!");
        }

        public override void LoadContent()
        {
            Log.Write("Hey, i loaded!");
        }

        public override void Update()
        {
            
        }
    }
}
