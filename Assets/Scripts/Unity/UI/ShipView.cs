using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        Image m_SelectedNodeCursor;

        [SerializeField]
        ShipNode m_ShipNodeTemplate;
        [SerializeField]
        ShipFrameSection m_ShipFrameTemplate;
        [SerializeField]
        ShipNodeConnectionHandle m_NodeConnectionHandleTemplate;

        ObjectPool<ShipNode> m_NodePool;
        ObjectPool<ShipFrameSection> m_FramePool;
        ObjectPool<ShipNodeConnectionHandle> m_NodeHandlePool;

        Structure.Node m_SelectedNode;
        List<ShipNodeConnectionHandle> m_ActiveNodeHandles;

        Game.Ship.Ship m_Ship;

        Dictionary<Structure.Node, ShipNode> m_NodeLookup;
        Dictionary<Structure.FrameSection, ShipFrameSection> m_FrameLookup;

        public delegate void NodeCallback(Structure.Node node, ShipNode graphic);
        public NodeCallback OnAddedNode;

        public delegate void NodeRotationCallback(Structure.Node node, Structure.FrameSection frame, float angle);
        public NodeRotationCallback OnRotateFrameNode;

        private void Awake()
        {
            m_NodePool = new ObjectPool<ShipNode>(m_ShipNodeTemplate);
            m_NodeLookup = new Dictionary<Structure.Node, ShipNode>();

            m_FramePool = new ObjectPool<ShipFrameSection>(m_ShipFrameTemplate);
            m_FrameLookup = new Dictionary<Structure.FrameSection, ShipFrameSection>();

            m_NodeHandlePool = new ObjectPool<ShipNodeConnectionHandle>(m_NodeConnectionHandleTemplate);
            m_ActiveNodeHandles = new List<ShipNodeConnectionHandle>();
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
                    sn =AddNode(nodes[i]);
                }
                sn.transform.position = node.Position;
            }

            var skeleton = m_Ship.structure.Skeleton;
            foreach (var e in skeleton.AllEdges())
            {
                ShipFrameSection section;
                if (!m_FrameLookup.TryGetValue(e, out section))
                {
                    section = m_FramePool.Get();
                    section.gameObject.SetActive(true);
                    section.transform.SetParent(m_FrameRoot);
                    m_FrameLookup.Add(e, section);
                }
                section.Initialize(e);
            }
        }

        ShipNode AddNode(Structure.Node node)
        {
            var nodegraphic = m_NodePool.Get();
            nodegraphic.gameObject.SetActive(true);
            nodegraphic.transform.SetParent(m_NodesRoot);
            m_NodeLookup.Add(node, nodegraphic);

            if (OnAddedNode != null)
            {
                OnAddedNode.Invoke(node, nodegraphic);
            }
            return nodegraphic;
        }

        public void SelectNode(Structure.Node node)
        {
            SelectNode(m_SelectedNode, false);
            if (node != null)
            {
                SelectNode(node, true);
            }
        }

        void SelectNode(Structure.Node node, bool enabled)
        {
            m_SelectedNodeCursor.enabled = enabled;
            if (node != null)
            {
                var g = m_NodeLookup[node];
                if (enabled)
                {
                    var worker = new List<Structure.FrameSection>();
                    int len = m_Ship.structure.Skeleton.GetConnectedEdges(node, worker);
                    m_SelectedNodeCursor.transform.position = g.transform.position;

                    for (int i = 0; i < len; i++)
                    {
                        var handle = m_NodeHandlePool.Get();
                        handle.transform.SetParent(g.transform, false);
                        handle.gameObject.SetActive(true);
                        handle.OnDragNode += OnDragNodeHandle;
                        handle.Initialize(node, worker[i]);
                        Debug.Log("handle " + handle, handle);
                        m_ActiveNodeHandles.Add(handle);
                    }
                    m_SelectedNode = node;
                }
                else
                {
                    for (int i = 0; i < m_ActiveNodeHandles.Count; i++)
                    {
                        m_ActiveNodeHandles[i].gameObject.SetActive(false);
                        m_NodeHandlePool.Return(m_ActiveNodeHandles[i]);
                    }
                    m_ActiveNodeHandles.Clear();
                    m_SelectedNode = null;
                }
            }
        }

        void OnDragNodeHandle(Structure.Node node, Structure.FrameSection frame, Vector2 position)
        {
            if(m_SelectedNode!=null)
            {
                var dir = position - (Vector2)m_SelectedNode.Position;
                var angle = Vector2.SignedAngle(Vector2.right, dir);
                OnRotateFrameNode.Invoke(node, frame, angle);
            } else
            {
                Debug.LogError("Selected node is null! No Node handles should be active!");
            }
        }
    }


}