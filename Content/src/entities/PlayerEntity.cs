using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using wastelands.src.graphics;

namespace wastelands.src.entities
{
    class PlayerEntity : Entity
    {
        public Texture2D sprite;

        public PlayerEntity(Vector2 position)
        {
            this.position = position;
        }

        public override void Init()
        {
            size = Vector2.One * 32f;
        }

        public override void Load(ContentManager content)
        {
            sprite = content.Load<Texture2D>("sprites/error");
        }

        public override void Update(float time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                position.X += 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.Y -= 1f;
            }
        }

        public override void Draw(SpriteBatch batch, Vector2 relativePosition)
        {
            Draww.DrawSprite(batch, sprite, relativePosition);
        }
    }
}
