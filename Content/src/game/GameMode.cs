using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.game
{
    public class GameMode
    {
        public virtual void LoadContent(ContentManager content) { } // Called when opening the game
        public virtual void Initialize() { } // Called when starting the GameMode
        public virtual void Unload() { } // Called when the GameMode is changed
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime, GraphicsDevice device, SpriteBatch batch) { }
    }

    public static class GameModes
    {
        public static Dictionary<string, GameMode> gameModes = new Dictionary<string, GameMode> {
            { "main-menu", new MainMenuGameMode() }
        };

        public static GameMode Get(string name)
        {
            return gameModes[name];
        }
    }
}
