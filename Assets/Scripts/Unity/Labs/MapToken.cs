using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lab.MapGenerator.Token
{
    public abstract class MapToken
    {
        public virtual void Visit(MapGrammar grammer)
        {
            grammer.Consume(this);
        }
        public virtual void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "TOKEN[INVALID]";
        }
    }

    public class Start : MapToken {
        public override void Visit(MapGrammar grammer)
        {
            grammer.Consume(this);
        }
        public override void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "START";
        }
    }

    public class Hall : MapToken {
        public override void Visit(MapGrammar grammer)
        {
            grammer.Consume(this);
        }
        public override void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "HALL";
        }
    }

    public class Room : MapToken {
        public override void Visit(MapGrammar grammer)
        {
            grammer.Consume(this);
        }
        public override void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return "ROOM";
        }
    }

    public class Turn : MapToken
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
        public override void Visit(MapGrammar grammer)
        {
            grammer.Consume(this);
        }
        public override void Visit(MapParser parser)
        {
            parser.Consume(this);
        }
        public override string ToString()
        {
            return (IsLeft ? "LEFT":"RIGHT")+" TURN";
        }
    }

}