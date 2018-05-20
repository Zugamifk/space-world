using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab;

namespace Unity.Lab
{
    public class MapView : MonoBehaviour
    {
        bool m_Initialized;
        List<Tile> m_Tiles;

        private void Update()
        {
            if(!m_Initialized && MapModel.Current!=null) {
                Initialize();
            }
        }

        void Initialize()
        {
            var model = MapModel.Current;
            m_Initialized = true;
            m_Tiles = new List<Tile>();
            for (int x=0;x<model.Tiles.GetLength(0);x++)
            {
                for (int y = 0; y < model.Tiles.GetLength(1); y++)
                {
                    var data = model.Tiles[x, y];
                    if (data != null)
                    {
                        var t = AssetLookup.Get<Tile>();
                        t.transform.SetParent(transform);
                        t.transform.position = new Vector3(x,y,0);
                        m_Tiles.Add(t);
                    }
                }
            }
        }
    }
}