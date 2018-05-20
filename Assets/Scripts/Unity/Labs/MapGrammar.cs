using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab.MapGenerator.Token;

namespace Game.Lab.MapGenerator
{
    public class MapGrammar
    {
        List<MapToken> m_CurrentIteration;
        List<MapToken> m_IterationWorker;
        int m_IterationCount;

        public List<MapToken> CurrentIteration
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

        public void Consume(MapToken token)
        {
            AddToken(token);
        }

        public void Consume(Start token)
        {
            AddToken(new Room());
        }

        public void Consume(Room token)
        {
            AddToken(new Hall());
            AddToken(new Room());
        }

        public void Consume(Hall hall)
        {
            AddToken(new Hall());
            AddToken(Turn.Left());
            AddToken(new Hall());
        }

        void AddToken(MapToken token)
        {
            m_IterationWorker.Add(token);
        }

        public void Initialize()
        {
            m_IterationWorker = new List<MapToken>();
            m_CurrentIteration = new List<MapToken>();
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
                sb.Append("["+t+"]");
                sb.Append(" ");
            }
            Log.Print(this, sb.ToString());
        }
    }
}