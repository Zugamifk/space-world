using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Lab.MapGenerator.Grammar.Test
{
    public class BoringHall : Hall
    {
        public override void Visit(MapGrammar grammar)
        {
            ((TestGrammar)grammar).Consume(this);
        }
        public override string ToString()
        {
            return "HALL[S]";
        }
    }

    public class LongHall : Hall
    {
        public override void Visit(MapGrammar grammar)
        {
            ((TestGrammar)grammar).Consume(this);
        }
        public override string ToString()
        {
            return "HALL[L]";
        }
    }

    public class Branch : Hall
    {
        public override void Visit(MapGrammar grammar)
        {
            ((TestGrammar)grammar).Consume(this);
        }
        public override string ToString()
        {
            return "HALL[B]";
        }
    }

    public class TestGrammar : MapGrammar
    {

        public override void Consume(Start token)
        {
            AddToken(new Branch());
        }

        public override void Consume(Room token)
        {
            AddToken(new LongHall());
            AddToken(new Room());
        }

        public override void Consume(Hall hall)
        {
            AddToken(new BoringHall());
            if (Random.value < 0.2)
            {
                AddToken(new Push());
                AddToken(Turn.Right());
                AddToken(new BoringHall());
                AddToken(new BoringHall());
                AddToken(new BoringHall());
                AddToken(new Room());
                AddToken(new Pop());
            }
            AddToken(Turn.Left());
            AddToken(new BoringHall());
        }

        public void Consume(BoringHall hall)
        {
            AddToken(new BoringHall());
        }

        public void Consume(LongHall hall)
        {
            AddToken(new BoringHall());
            AddToken(new LongHall());
        }

        public void Consume(Branch hall)
        {
            AddToken(new Push());
            AddToken(Turn.Left());
            AddToken(new BoringHall());
            AddToken(new LongHall());
            AddToken(Turn.Right());
            AddToken(new BoringHall());
            AddToken(new BoringHall());
            AddToken(new Branch());
            AddToken(new Pop());
            AddToken(Turn.Right());
            AddToken(new BoringHall());
            AddToken(new LongHall());
            AddToken(Turn.Left());
            AddToken(new BoringHall());
            AddToken(new BoringHall());
            AddToken(new Branch());

            AddToken(new BoringHall());
        }
    }
}