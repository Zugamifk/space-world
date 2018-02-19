using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Ship;

namespace Unity.UI
{
    public class ShipBuildingScreen : MonoBehaviour
    {
        enum ECursorMode
        {
            None,
            Build
        }

        [SerializeField]
        Image m_BuildCursor;
        [SerializeField]
        ShipNode m_ShipNodeTemplate;
        [SerializeField]
        RectTransform m_BuildAreaRoot;

        ECursorMode m_CurrentCursorMode;

        IdleMode m_IdleMode;
        BuildMode m_BuildMode;
        Mode m_CurrentMode;

        ShipBuilder m_ShipBuilder;

        ObjectPool<ShipNode> m_NodePool;

        Dictionary<Structure.Node, ShipNode> m_NodeLookup;

        private void Awake()
        {
            m_ShipBuilder = new ShipBuilder(null);

            m_IdleMode = new IdleMode();
            m_BuildMode = new BuildMode()
            {
                Cursor = m_BuildCursor,
                Builder = m_ShipBuilder 
            };

            m_NodePool = new ObjectPool<ShipNode>(m_ShipNodeTemplate);
            m_NodeLookup = new Dictionary<Structure.Node, ShipNode>();
        }

        void Update()
        {
            if(m_CurrentMode?.Cursor !=null)
            {
                m_CurrentMode.Cursor.rectTransform.position = Input.mousePosition;
            }
        }

        void Initialize()
        {
            SetMode(m_IdleMode);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            if(active)
            {
                Initialize();
                WorldInput.Instance.SetCustomMode(UpdateInput);
            } else
            {
                WorldInput.Instance.EndCustomMode();
            }
        }

        void UpdateInput(WorldInput.InputState input)
        {
        }

        void SetMode(Mode mode)
        {
            if(m_CurrentMode!=null)
            {
                SetModeActive(m_CurrentMode, false);
            }

            SetModeActive(mode, true);
        }

        void RebuildBuildingArea()
        {
            var nodes = m_ShipBuilder.Ship.structure.Nodes;
            for(int i =0;i<nodes.Count;i++)
            {
                Structure.Node node = nodes[i];
                ShipNode sn;
                if(!m_NodeLookup.TryGetValue(nodes[i], out sn))
                {
                    var nodegraphic = m_NodePool.Get();
                    nodegraphic.gameObject.SetActive(true);
                    nodegraphic.transform.SetParent(m_BuildAreaRoot);
                    nodegraphic.transform.position = node.Position;
                    m_NodeLookup.Add(nodes[i], nodegraphic);
                }
            }
        }

        // ===============================
        // Modes
        // ===============================

        void SetModeActive(Mode mode, bool active)
        {
            if (mode.Cursor != null)
            {
                mode.Cursor.enabled = active;
            }

            if(active)
            {
                m_CurrentMode = mode;
            } else
            {
                m_CurrentMode = null;
            }
        }

        class Mode
        {
            public Image Cursor;
            public virtual bool OnClickedBackground() { return false; }
        }

        class IdleMode : Mode
        {

        }

        class BuildMode : Mode
        {
            public ShipBuilder Builder;

            public override bool OnClickedBackground()
            {
                Builder.AddNode(Input.mousePosition);
                return true;
            }
        }

        // ===============================
        // Button Callbacks
        // ===============================
        public void PressedBuild()
        {
            SetMode(m_BuildMode);
        }

        public void PressedBackground()
        {
            if(m_CurrentMode.OnClickedBackground())
            {
                RebuildBuildingArea();
            }
        }
    }
}