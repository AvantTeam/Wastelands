using System;
using System.Collections.Generic;
using System.Text;

namespace wastelands.src.ui
{
    public abstract class Element
    {
        public AlignType align = Align.center;
        private Element parent;
        private List<Element> children = new List<Element>();

        public void removeChild(Element child)
        {
            try
            {
                children.Remove(child);
            }
            catch (Exception e)
            {
                // TODO Later add logger error
            }
        }

        public void removeChild(int id)
        {
            try
            {
                children.RemoveAt(id);
            }
            catch (Exception e)
            {
                // TODO Later add logger error
            }
        }

        public void disownChild(Element parent, Element child)
        {
            parent.addChild(child);
            removeChild(child);
        }

        public void disownChild(Element parent, int id)
        {
            parent.addChild(children[id]);
            removeChild(id);
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
