using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class Log
{
    const string k_DefaultClassColor = "E47771";
    const string k_DefaultCallerColor = "529371";
    const string k_DefaultArgColor = "662351";

    static readonly object k_DefaultObject = new object();
    class Decorator
    {
        const string k_FormatString = "[<color=#{0}>{1}</color>.<color=#{2}>{3}</color>] {4}";
        string m_ObjectString;
        string m_ClassColor;
        string m_ArgColor;
        public Decorator(object obj, string classColor, string argColor)
        {
            m_ObjectString = obj.GetType().ToString();
            m_ClassColor = classColor;
            m_ArgColor = argColor;
        }
        public string Format(string caller, string msg, params object[] args)
        {
            msg = string.Format(k_FormatString, m_ClassColor, m_ObjectString, k_DefaultCallerColor, caller, msg);
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = string.Format("<color=#{0}>{1}</color>", m_ArgColor, args[i].ToString());
            }
            msg = string.Format(msg, args);
            return msg;
        }
    }
    static Dictionary<int, Decorator> m_DecoratorLookup = new Dictionary<int, Decorator>();
    static Log()
    {
        Register(k_DefaultObject);
    }

    public static void Register(object obj, string classColor = k_DefaultClassColor, string argColor = k_DefaultArgColor)
    {
        int hash = obj.GetHashCode();
        Decorator decorator = new Decorator(obj, classColor, argColor);
        m_DecoratorLookup.Add(hash, decorator);
    }

    public static void Print(object obj, string msg,params object[] args)
    {
        if(obj == null)
        {
            obj = k_DefaultObject;
        }
        Decorator decorator = m_DecoratorLookup[obj.GetHashCode()];
        UnityEngine.Debug.Log(decorator.Format(GetCaller(), msg, args));
    }

    static string GetCaller()
    {
        StackFrame frame = new StackFrame(2);
        var method = frame.GetMethod();
        return method.Name; 
    }
}
