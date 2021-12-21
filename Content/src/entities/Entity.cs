using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.entities
{
    public abstract class Entity
    {
        public Vector2 position;
        public Vector2 size;
        public int z; //Drawing layer

        public Entity()
        {
            Wastelands.entities.Add(this);
        }

        public virtual void Init() { }
        public virtual void Load(ContentManager content) { }
        public virtual void Update(float time) { }
        public virtual void Draw(SpriteBatch batch, Vector2 relativePosition) { }
    }
}
