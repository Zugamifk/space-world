using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<TVertex> {

    class VertexInfo
    {
        public TVertex vertex;
        public HashSet<VertexInfo> connected;
    }

    Dictionary<TVertex, VertexInfo> m_Vertices;

    public Graph()
    {
        m_Vertices = new Dictionary<TVertex, VertexInfo>();
    }

    public void Add(TVertex v)
    {
        var info = new VertexInfo()
        {
            vertex = v,
            connected = new HashSet<VertexInfo>()
        };
        m_Vertices.Add(v, info);
    }

    public void Connect(TVertex a, TVertex b)
    {
        VertexInfo ai, bi;
        bool ac = m_Vertices.TryGetValue(a, out ai);
        bool bc = m_Vertices.TryGetValue(b, out bi);
        if (ac && bc)
        {
            ai.connected.Add(bi);
            bi.connected.Add(ai);
        } else
        {
            if(!ac && !bc)
            {
                Debug.Log("Can not connect " + a + " and " + b + ", graph does not contain either of them!");
            }
            else if(!ac)
            {
                Debug.Log("Can not connect " + a + " and " + b + ", graph does not contain " + a);
            } else if (!bc)
            {
                Debug.Log("Can not connect " + a + " and " + b + ", graph does not contain " + b);
            }
        }
    }

    public void TraverseDFS()
    {
        Queue<VertexInfo> queue = new Queue<VertexInfo>();
        foreach(var v in m_Vertices.Values)
        {

        }
    }
}
