using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<TVertex, TEdge>
{
    /// <summary>
    /// if true, re-adding an edge will replace the old one.
    /// </summary>
    public bool CanReplaceEdges = false;

    /// <summary>
    /// If true, connecting edges will connect them from a to b. If false, both will be connected to each other.
    /// </summary>
    public bool Directed = false;

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

    public bool Connect(TVertex a, TVertex b, TEdge edge)
    {
        if (a.Equals(b))
        {
            Debug.LogError("Both vertices are the same!");
            return false;
        }
        VertexInfo ai, bi;
        EdgeInfo ei;
        bool ac = m_Vertices.TryGetValue(a, out ai);
        bool bc = m_Vertices.TryGetValue(b, out bi);
        bool ec = m_Edges.TryGetValue(edge, out ei);
        if (ac && bc && !ec)
        {
            var info = new EdgeInfo()
            {
                edge = edge,
                a = ai,
                b = bi
            };
            if (ai.connected.Contains(bi))
            {
                if (CanReplaceEdges)
                {
                    var oldEdge = m_Connections[a, b];
                    m_Connections[a, b] = info;
                    m_Edges[edge] = info;
                    m_Edges.Remove(oldEdge.edge);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ai.connected.Add(bi);
                if (!Directed)
                {
                    bi.connected.Add(ai);
                }
                m_Edges.Add(edge, info);
                m_Connections.Add(a, b, info);
                return true;
            }
        }
        else
        {
            if (ec)
            {
                Debug.Log(a + " and " + b + " are already connected!");
            }
            else if (!ac && !bc)
            {
                Debug.Log("Can not connect " + a + " and " + b + ", graph does not contain either of them!");
            }
            else if (!ac)
            {
                Debug.Log("Can not connect " + a + " and " + b + ", graph does not contain " + a);
            }
            else if (!bc)
            {
                Debug.Log("Can not connect " + a + " and " + b + ", graph does not contain " + b);
            }
            return false;
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

    public bool HasEdge(TEdge edge)
    {
        return m_Edges.ContainsKey(edge);
    }

    public bool HasVertex(TVertex vertex)
    {
        return m_Vertices.ContainsKey(vertex);
    }

    public bool TryGetEdgeVertices(TEdge edge, out TVertex a, out TVertex b)
    {
        EdgeInfo info;
        if (m_Edges.TryGetValue(edge, out info))
        {
            a = info.a.vertex;
            b = info.b.vertex;
            return true;
        }
        else
        {
            a = default(TVertex);
            b = default(TVertex);
            return false;
        }
    }

    public bool TryGetEdge(TVertex a, TVertex b, out TEdge edge)
    {
        EdgeInfo info;
        if (m_Connections.TryGetValue(a, b, out info))
        {
            edge = info.edge;
            return true;
        }
        else
        {
            edge = default(TEdge);
            return false;
        }
    }

    public int GetConnectedEdges(TVertex vert, IList<TEdge> results)
    {
        VertexInfo info;
        if (m_Vertices.TryGetValue(vert, out info))
        {
            foreach (var connected in info.connected)
            {
                EdgeInfo edge;
                if (m_Connections.TryGetValue(vert, connected.vertex, out edge))
                {
                    results.Add(edge.edge);
                }
                else
                {
                    Debug.LogError("Error in vertex connections! Vertex " + vert + " has a connection to " + connected.vertex + " but no edge info!");
                }
            }
            return info.connected.Count;
        }
        else
        {
            Debug.LogError("Vertex " + vert + " is not a part of graph " + this);
            return 0;
        }
    }

    public IEnumerable<TVertex> GetConnected(TVertex vert)
    {
        VertexInfo info;
        if (m_Vertices.TryGetValue(vert, out info))
        {
            foreach (var v in info.connected)
            {
                yield return v.vertex;
            }
        }
        else
        {
            Debug.LogError("Vertex " + vert + " is not a part of graph " + this);
        }
    }
}
