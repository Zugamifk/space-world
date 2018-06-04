using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game.Chemistry;

namespace Unity.Chemistry.Editor
{

    public class ChemicalsEditor
    {
        int selectedChemical;
        Element[] elements;
        string[] chemicalNames;
        Element current;
        ElementCompound currentCompound;
        public ChemicalsEditor()
        {
            int len = Element.ElementsCount;
            chemicalNames = new string[len];
            elements = new Element[len];
            for(int i=0;i<len;i++)
            {
                var e = Element.GetIndexed(i);
                chemicalNames[i] = e.Name;
                elements[i] = e;
            }
            selectedChemical = 0;
            currentCompound = new ElementCompound();
        }

        public void Draw()
        {
            int chemw = (int)EditorGUIUtility.currentViewWidth / 100;
            selectedChemical = GUILayout.SelectionGrid(selectedChemical, chemicalNames, chemw);
            GUI.enabled = false;
            GUILayout.TextField(currentCompound.Name);
            GUI.enabled = true;
            if (GUILayout.Button("Add"))
            {
                currentCompound = currentCompound.Add(elements[selectedChemical], 1);
            }
        }

    }
}