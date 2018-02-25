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

        public Structure.Node AddNode(Vector2 position)
        {
            return Ship.structure.AddNode(position);
        }
        public Structure.FrameSection ConnectNodes(Structure.Node a, Structure.Node b)
        {
            return Ship.structure.ConnectNodes(a, b);
        }
        public void RotateNode(Structure.Node node, Structure.FrameSection frame, float angle)
        {
            Ship.structure.RotateEdgeNode(node, frame, angle);
        }
    }
}