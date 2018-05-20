using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab.MapGenerator.Token;

namespace Game.Lab.MapGenerator
{
    public class MapParser
    {
        enum EOrientation
        {
            North,
            East,
            South,
            West
        }

        EOrientation m_Orientation = EOrientation.North;
        Vector2Int m_Position = Vector2Int.zero;
        MapModel m_CurrentModel;

        public void Initialize(MapModel model)
        {
            m_Position = model.Origin;
            m_Orientation = EOrientation.North;
            m_CurrentModel = model;
        }

        public void Parse(IEnumerable<MapToken> tokens)
        {
            foreach (var t in tokens)
            {
                t.Visit(this);
            }
        }

        public void Consume(MapToken token)
        {

        }

        public void Consume(Hall hall)
        {
            int x = m_Position.x;
            int y = m_Position.y;
            m_CurrentModel.Tiles[x, y] = new MapModel.Tile()
            {
                Position = m_Position
            };
            Step();
        }

        public void Consume(Room room)
        {
            int x = m_Position.x;
            int y = m_Position.y;
            int w = 3;
            int h = 3;
            var mr = new MapModel.Room()
            {
                Position = m_Position,
                Size = new Vector2Int(w, h)
            };
            for (int xi = 0; xi < w; xi++)
            {
                for (int yi = 0; yi < h; yi++)
                {
                    m_CurrentModel.Tiles[x + xi, y + yi] = mr;
                }
            }
            Step();
        }

        void Step()
        {
            switch (m_Orientation)
            {
                case EOrientation.North:
                    m_Position += Vector2Int.up;
                    break;
                case EOrientation.East:
                    m_Position += Vector2Int.right;
                    break;
                case EOrientation.South:
                    m_Position += Vector2Int.down;
                    break;
                case EOrientation.West:
                    m_Position += Vector2Int.left;
                    break;
                default:
                    break;
            }
        }
    }
}