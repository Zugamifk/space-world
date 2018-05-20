using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab.MapGenerator;

namespace Game.Lab
{
    public class MapController
    {
        MapModel m_Model;
        MapBuilder m_Builder;

        public MapModel CurrentMap
        {
            get
            {
                return m_Model;
            }
        }

        public MapController()
        {
            m_Builder = new MapBuilder();
        }

        public void SetModel(MapModel model)
        {
            m_Model = model;
        }

        public void GenerateMap()
        {
            m_Builder.Generate();
            m_Model = m_Builder.Current;
        }

        public void ResetMap()
        {
            m_Builder.Initialize();
            m_Builder.Build();
            m_Model = m_Builder.Current;
        }

        public void StepMapIteration()
        {
            m_Builder.StepIteration();
            m_Builder.Build();
        }
    }
}