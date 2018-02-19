using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ship
{
    public class ShipBuilder
    {
        public Ship Ship;

        public ShipBuilder(Ship ship)
        {
            Ship = ship ?? new Ship();
        }

        public void AddNode(Vector2 position)
        {
            Ship.structure.AddNode(position);
        }
    }
}