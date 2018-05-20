#define LOG_SHIP_STRUCTURE
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
            public Vector2 fromTangeant;
            public Node to;
            public Vector2 toTangeant;
            public void SetTangeant(Node node, Vector2 tangeant)
            {
                if (node == from)
                {
                    fromTangeant = tangeant;
                } else if(node == to)
                {
                    toTangeant = tangeant;
                } else
                {
                    Debug.LogError(node + " node is not connected to " + this);
                }
            }
        }
        public class Frame : Graph<Node, FrameSection> { }

        public Frame Skeleton;
        public List<Node> Nodes;

        public Structure()
        {
            Skeleton = new Frame();
            Nodes = new List<Node>();
#if LOG_SHIP_STRUCTURE
            Log.Register(this, "5A6468");
#endif
        }

        public Node AddNode(Vector2 position)
        {
            var node = new Node()
            {
                Position = position
            };
            Skeleton.Add(node);
            Nodes.Add(node);
            Log.Print(this, "Added node at {0}", position);
            return node;
        }

        public FrameSection ConnectNodes(Node a, Node b)
        {
            var diff = b.Position - a.Position;
            FrameSection fs = new FrameSection()
            {
                from = a,
                fromTangeant = diff,
                to = b,
                toTangeant = -diff
            };
            if (Skeleton.Connect(a, b, fs))
            {
                Log.Print(this, "Connected nodes at {0} and {1}", a.Position, b.Position);
            }
            return fs;
        }

        // possibly return a float indicating actual change in angle
        public void RotateEdgeNode(Node node, FrameSection frame, float angle)
        {
            var tan = Vector2.right.Rotate(angle);
            frame.SetTangeant(node, tan*500);
        }

        // todo: this should get all points surrounding the outside of the ship
        public List<Vector2> GetOuterHullPoints()
        {
            Node first = null;
            foreach(var vert in Skeleton.AllVertices())
            {
                if(first == null ||
                    first.Position.x > vert.Position.x)
                {
                    first = vert;
                }
            }

            var result = new List<Vector2>();
            return result;
        }
    }
}