using System.Collections.Generic;

namespace wastelands.src.ui
{
    public abstract class Element
    {
        public AlignType align = Align.center;
        private Element parent;
        private List<Element> children = new List<Element>();

        public void removeChild(Element child)
        {
            if (children.Contains(child)) children.Remove(child);
        }

        public void removeChild(int id)
        {
            if (id >= 0 && id < children.Count) children.RemoveAt(id);
        }

        public void disownChild(Element parent, Element child)
        {
            parent.addChild(child);
            removeChild(child);
        }

        public void disownChild(Element parent, int id)
        {
            if (id >= 0 && id < children.Count)
            {
                parent.addChild(children[id]);
                removeChild(id);
            }
        }

        public void addChild(Element child)
        {
            if (child.isOrphan())
            {
                children.Add(child);
                child.setParent(this);
            }
        }

        public void setParent(Element parent)
        {
            this.parent = parent;
        }

        public bool isOrphan() { return parent == null; }
    }
}
