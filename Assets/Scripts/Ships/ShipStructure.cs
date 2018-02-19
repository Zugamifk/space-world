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

        public Graph<Node> Skeleton;
        public List<Node> Nodes;

        public Structure()
        {
            Skeleton = new Graph<Node>();
            Nodes = new List<Node>();
        }

        public void AddNode(Vector2 position)
        {
            var node = new Node()
            {
                Position = position
            };
            Skeleton.Add(node);
            Nodes.Add(node);
            Debug.Log("Added node at " + position);
        }
    }
}