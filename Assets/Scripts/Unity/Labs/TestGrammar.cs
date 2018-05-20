using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lab.MapGenerator.Grammar
{
    public class TestGrammar : MapGrammar
    {

        public override void Consume(Start token)
        {
            AddToken(new Room());
        }

        public override void Consume(Room token)
        {
            AddToken(new Hall());
            AddToken(new Room());
        }

        public override void Consume(Hall hall)
        {
            AddToken(new Hall());
            if(Random.value < 0.2)
            {
                AddToken(new Push());
                AddToken(Turn.Right());
                AddToken(new Hall());
                AddToken(new Room());
                AddToken(new Pop());
            }
            AddToken(Turn.Left());
            AddToken(new Hall());
        }
    }
}