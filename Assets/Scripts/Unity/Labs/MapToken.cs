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
    }

}