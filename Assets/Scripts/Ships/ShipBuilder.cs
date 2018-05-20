using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ship.Builder
{
    public class ShipBuilder
    {
        public Ship Ship;
        ShipChamberProcessor chamberProcessor;

        public ShipBuilder(Ship ship)
        {
            Ship = ship ?? new Ship();
            chamberProcessor = new ShipChamberProcessor(Ship.structure);
        }

        public Structure.Node AddNode(Vector2 position)
        {
            return Ship.structure.AddNode(position);
        }
        public Structure.FrameSection ConnectNodes(Structure.Node a, Structure.Node b)
        {
            var result = Ship.structure.ConnectNodes(a, b);
            chamberProcessor.UpdateChambers();
            return result; 
        }

        public void MoveNode(Structure.Node node, Vector2 position)
        {
            node.Position = position;
        }

        public void RotateNode(Structure.Node node, Structure.FrameSection frame, float angle)
        {
            Ship.structure.RotateEdgeNode(node, frame, angle);
        }

        void UpdateChambers()
        {

        }
    }
}