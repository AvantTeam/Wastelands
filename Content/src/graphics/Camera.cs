using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace wastelands.src.graphics
{
    public class Camera
    {
        public Vector2 position;
        public float speed = 5f;
        public float zoom = 1f;

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.Y -= speed;
            }
        }
    }
}
