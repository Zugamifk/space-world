using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Spline
{
    public List<Vector2> Points;
    public float Tension;
    public float Continuity;
    public float Bias;

    public Spline(float tension = 0, float continuity = 0, float bias = 0)
    {
        Points = new List<Vector2>();
        Tension = tension;
        Continuity = continuity;
        Bias = bias;
    }

    public void AddPoint(Vector2 point, int index = -1)
    {
        if (index < 0)
        {
            index = Points.Count;
        }
        Points.Insert(index, point);
    }

    public void SetPoint(Vector2 point, int index)
    {
        if (index < Points.Count)
        {
            Points[index] = point;
        }
        else
        {
            Points.Insert(index, point);
        }
    }

    public Vector2 Evaluate(float x)
    {
        int k = Mathf.FloorToInt(x);
        Vector2 p1 = Points[k];
        Vector2 p0 = Points[Mathf.Max(k - 1, 0)];
        Vector2 p2 = Points[Mathf.Min(k + 1, Points.Count - 1)];
        Vector2 p3 = Points[Mathf.Min(k + 2, Points.Count - 1)];

        Vector2 m0 = ((1 - Tension) * (1 + Bias) * (1 + Continuity) / 2) * (p1 - p0) +
            ((1 - Tension) * (1 + Bias) * (1 + Continuity) / 2) * (p2 - p1);
        Vector2 m1 = ((1 - Tension) * (1 + Bias) * (1 - Continuity) / 2) * (p2 - p1) +
           ((1 - Tension) * (1 - Bias) * (1 + Continuity) / 2) * (p3 - p2);

        float t = x % 1;
        float h00 = (1 + 2 * t) * (1 - t) * (1 - t);
        float h10 = t * (1 - t) * (1 - t);
        float h01 = t * t * (3 - 2 * t);
        float h11 = t * t * (t - 1);

        return h00 * p1 + h10 * m0 + h01 * p2 + h11 * m1;
    }
}
