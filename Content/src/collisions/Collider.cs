using Microsoft.Xna.Framework;

namespace wastelands.src.collisions
{
    public class Collider
    {
        public Rectangle bounds;

        public Collider(int x, int y, int w, int h)
        {
            bounds = new Rectangle(x, y, w, h);
        }

        public Collider(Rectangle bounds)
        {
            this.bounds = bounds;
        }
    }
}
