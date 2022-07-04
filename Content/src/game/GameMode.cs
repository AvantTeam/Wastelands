using System;

namespace wastelands.src.game
{
    public class GameMode
    {
        public virtual void LoadContent() { } // Called when opening the game
        public virtual void Initialize() { } // Called when starting the GameMode
        public virtual void Unload() { } // Called when the GameMode is changed
        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
