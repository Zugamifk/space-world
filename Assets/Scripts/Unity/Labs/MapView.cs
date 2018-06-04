using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab;

namespace Unity.Lab
{
    public class MapView : MonoBehaviour
    {
        List<Tile> m_Tiles = new List<Tile>();

        private void Awake()
        {
            Log.Register(this, "56ABAE");
        }

        public void Refresh(MapModel model)
        {
            foreach(var t in m_Tiles)
            {
                Destroy(t.gameObject);
            }
            m_Tiles.Clear();

            for (int x = 0; x < model.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < model.Tiles.GetLength(1); y++)
                {
                    var data = model.Tiles[x, y];
                    if (data != null)
                    {
                    Log.Print(this, "tile {0}", data.Identifier);
                        var t = AssetLookup.Get<Tile>();
                        t.transform.SetParent(transform);
                        t.transform.position = new Vector3(x, y, 0);
                        t.Identifier = data.Identifier;
                        m_Tiles.Add(t);
                    }
                }
            }
        }
    }
}