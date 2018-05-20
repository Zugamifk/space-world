using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lab
{
    public class MapModel
    {
        const int k_MapSide = 1024;
        public class Tile
        {
            public Vector2 Position;
        }

        public class Room : Tile
        {
            public Vector2Int Size;
        }

        public Tile[,] Tiles;

        // todo: put this somewhere
        public static MapModel Current;

        public MapModel()
        {
            Tiles = new Tile[k_MapSide, k_MapSide];
        }

        public Vector2Int Origin
        {
            get
            {
                return new Vector2Int(k_MapSide/2, k_MapSide/2);
            }
        }
    }
}