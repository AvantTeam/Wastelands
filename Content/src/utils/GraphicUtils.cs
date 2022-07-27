using Microsoft.Xna.Framework;

namespace wastelands.src.utils
{
    public static class GraphicUtils
    {

        public static Color MultiplyColor(Color a, float mult, float alphaMult)
        {
            Vector4 col = a.ToVector4();
            col.X *= mult;
            col.Y *= mult;
            col.Z *= mult;
            col.W *= alphaMult;

            return new Color(col);
        }
    }
}
