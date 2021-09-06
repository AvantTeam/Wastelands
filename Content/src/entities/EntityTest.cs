using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using wastelands.src.graphics;
using System;

namespace wastelands.src.entities
{
    public class EntityTest : Entity
    {
        public Texture2D sprite;
        public override void Load(ContentManager content)
        {
            sprite = content.Load<Texture2D>("sprites/error");
        }

        public override void Update(float time)
        {
            position.X = (float)Math.Cos(time) * 30;
            position.Y = (float)Math.Sin(time) * 30;
        }

        public override void Draw(SpriteBatch batch, Vector2 relativePosition)
        {
            Draww.DrawSprite(batch, sprite, relativePosition);
        }
    }
}
