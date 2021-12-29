namespace wastelands.src.ui
{
    public class AlignType
    {
        public int h = 0, v = 0;

        public AlignType(int h, int v)
        {
            this.h = h;
            this.v = v;
        }
    }

    public static class Align
    {
        public static AlignType
            center = new AlignType(0, 0),
            right = new AlignType(1, 0),
            left = new AlignType(-1, 0),
            top = new AlignType(0, 1),
            bottom = new AlignType(0, -1),
            topRight = new AlignType(1, 1),
            topLeft = new AlignType(-1, 1),
            bottomRight = new AlignType(1, -1),
            bottomLeft = new AlignType(-1, -1);
    }
}
