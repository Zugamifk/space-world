using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Unity.Chemistry.Editor
{
    public class ChemistryEditor : UnityEditor.EditorWindow
    {
        readonly string[] k_Tabs = new string[]
        {
            "Chemicals",
            "Food"
        };
        int currentOption = 0;
        const int k_MaxOptions = 2;
        static ChemistryEditor window;
        ChemicalsEditor chemicalsEditor;

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Chemistry")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            window = (ChemistryEditor)EditorWindow.GetWindow(typeof(ChemistryEditor));
            window.chemicalsEditor = new ChemicalsEditor();
            window.Show();
        }

        void OnGUI()
        {
            currentOption = GUILayout.SelectionGrid(currentOption, k_Tabs, 2);
            switch (currentOption)
            {
                // chemicals
                case 0:
                    if (chemicalsEditor == null)
                    {
                        chemicalsEditor = new ChemicalsEditor();
                    }
                    chemicalsEditor.Draw();
                    break;
                case 1: break;
                default:
                    throw new System.ArgumentException("Invalid option! " + currentOption + " > " + k_MaxOptions);
            }
        }
    }
}