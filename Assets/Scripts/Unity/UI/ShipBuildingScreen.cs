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
        RectTransform m_BuildAreaRoot;
        [SerializeField]
        ShipView m_ShipView;
        [SerializeField]
        ShipNodeInfoPanel m_NodeInfoPanel;

        ECursorMode m_CurrentCursorMode;

        IdleMode m_IdleMode;
        BuildMode m_BuildMode;
        NodeEditMode m_NodeEditMode;
        Mode m_CurrentMode;

        ShipBuilder m_ShipBuilder;

        private void Awake()
        {
            m_ShipBuilder = new ShipBuilder(null);
            m_ShipView.Initialize(m_ShipBuilder.Ship);

            m_IdleMode = new IdleMode()
            {
                Screen = this,
            };
            m_BuildMode = new BuildMode()
            {
                Screen =  this,
                Cursor = m_BuildCursor,
                Builder = m_ShipBuilder
            };
            m_NodeEditMode = new NodeEditMode()
            {
                Screen = this,
                Builder = m_ShipBuilder
            };

            m_ShipView.OnAddedNode += AddedNode;
            m_ShipView.OnRotateFrameNode += OnUsedNodeHandle;
        }

        void Update()
        {
            if (m_CurrentMode?.Cursor != null)
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
            if (active)
            {
                Initialize();
                WorldInput.Instance.SetCustomMode(UpdateInput);
            }
            else
            {
                WorldInput.Instance.EndCustomMode();
            }
        }

        void UpdateInput(WorldInput.InputState input)
        {
            m_CurrentMode.ConsumeInput(input);
        }

        void SetIdleMode()
        {
            SetMode(m_IdleMode);
        }

        void SetNodeEditMode(Structure.Node node)
        {
            m_NodeInfoPanel.Show(node);
            SetMode(m_NodeEditMode);
            m_NodeEditMode.OnSelectedNode(node);
        }

        void SetMode(Mode mode)
        {
            if (m_CurrentMode != null)
            {
                SetModeActive(m_CurrentMode, false);
            }
            Debug.Log("currrent mode is " + mode.GetType().ToString());
            SetModeActive(mode, true);
        }

        // refactor as neededd
        void RebuildBuildingArea()
        {
            m_ShipView.RebuildView();
        }

        // ===============================
        // Modes
        // ===============================

        void SetModeActive(Mode mode, bool active)
        {
            if(m_CurrentMode!=null)
            {
                m_CurrentMode.SetActive(active);
            }
            if (active)
            {
                m_CurrentMode = mode;
                mode.SetActive(true);
            }
            else
            {
                m_CurrentMode = null;
            }
        }

        class Mode
        {
            public ShipBuildingScreen Screen;
            public Image Cursor;
            public virtual void SetActive(bool active)
            {
                if(Cursor!=null)
                {
                    Cursor.enabled = active;
                }
            }
            public virtual void ConsumeInput(WorldInput.InputState input) { }
            public virtual bool OnClickedBackground() { return false; }
            public virtual bool OnSelectedNode(Structure.Node node) { return false; }
            public virtual bool OnUsedNodeEdgeHandle(Structure.Node node, Structure.FrameSection section, float angle) { return false; }
        }

        class IdleMode : Mode
        {
            public override bool OnSelectedNode(Structure.Node node)
            {
                Screen.SetNodeEditMode(node);
                return false;
            }
        }

        class BuildMode : Mode
        {
            public ShipBuilder Builder;
            Structure.Node m_LastNode;

            public override void ConsumeInput(WorldInput.InputState input)
            {
                if(input.alt)
                {
                    Screen.SetIdleMode();
                }
            }

            public override bool OnSelectedNode(Structure.Node node)
            {
                if(m_LastNode!=null)
                {
                    Builder.ConnectNodes(m_LastNode, node);
                    m_LastNode = node;
                    return true;
                } else
                {
                    Screen.SetIdleMode();
                    return false;
                }
            }

            public override bool OnClickedBackground()
            {
                var newNode = Builder.AddNode(Input.mousePosition);
                if (m_LastNode != null)
                {
                    Builder.ConnectNodes(m_LastNode, newNode);
                }
                m_LastNode = newNode;
                return true;
            }
        }

        class NodeEditMode : Mode
        {
            public Structure.Node SelectedNode;
            public ShipBuilder Builder;

            public override void SetActive(bool active)
            {
                base.SetActive(active);
                if(!active)
                {
                    Screen.m_ShipView.SelectNode(null);
                }
            }

            public override bool OnClickedBackground()
            {
                Screen.SetIdleMode();
                return false;
            }

            public override bool OnSelectedNode(Structure.Node node)
            {
                SelectedNode = node;
                Screen.m_ShipView.SelectNode(node);
                return false;
            }

            public override bool OnUsedNodeEdgeHandle(Structure.Node node, Structure.FrameSection section, float angle)
            {
                Builder.RotateNode(node, section, angle);
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
            if (m_CurrentMode.OnClickedBackground())
            {
                RebuildBuildingArea();
            }
        }

        // ===============================
        // Other Callbacks
        // ===============================
        void AddedNode(Structure.Node node, ShipNode graphic)
        {
            graphic.SetOnClick(()=> SelectNode(node));
        }

        void SelectNode(Structure.Node node)
        {
            if(m_CurrentMode.OnSelectedNode(node))
            {
                RebuildBuildingArea();
            }
        }

        void OnUsedNodeHandle(Structure.Node node, Structure.FrameSection frame, float angle)
        {
            if(m_CurrentMode.OnUsedNodeEdgeHandle(node, frame, angle))
            {
                RebuildBuildingArea();
            }
        }
    }
}