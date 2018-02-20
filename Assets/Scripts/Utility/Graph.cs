using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<TVertex, TEdge> {

    class VertexInfo
    {
        public TVertex vertex;
        public HashSet<VertexInfo> connected;
    }

    class EdgeInfo
    {
        public TEdge edge;
        public VertexInfo a;
        public VertexInfo b;
    }

    Dictionary<TVertex, VertexInfo> m_Vertices;
    Dictionary<TEdge, EdgeInfo> m_Edges;
    PairDictionary<TVertex, EdgeInfo> m_Connections;

    public Graph()
    {
        m_Vertices = new Dictionary<TVertex, VertexInfo>();
        m_Edges = new Dictionary<TEdge, EdgeInfo>();
        m_Connections = new PairDictionary<TVertex, EdgeInfo>();
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

    public void Connect(TVertex a, TVertex b, TEdge edge)
    {
        VertexInfo ai, bi;
        EdgeInfo ei;
        bool ac = m_Vertices.TryGetValue(a, out ai);
        bool bc = m_Vertices.TryGetValue(b, out bi);
        bool ec = m_Edges.TryGetValue(edge, out ei);
        if (ac && bc && !ec)
        {
            ai.connected.Add(bi);
            bi.connected.Add(ai);
            var info = new EdgeInfo()
            {
                edge = edge,
                a = ai,
                b = bi
            };
            m_Edges.Add(edge, info);
            m_Connections.Add(a, b, info);
        } else
        {
            if(ec)
            {
                Debug.Log(a + " and " + b + " are already connected!");
            } else if(!ac && !bc)
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

    public IEnumerable<TEdge> AllEdges()
    {
        foreach (var e in m_Edges.Keys)
        {
            yield return e;
        }
    }

    public IEnumerable<TVertex> AllVertices()
    {
        foreach (var v in m_Vertices.Keys)
        {
            yield return v;
        }
    }

    public bool GetEdgeVertices(TEdge edge, out TVertex a, out TVertex b) {
        EdgeInfo info;
        if(m_Edges.TryGetValue(edge, out info))
        {
            a = info.a.vertex;
            b = info.b.vertex;
            return true;
        } else
        {
            a = default(TVertex);
            b = default(TVertex);
            return false;
        }
    }
}
