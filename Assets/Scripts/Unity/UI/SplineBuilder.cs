using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineBuilder : MonoBehaviour {


    [SerializeField]
    Transform[] m_transforms;
    [SerializeField]
    float tension;
    [SerializeField]
    float continuity;
    [SerializeField]
    float bias;

    Spline spline;
	// Use this for initialization
	void Start () {
        spline = new Spline(tension, continuity, bias );
        foreach(var tf in m_transforms)
        {
            spline.AddPoint(tf.position);
        }
	}
	
	// Update is called once per frame
	void Update () {
        spline.Tension = tension;
        spline.Continuity = continuity;
        spline.Bias = bias;
        for (int i=0;i<m_transforms.Length;i++)
        {
            spline.SetPoint(m_transforms[i].position, i);
        }
        spline.DebugDraw(Color.red);
	}
}
