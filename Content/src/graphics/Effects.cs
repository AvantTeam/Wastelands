using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.graphics
{
    public static class Effects
    {
        public static Effect auraEffect, slimeEffect;

        public static void LoadEffects(ContentManager content)
        {
            auraEffect = content.Load<Effect>("shaders/aura");
            slimeEffect = content.Load<Effect>("shaders/slime");
        }
    }
}
