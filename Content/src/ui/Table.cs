using System;
using System.Collections.Generic;

namespace wastelands.src.ui
{
    class Table : Element
    {
        private Element parent;
        private List<Element> children = new List<Element>();

        public void removeChild(Element child) {
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

        public Element disownChild(Element parent, Element child)
        {
            parent.addChild(child);
            removeChild(child);
        }

        public Element disownChild(Element parent, int id)
        {
            parent.addChild(children[id]);
            removeChild(id);
        }

        public bool isOrphan() { return parent == null; }
    }
}
