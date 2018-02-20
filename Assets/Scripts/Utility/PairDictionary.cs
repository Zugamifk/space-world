using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairDictionary<TKey, TValue>
{

    public struct KeyPair
    {
        public TKey first;
        public TKey second;
        public KeyPair(TKey a, TKey b)
        {
            int fh = a.GetHashCode();
            int sh = b.GetHashCode();
            if (fh < sh)
            {
                first = a;
                second = b;
            }
            else
            {
                first = b;
                second = a;
            }
        }
    }

    Dictionary<KeyPair, TValue> m_Dictionary;

    public PairDictionary()
    {
        m_Dictionary = new Dictionary<KeyPair, TValue>();
    }

    public TValue this[TKey first, TKey second]
    {
        get
        {
            return m_Dictionary[new KeyPair(first, second)];
        }
        set
        {
            m_Dictionary[new KeyPair(first, second)] = value;
        }
    }

    public Dictionary<KeyPair, TValue>.KeyCollection Keys
    {
        get
        {
            return m_Dictionary.Keys;
        }
    }

    public Dictionary<KeyPair, TValue>.ValueCollection Values
    {
        get
        {
            return m_Dictionary.Values;
        }
    }

    public int Count
    {
        get
        {
            return m_Dictionary.Count;
        }
    }

    public bool ContainsKeys(TKey first, TKey second)
    {
        var key = new KeyPair(first, second);
        return m_Dictionary.ContainsKey(key);
    }

    public void Clear()
    {
        m_Dictionary.Clear();
    }

    public void Add(TKey first, TKey second, TValue value)
    {
        var key = new KeyPair(first, second);
        m_Dictionary.Add(key, value);
    }

    public bool Remove(TKey first, TKey second, TValue value)
    {
        var key = new KeyPair(first, second);
        return m_Dictionary.Remove(key);
    }

    public bool TryGetValue(TKey first, TKey second, out TValue value)
    {
        var key = new KeyPair(first, second);
        return m_Dictionary.TryGetValue(key, out value);
    }

}