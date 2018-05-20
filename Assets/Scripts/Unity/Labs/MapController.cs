using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab.MapGenerator;

namespace Game.Lab
{
    public class MapController
    {
        MapBuilder m_Builder;

        public MapController()
        {
            m_Builder = new MapBuilder();
        }

        public void GenerateMap()
        {
            m_Builder.Generate();
            MapModel.Current = m_Builder.Current;
        }
    }
}