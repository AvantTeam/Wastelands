using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.graphics
{
    public static class Draww
    {
        public static void DrawSprite(SpriteBatch batch, Texture2D sprite, Vector2 position, Color color, float zoom)
        {
            if (sprite == null) return;
            batch.Draw(sprite, new Rectangle((int)(position.X - (sprite.Width / 2) * zoom), (int)(position.Y - (sprite.Width / 2) * zoom), (int)(sprite.Width * zoom), (int)(sprite.Height * zoom)), color);
        }

        public static void DrawSprite(SpriteBatch batch, Texture2D sprite, Vector2 position, float zoom)
        {
            DrawSprite(batch, sprite, position, Color.White, zoom);
        }

        public static void DrawSprite(SpriteBatch batch, Texture2D sprite, Vector2 position)
        {
            DrawSprite(batch, sprite, position, Color.White, 1f);
        }

        public static void DrawSpriteRaw(SpriteBatch batch, Texture2D sprite, Vector2 position, float zoom, Color color)
        {
            batch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, (int)(sprite.Width * zoom), (int)(sprite.Height * zoom)), color);
        }

        public static void DrawTile(SpriteBatch batch, Texture2D sprite, Vector2 position)
        {
            if (sprite == null) return;
            batch.Draw(sprite, new Rectangle((int)(position.X - 16), (int)(position.Y - 16), 32, 32), Color.White);
        }
    }
}
