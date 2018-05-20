using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lab.MapGenerator
{
    public class MapBuilder
    {
        const int k_IterCount = 3;

        MapModel m_Model;
        MapGrammar m_Grammar;
        MapParser m_Parser;

        bool m_Initialized;

        public MapBuilder()
        {
            Log.Register(this, "409140");

            m_Grammar = new Grammar.TestGrammar();
            m_Parser = new MapParser();
            m_Initialized = false;
        }

        public MapModel Current
        {
            get
            {
                return m_Model;
            }
        }

        public void Generate()
        {
            Initialize();
            for (int i=0;i<k_IterCount;i++)
            {
                StepIteration();
            }
            Build();
        }

        public MapBuilder Initialize()
        {
            m_Model = new MapModel();
            m_Grammar.Initialize();
            m_Initialized = true;
            return this;
        }
        
        public MapBuilder StepIteration()
        {
            if(!m_Initialized)
            {
                Initialize();
            }
            m_Grammar.Iterate();
            return this;
        }

        public MapBuilder Build()
        {
            m_Parser.Initialize(m_Model);
            m_Parser.Parse(m_Grammar.CurrentIteration);
            return this;
        }
    }
}