using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.entities
{
    public abstract class Entity
    {
        public int x, y; //Entity position
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
