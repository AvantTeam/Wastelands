using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.graphics
{
    public static class Draww
    {
        public static void DrawSprite(SpriteBatch batch, Texture2D sprite, Vector2 position, Color color)
        {
            batch.Draw(sprite, new Rectangle((int)position.X - sprite.Width / 2, (int)position.Y - sprite.Height / 2, sprite.Width, sprite.Height), color);
        }

        public static void DrawSprite(SpriteBatch batch, Texture2D sprite, Vector2 position)
        {
            DrawSprite(batch, sprite, position, Color.White);
        }
    }
}
