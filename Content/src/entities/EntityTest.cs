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

        public EntityTest(Vector2 position)
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
            Vector2 dir = position - Vars.relativeMousePosition;
            dir.Normalize();

            position += dir * -1.5f;
        }

        public override void Draw(SpriteBatch batch, Vector2 relativePosition)
        {
            Draww.DrawSprite(batch, sprite, relativePosition);
        }
    }
}
