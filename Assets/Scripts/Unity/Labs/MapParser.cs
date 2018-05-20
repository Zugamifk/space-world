using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Lab.MapGenerator.Grammar;

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

        class State
        {
            public readonly EOrientation Orientation;
            public readonly Vector2Int Position;

            public State(EOrientation orientation, Vector2Int position)
            {
                Orientation = orientation;
                Position = position;
            }
        }

        EOrientation m_Orientation = EOrientation.North;
        Vector2Int m_Position = Vector2Int.zero;
        MapModel m_CurrentModel;
        Stack<State> m_States = new Stack<State>();

        public void Initialize(MapModel model)
        {
            m_Position = model.Origin;
            m_Orientation = EOrientation.North;
            m_CurrentModel = model;
        }

        public void Parse(IEnumerable<IMapToken> tokens)
        {
            foreach (var t in tokens)
            {
                t.Visit(this);
            }
        }

        public void Consume(IMapToken token)
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
                    y0 = 1 - h;
                    goto case EOrientation.North;
                case EOrientation.West:
                    x0 = 1 - w;
                    goto case EOrientation.East;
            }
            for (int xi = x0; xi < x0 + w; xi++)
            {
                for (int yi = y0; yi < y0 + h; yi++)
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

        public void Consume(Push push)
        {
            var state = new State(m_Orientation, m_Position);
            m_States.Push(state);
        }

        public void Consume(Pop pop)
        {
            var state = m_States.Pop();
            m_Orientation = state.Orientation;
            m_Position = state.Position;
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