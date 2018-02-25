using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using Game.Ship;

namespace Unity.UI
{
    [RequireComponent(typeof(UILineRenderer))]
    public class ShipFrameSection : MonoBehaviour
    {

        [SerializeField]
        int m_LinePoints;

        UILineRenderer linerenderer;
        Spline spline;

        Structure.FrameSection frame;

        private void Awake()
        {
            linerenderer = GetComponent<UILineRenderer>();
            spline = new Spline(0, 0, 0);
        }

        private void Update()
        {
            spline.DebugDraw(Color.green);
        }

        public void Rebuild()
        {
            var a = frame.from.Position;
            var b = frame.to.Position;
            spline.SetPoint(a + frame.fromTangeant, 0);
            spline.SetPoint(a, 1);
            spline.SetPoint(b, 2);
            spline.SetPoint(b + frame.toTangeant, 3);

            int count = m_LinePoints + 2;
            var pts = new Vector2[count];
            float t = 1;
            float step = 1 / (float)(count - 1);
            for (int i = 0; i < count; i++)
            {
                pts[i] = spline.Evaluate(t);
                t += step;
            }

            linerenderer.Points = pts;
        }

        public void Initialize(Structure.FrameSection frame)
        {
            this.frame = frame;
            Rebuild();
        }
    }
}