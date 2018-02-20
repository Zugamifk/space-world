using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoKeyDictionary<TFirstKey, TSecondKey, TValue> {

    public struct KeyPair {
        public TFirstKey first;
        public TSecondKey second;
        public KeyPair(TFirstKey f, TSecondKey s)
        {
            first = f;
            second = s;
        }
    }

    Dictionary<KeyPair, TValue> m_Dictionary;


    public TValue this[TFirstKey first, TSecondKey second]
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

    public bool ContainsKeys(TFirstKey first, TSecondKey second)
    {
        var key = new KeyPair(first, second);
        return m_Dictionary.ContainsKey(key);
    }

    public void Clear()
    {
        m_Dictionary.Clear();
    }

    public void Add(TFirstKey first, TSecondKey second, TValue value)
    {
        var key = new KeyPair(first, second);
        m_Dictionary.Add(key, value);
    }

    public bool Remove(TFirstKey first, TSecondKey second, TValue value)
    {
        var key = new KeyPair(first, second);
        return m_Dictionary.Remove(key);
    }

    public bool TryGetValue(TFirstKey first, TSecondKey second, out TValue value)
    {
        var key = new KeyPair(first, second);
        return m_Dictionary.TryGetValue(key, out value);
    }
}
