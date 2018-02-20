using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ship
{
    public class Structure
    {
        public class Node
        {
            public Vector2 Position;
        }

        public class FrameSection
        {
            public Node from;
            public Node to;
        }

        public Graph<Node, FrameSection> Skeleton;
        public List<Node> Nodes;

        public Structure()
        {
            Skeleton = new Graph<Node, FrameSection>();
            Nodes = new List<Node>();
        }

        public Node AddNode(Vector2 position)
        {
            var node = new Node()
            {
                Position = position
            };
            Skeleton.Add(node);
            Nodes.Add(node);
            Debug.Log("Added node at " + position);
            return node;
        }

        public FrameSection ConnectNodes(Node a, Node b)
        {
            FrameSection fs = new FrameSection()
            {
                from = a,
                to = b
            };
            Skeleton.Connect(a, b, fs);
            Debug.Log("Connected nodes at " + a.Position + " and " + b.Position);
            return fs;
        }
    }
}