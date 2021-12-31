using System;
using wastelands.src.graphics;


namespace wastelands.src.ui
{
    class Table : Element
    {
        private NinePatch tex;
        private AlignType align = Align.topLeft;
        private int w, h;

        public Table() { }

        public Table(Action<Table> table) : this()
        {
            table(this);
        }

        new public void Draw()
        {
            base.Draw();
        }

        public Table SetTexture(NinePatch tex)
        {
            this.tex = tex;
            return this;
        }

        public Table SetAlign(AlignType align)
        {
            this.align = align;
            return this;
        }

        public Table SetSize(int w, int h)
        {
            this.w = w;
            this.h = h;
            return this;
        }
    }
}