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
            int x0 = 0;
            int y0 = 0;
            switch (m_Orientation)
            {
                case EOrientation.North:
                    x0 = Random.Range(0, -w);
                    break;
                case EOrientation.East:
                    y0 = Random.Range(0, -h);
                    break;
                case EOrientation.South:
                    y0 = 1- h;
                    goto case EOrientation.North;
                case EOrientation.West:
                    x0 = 1- w;
                    goto case EOrientation.East;
            }
            for (int xi = x0; xi < x0+w; xi++)
            {
                for (int yi = y0; yi < y0+h; yi++)
                {
                    m_CurrentModel.Tiles[x + xi, y + yi] = mr;
                }
            }
            Step();
        }

        public void Consume(Turn turn)
        {
            if (turn.IsLeft)
            {
                m_Orientation = (EOrientation)(((int)m_Orientation + 3) % 4);
            }
            else
            {
                m_Orientation = (EOrientation)(((int)m_Orientation + 1) % 4);
            }
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