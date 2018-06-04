using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grammar = Game.Lab.MapGenerator.Grammar;

namespace Game.Lab.MapGenerator.Dungeon
{
    public class Room : Grammar.Room
    {
        public readonly string Name;

        public override void Visit(MapGrammar grammar)
        {
            grammar.Consume(this);
        }
    }
    public class DungeonMap : MapGrammar
    {
        
    }
}