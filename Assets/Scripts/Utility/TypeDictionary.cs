using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeDictionary<TValue>
{
    Dictionary<System.Type, TValue> m_Dictionary = new Dictionary<System.Type, TValue>();

    public void Set<TKey>(TValue value)
    {
        var key = typeof(TKey);
        m_Dictionary.Add(key, value);
    }

    public TValue Get<TKey>()
    {
        TValue result;
        if(!m_Dictionary.TryGetValue(typeof(TKey), out result))
        {
            result = default(TValue);
        }
        return result;
    }

    public bool ContainsKey<TKey>()
    {
        return m_Dictionary.ContainsKey(typeof(TKey));
    }
}
