using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dictionaryx {

	public static void MatchKeys<TKey, TValueA, TValueB>(
        this Dictionary<TKey, TValueA> self, 
        Dictionary<TKey, TValueB> other, 
        System.Func<TKey, TValueA> generator, 
        System.Action<TKey, TValueA> finaliser)
    {
        foreach(var key in other.Keys)
        {
            if(!self.ContainsKey(key))
            {
                TValueA newValue = generator.Invoke(key);
                self.Add(key, newValue);
            }
        }
        foreach(var key in self.Keys)
        {
            if(!other.ContainsKey(key))
            {
                finaliser.Invoke(key, self[key]);
                self.Remove(key);
            }
        }
    }
}
