using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.entities
{
    public abstract class Entity
    {
        public Vector2 position;

        public Entity()
        {
            Wastelands.entities.Add(this);
        }

        public virtual void Init() { }
        public virtual void Load(ContentManager content) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch batch, Vector2 relativePosition) { }
    }
}
