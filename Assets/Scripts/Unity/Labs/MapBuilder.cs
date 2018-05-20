using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lab.MapGenerator
{
    public class MapBuilder
    {
        MapModel m_Model;
        MapGrammar m_Grammar;
        MapParser m_Parser;

        public MapBuilder()
        {
            Log.Register(this, "409140");
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
            m_Model = new MapModel();
            m_Grammar = new MapGrammar();
            m_Grammar.Initialize();
            for(int i=0;i<5;i++)
            {
                m_Grammar.Iterate();
            }

            m_Parser = new MapParser();
            m_Parser.Initialize(m_Model);
            m_Parser.Parse(m_Grammar.CurrentIteration);
        }
        
    }
}