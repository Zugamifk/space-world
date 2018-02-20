using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

namespace Unity.UI
{
    [RequireComponent(typeof(UILineRenderer))]
    public class ShipFrameSection : MonoBehaviour
    {

        [SerializeField]
        int m_LinePoints;

        UILineRenderer linerenderer;
        Spline spline;
        ShipNode start;
        ShipNode end;

        private void Awake()
        {
            linerenderer = GetComponent<UILineRenderer>();
            spline = new Spline(1, 0, 0);
        }

        private void Update()
        {
            DebugDraw();
        }

        public void Rebuild()
        {
            if (start != null && end != null)
            {
                spline.SetPoint(start.transform.position, 0);
                spline.SetPoint(end.transform.position, 1);

                int count = m_LinePoints + 2;
                var pts = new Vector2[count];
                float t = 0;
                float step = 1 / (float)(count - 1);
                for (int i = 0; i < count; i++)
                {
                    pts[i] = spline.Evaluate(t);
                    t += step;
                }

                linerenderer.Points = pts;
            }
        }

        public void Initialize(ShipNode start, ShipNode end)
        {
            this.start = start;
            this.end = end;
            Rebuild();
        }

        void DebugDraw()
        {
            int count = 100;
            float step = 1 / (float)(count - 1);
            float t = step;
            Vector3 last = spline.Evaluate(0);
            for (int i = 0; i < count; i++)
            {
                var pt = spline.Evaluate(t);
                Debug.DrawLine(last, pt, Color.green);
                last = pt;
                t += step;
            }

        }


    }
}