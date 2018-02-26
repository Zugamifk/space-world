using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool { }

public class ObjectPool<T> : ObjectPool where T : Object
{
    T m_Template;
    HashSet<T> m_Pool;
    Queue<T> m_FreeObjects;

    public ObjectPool(T template)
    {
        m_Template = template;
        m_Pool = new HashSet<T>();
        m_FreeObjects = new Queue<T>();
    }

    public T Get()
    {
        T result = null;
        if (m_FreeObjects.Count > 0)
        {
            result = m_FreeObjects.Dequeue();
        }
        else
        {
            result = Object.Instantiate<T>(m_Template);
            m_Pool.Add(result);
        }
        return result;
    }

    public void Return(T obj)
    {
        m_FreeObjects.Enqueue(obj);
    }
}
