using Microsoft.Xna.Framework;
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

        public void Init() { }
        public void Load() { }
        public void Update() { }
        public void Draw(SpriteBatch batch) { }
    }
}
