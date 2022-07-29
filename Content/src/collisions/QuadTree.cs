using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace wastelands.src.collisions
{
    // Adapted from https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374
    public class QuadTree
    {
        private const int maxObjectsPerNode = 10;
        private const int maxNodeDepth = 5;

        private int depth = 0;
        private List<Collider> objects;
        private Rectangle bounds;
        private QuadTree[] nodes;

        public QuadTree(int depth, Rectangle bounds)
        {
            this.depth = depth;
            this.bounds = bounds;

            objects = new List<Collider>();
            nodes = new QuadTree[4];
        }

        // Clear the QuadTree and all it's nodes
        public void Clear()
        {
            for(int i = 0; i < nodes.Length; i++)
            {
                if(nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        // Split the QuadTree into four nodes
        private void Split()
        {
            int divWidth = bounds.Width / 2;
            int divHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes[0] = new QuadTree(depth + 1, new Rectangle(x + divWidth, y, divWidth, divHeight));
            nodes[1] = new QuadTree(depth + 1, new Rectangle(x, y, divWidth, divHeight));
            nodes[2] = new QuadTree(depth + 1, new Rectangle(x, y + divHeight, divWidth, divHeight));
            nodes[3] = new QuadTree(depth + 1, new Rectangle(x + divWidth, y + divHeight, divWidth, divHeight));
        }

        // Get the node index of a possible Rectangle
        private int GetIndex(Rectangle rect)
        {
            int index = -1;
            Vector2 midPoint = new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);

            bool topQuadrant = rect.Y < midPoint.Y && rect.Y + rect.Height < midPoint.Y;
            bool bottomQuadrant = rect.Y > midPoint.Y;

            if (rect.X < midPoint.X && rect.X + rect.Width < midPoint.X)
            {
                if (topQuadrant) index = 1;
                else if (bottomQuadrant) index = 2;
            } else if (rect.X > midPoint.X)
            {
                if (topQuadrant) index = 0;
                else if (bottomQuadrant) index = 3;
            }

            return index;
        }

        // Insert a Collider into the QuadTree
        public void InsertObject(Collider collider)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(collider.bounds);

                if (index != -1)
                {
                    nodes[index].InsertObject(collider);
                    return;
                }
            }

            objects.Add(collider);

            if (objects.Count > maxObjectsPerNode && depth < maxNodeDepth)
            {
                if (nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < objects.Count)
                {
                    int index = GetIndex(objects[i].bounds);
                    if (index != -1)
                    {
                        nodes[index].InsertObject(objects[i]);
                        objects.RemoveAt(i);
                    }
                    else i++;
                }
            }
        }

        // Iteration for GetPossibleColliders
        private List<Collider> GetPossibleCollidersIterate(List<Collider> returnObjects, Rectangle rect)
        {
            int index = GetIndex(rect);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].GetPossibleCollidersIterate(returnObjects, rect);
            }

            returnObjects.AddRange(objects);

            return returnObjects;
        }

        // Return all possible colliders from a rect
        public List<Collider> GetPossibleColliders(Rectangle rect)
        {
            List<Collider> output = new List<Collider>();

            return GetPossibleCollidersIterate(output, rect);
        }
    }
}
