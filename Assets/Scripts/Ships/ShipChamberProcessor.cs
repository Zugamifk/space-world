using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Ship.Builder
{
    public class ShipChamberProcessor
    {
        class NodeInfo
        {
            public Structure.Node node;
            public List<Structure.Frame> connectedFrames;
        }
        Structure m_Structure;
        List<Structure.Frame> m_ProcessingChambers;
        Queue<NodeInfo> m_OpenNodes;
        Dictionary<Structure.Node, NodeInfo> m_Info;
        HashSet<NodeInfo> m_Visited;

        public ShipChamberProcessor(Structure structure)
        {
            Log.Register(this, "236657");
            m_Structure = structure;
            m_OpenNodes = new Queue<NodeInfo>();
            m_ProcessingChambers = new List<Structure.Frame>();
        }

        public void UpdateChambers()
        {
            float x = float.MinValue;
            float y = float.MinValue;
            Structure.Frame directedFrame = new Structure.Frame();
            directedFrame.Directed = true;
            NodeInfo first = null;
            m_Info.Clear();
            foreach (var n in m_Structure.Skeleton.AllVertices())
            {
                directedFrame.Add(n);
                var info = new NodeInfo()
                {
                    node = n,
                    connectedFrames = new List<Structure.Frame>()
                };
                m_Info.Add(n, info);
                if (first == null || NodeSorter(n, first.node) < 0)
                {
                    first = info;
                }
            }
            foreach (var e in m_Structure.Skeleton.AllEdges())
            {
                if (NodeSorter(e.from, e.to) < 0)
                {
                    directedFrame.Connect(e.from, e.to, e);
                }
            }
            m_OpenNodes.Clear();
            m_ProcessingChambers.Clear();
            m_OpenNodes.Enqueue(first);
            while (m_OpenNodes.Count > 0)
            {
                var currentNode = m_OpenNodes.Dequeue();
                Structure.Node lastNode = null;
                bool added = false;
                foreach (var n in m_Structure.Skeleton.GetConnected(currentNode.node))
                {
                    Log.Print(this, "{0} is connected to {1}", currentNode, n);
                    if (lastNode != null)
                    {
                        var newChamber = new Structure.Frame();
                        newChamber.Add(currentNode.node);
                        newChamber.Add(n);
                        newChamber.Add(lastNode);
                        m_ProcessingChambers.Add(newChamber);
                        m_Info[n].connectedFrames.Add(newChamber);
                        m_Info[lastNode].connectedFrames.Add(newChamber);
                     //   if()
                        m_OpenNodes.Enqueue(m_Info[lastNode]);
                        added = true;
                    }
                    lastNode = n;
                }
                if(added)
                {
                    m_OpenNodes.Enqueue(m_Info[lastNode]);
                }
            }
            foreach (var c in m_ProcessingChambers)
            {
                Log.Print(this, "Chamber {0}", c);
            }
        }

        int NodeSorter(Structure.Node a, Structure.Node b)
        {
            float diff = a.Position.x - b.Position.x;
            if (Mathf.Approximately(diff, 0))
            {
                diff = a.Position.y - b.Position.y;
            }
            return Mathf.RoundToInt(Mathf.Sign(diff));
        }

    }
}
