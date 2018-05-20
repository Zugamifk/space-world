using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab.MapGenerator.Grammar;

namespace Game.Lab.MapGenerator
{
    public abstract class MapGrammar
    {
        List<IMapToken> m_CurrentIteration;
        List<IMapToken> m_IterationWorker;
        int m_IterationCount;

        public List<IMapToken> CurrentIteration
        {
            get
            {
                return m_CurrentIteration;
            }
        }

        public MapGrammar()
        {
            Log.Register(this, "9582CA");
        }

        public virtual void Consume(IMapToken token)
        {
            AddToken(token);
        }

        public virtual void Consume(Start token) { }

        public virtual void Consume(Room token) { }

        public virtual void Consume(Hall hall) { }

        protected void AddToken(IMapToken token)
        {
            m_IterationWorker.Add(token);
        }

        public void Initialize()
        {
            m_IterationWorker = new List<IMapToken>();
            m_CurrentIteration = new List<IMapToken>();
            m_CurrentIteration.Add(new Start());
            m_IterationCount = 0;
            LogWorker();
        }

        public void Iterate()
        {
            m_IterationCount++;

            for (int i = 0; i < m_CurrentIteration.Count; i++)
            {
                m_CurrentIteration[i].Visit(this);
            }
            var iter = m_CurrentIteration;
            m_CurrentIteration = m_IterationWorker;
            m_IterationWorker = iter;
            m_IterationWorker.Clear();

            LogWorker();
        }

        void LogWorker()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Current iteration: " + m_IterationCount + "\n");
            foreach (var t in m_CurrentIteration)
            {
                sb.Append("[" + t + "]");
                sb.Append(" ");
            }
            Log.Print(this, sb.ToString());
        }
    }
}