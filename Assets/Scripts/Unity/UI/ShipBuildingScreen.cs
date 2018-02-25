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
        Mode m_CurrentMode;

        ShipBuilder m_ShipBuilder;

        private void Awake()
        {
            m_ShipBuilder = new ShipBuilder(null);
            m_ShipView.Initialize(m_ShipBuilder.Ship);

            m_IdleMode = new IdleMode();
            m_BuildMode = new BuildMode()
            {
                Screen =  this,
                Cursor = m_BuildCursor,
                Builder = m_ShipBuilder
            };

            m_ShipView.OnAddedNode += AddedNode;
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

        void SetMode(Mode mode)
        {
            if (m_CurrentMode != null)
            {
                SetModeActive(m_CurrentMode, false);
            }

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
            if (mode.Cursor != null)
            {
                mode.Cursor.enabled = active;
            }

            if (active)
            {
                m_CurrentMode = mode;
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
            public virtual void ConsumeInput(WorldInput.InputState input) { }
            public virtual bool OnClickedBackground() { return false; }
        }

        class IdleMode : Mode
        {

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
            public override bool OnClickedBackground()
            {
                Screen.SetIdleMode();
                return false;
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
            m_NodeInfoPanel.Show(node);
        }
    }
}