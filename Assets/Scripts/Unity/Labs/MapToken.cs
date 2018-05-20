using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lab.MapGenerator.Grammar
{
    public interface IMapToken
    {
        void Visit(MapGrammar grammar);
        void Visit(MapParser parser);
        string ToString();
    }

    public class Start : IMapToken {
        public void Visit(MapGrammar grammar)
        {
            grammar.Consume(this);
        }
        public void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "START";
        }
    }

    public class Hall : IMapToken {
        public void Visit(MapGrammar grammar)
        {
            grammar.Consume(this);
        }
        public void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "HALL";
        }
    }

    public class Room : IMapToken {
        public void Visit(MapGrammar grammar)
        {
            grammar.Consume(this);
        }
        public void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "ROOM";
        }
    }

    public class Turn : IMapToken
    {
        public readonly bool IsLeft;
        Turn(bool isleft)
        {
            IsLeft = isleft;
        }
        public static Turn Left()
        {
            return new Turn(true);
        }
        public static Turn Right()
        {
            return new Turn(false);
        }
        public void Visit(MapGrammar grammar)
        {
            grammar.Consume(this);
        }
        public void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return (IsLeft ? "LEFT":"RIGHT")+" TURN";
        }
    }

    public class Push : IMapToken
    {
        public void Visit(MapGrammar grammar)
        {
            grammar.Consume(this);
        }
        public void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "<";
        }
    }

    public class Pop : IMapToken
    {
        public void Visit(MapGrammar grammar)
        {
            grammar.Consume(this);
        }
        public void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return ">";
        }
    }
}