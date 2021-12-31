using System.Collections.Generic;

namespace wastelands.src.ui
{
    public abstract class Element
    {
        private Element parent;
        private readonly List<Element> children = new List<Element>();

        public void RemoveChild(Element child)
        {
            if (children.Contains(child)) children.Remove(child);
        }

        public void RemoveChild(int id)
        {
            if (id >= 0 && id < children.Count) children.RemoveAt(id);
        }

        public void DisownChild(Element parent, Element child)
        {
            parent.AddChild(child);
            RemoveChild(child);
        }

        public void DisownChild(Element parent, int id)
        {
            if (id >= 0 && id < children.Count)
            {
                parent.AddChild(children[id]);
                RemoveChild(id);
            }
        }

        public Element AddChild(Element child)
        {
            if (child.IsOrphan())
            {
                children.Add(child);
                child.SetParent(this);
            }

            return this;
        }

        public void SetParent(Element parent)
        {
            this.parent = parent;
        }

        public bool IsOrphan() { return parent == null; }

        public void Draw()
        {
            children.ForEach(e => e.Draw());
        }
    }
}
