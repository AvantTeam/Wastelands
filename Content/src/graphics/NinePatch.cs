using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wastelands.src.graphics
{
    // Rendering based off craftworkgames's code at StackExchange
    public class NinePatch
    {
        public readonly Texture2D tex;

        public readonly int
            leftMargin,
            rightMargin,
            topMargin,
            bottomMargin;

        public NinePatch(Texture2D tex, int leftMargin, int rightMargin, int topMargin, int bottomMargin)
        {
            this.tex = tex;

            this.leftMargin = leftMargin;
            this.rightMargin = rightMargin;
            this.topMargin = topMargin;
            this.bottomMargin = bottomMargin;
        }

        private Rectangle[] createPatches(Rectangle rectangle)
        {
            var x = rectangle.X;
            var y = rectangle.Y;
            var w = rectangle.Width;
            var h = rectangle.Height;

            var middleWidth = w - leftMargin - rightMargin;
            var middleHeight = h - topMargin - bottomMargin;

            var bottomY = y + h - bottomMargin;
            var rightX = x + w - rightMargin;
            var leftX = x + leftMargin;
            var topY = y + topMargin;

            var patches = new[]
            {
                new Rectangle(x,      y,        leftMargin,  topMargin),
                new Rectangle(leftX,  y,        middleWidth,  topMargin),
                new Rectangle(rightX, y,        rightMargin, topMargin),
                new Rectangle(x,      topY,     leftMargin,  middleHeight),
                new Rectangle(leftX,  topY,     middleWidth,  middleHeight),
                new Rectangle(rightX, topY,     rightMargin, middleHeight),
                new Rectangle(x,      bottomY,  leftMargin,  bottomMargin),
                new Rectangle(leftX,  bottomY,  middleWidth,  bottomMargin),
                new Rectangle(rightX, bottomY,  rightMargin, bottomMargin)
            };

            return patches;
        }

        public NinePatchRenderable createRenderable(Rectangle destinationSize)
        {
            return new NinePatchRenderable(this, createPatches(tex.Bounds), createPatches(destinationSize));
        }

    }

    public class NinePatchRenderable
    {
        public readonly NinePatch tex;

        private Rectangle[] inPatch, destPatch;

        public NinePatchRenderable(NinePatch tex, Rectangle[] inPatch, Rectangle[] destPatch)
        {
            this.tex = tex;
            this.inPatch = inPatch;
            this.destPatch = destPatch;
        }

        private Rectangle Displace(Rectangle rect, int x, int y)
        {
            return new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
        }

        public void Render(int x, int y)
        {
            for (var i = 0; i < inPatch.Length; i++)
            {
                Wastelands.spriteBatch.Draw(tex.tex, sourceRectangle: inPatch[i],
                destinationRectangle: Displace(destPatch[i], x, y), color: Color.White);
            }
        }
    }
}
