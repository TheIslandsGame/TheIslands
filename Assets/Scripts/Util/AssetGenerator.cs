using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

#if UNITY_EDITOR
namespace Util
{
    public class AssetGenerator : MonoBehaviour
    {
        public string levelName;

        public string GetAssetPath()
        {
            return $"Assets/Textures/Backgrounds/{levelName}";
        }
    }

    [CustomEditor(typeof(AssetGenerator))]
    public class AssetGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (AssetGenerator) target;
            if (GUILayout.Button("LOAD ASSETS"))
            {
                foreach (Transform child in script.transform)
                {
                    DestroyImmediate(child.gameObject, false);
                }
                var rootPath = script.GetAssetPath();
                var guids = AssetDatabase.FindAssets("t:texture2D", new[] {rootPath});
                Debug.Log($"Found {guids.Length} files");

                var lookup = new SortedDictionary<string, List<Sprite>>();
                
                foreach (string guid in guids)
                {
                    var fullPath = AssetDatabase.GUIDToAssetPath(guid);
                    var path = fullPath.Substring(rootPath.Length + 1);
                    var fileName = path.Substring(path.LastIndexOf('/') + 1);
                    var folderName = path.Substring(0, path.LastIndexOf('/'));
                    fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
                    Debug.Log($"Loading {fileName} in {folderName}");
                    Texture2D texRef = AssetDatabase.LoadAssetAtPath<Texture2D>(fullPath);
                    var sprite = Sprite.Create(texRef, new Rect(0, 0, texRef.width, texRef.height), new Vector2(0.5F, 0.5F), 100F, 0, SpriteMeshType.FullRect, Vector4.zero, false);
                    
                    lookup.ComputeIfAbsent(folderName, k => new List<Sprite>()).Add(sprite);
                }
                
                Debug.Log("Parsed Asset Database, creating GameObjects..");

                foreach (var entry in lookup)
                {
                    var obj = new GameObject(entry.Key);
                    obj.transform.position = Vector3.zero;
                    obj.transform.localScale = Vector3.one;
                    obj.transform.parent = script.transform;
                    var loader = obj.AddComponent<SpriteLoader>();
                    loader.sprites = entry.Value.ToArray();
                    loader.Regenerate();
                }
            }
        }
    }
}
#endif