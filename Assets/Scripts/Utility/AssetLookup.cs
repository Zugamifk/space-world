using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace Unity
{
    public class AssetLookup : ScriptableObject
    {

        [SerializeField]
        Lab.Tile m_LabTile;

        Dictionary<System.Type, Object> m_Lookup;
        static AssetLookup s_Instance;

        private void OnEnable()
        {
            Log.Register(this, "FD7B2D");

            s_Instance = this;

            m_Lookup = new Dictionary<System.Type, Object>();

            Add(m_LabTile);
        }

        void Add<T>(T obj) where T : Object
        {
            var type = typeof(T);
            m_Lookup.Add(type, obj);
        }

        public static T Get<T>() where T : Object
        {
            var type = typeof(T);
            Object result;
            if (s_Instance.m_Lookup.TryGetValue(type, out result))
            {
                return (T)GameObject.Instantiate(result);
            }
            else
            {
                Log.Print(s_Instance, "No assets of type {0} in lookup!", type);
                return default(T);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        [MenuItem("Assets/Create/AssetLookup")]
        public static void CreateAsset()
        {
            AssetLookup asset = ScriptableObject.CreateInstance<AssetLookup>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New AssetLookup.asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
#endif
    }
}
