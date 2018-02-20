using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Ship;

namespace Unity.UI
{
    public class ShipView : MonoBehaviour
    {

        [SerializeField]
        RectTransform m_NodesRoot;
        [SerializeField]
        RectTransform m_FrameRoot;

        [SerializeField]
        ShipNode m_ShipNodeTemplate;
        [SerializeField]
        ShipFrameSection m_ShipFrameTemplate;

        ObjectPool<ShipNode> m_NodePool;
        ObjectPool<ShipFrameSection> m_FramePool;

        Game.Ship.Ship m_Ship;

        Dictionary<Structure.Node, ShipNode> m_NodeLookup;
        Dictionary<Structure.FrameSection, ShipFrameSection> m_FrameLookup;

        private void Awake()
        {
            m_NodePool = new ObjectPool<ShipNode>(m_ShipNodeTemplate);
            m_NodeLookup = new Dictionary<Structure.Node, ShipNode>();

            m_FramePool = new ObjectPool<ShipFrameSection>(m_ShipFrameTemplate);
            m_FrameLookup = new Dictionary<Structure.FrameSection, ShipFrameSection>();
        }

        public void Initialize(Game.Ship.Ship ship)
        {
            m_Ship = ship;
        }
        
        public void RebuildView()
        {
            var nodes = m_Ship.structure.Nodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                Structure.Node node = nodes[i];
                ShipNode sn;
                if (!m_NodeLookup.TryGetValue(nodes[i], out sn))
                {
                    var nodegraphic = m_NodePool.Get();
                    nodegraphic.gameObject.SetActive(true);
                    nodegraphic.transform.SetParent(m_NodesRoot);
                    nodegraphic.transform.position = node.Position;
                    m_NodeLookup.Add(nodes[i], nodegraphic);
                }
            }

            var skeleton = m_Ship.structure.Skeleton;
            foreach (var e in skeleton.AllEdges())
            {
                ShipFrameSection section;
                if (!m_FrameLookup.TryGetValue(e, out section))
                {
                    var frame = m_FramePool.Get();
                    frame.gameObject.SetActive(true);
                    frame.transform.SetParent(m_FrameRoot);
                    Structure.Node a, b;
                    if (skeleton.GetEdgeVertices(e, out a, out b))
                    {
                        ShipNode ag = m_NodeLookup[a];
                        ShipNode bg = m_NodeLookup[b];
                        frame.Initialize(ag, bg);
                    }

                    m_FrameLookup.Add(e, frame);
                }
            }
        }
    }
}