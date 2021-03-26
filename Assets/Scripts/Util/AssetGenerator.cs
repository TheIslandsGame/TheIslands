using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
using UnityEditor;
#endif

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
    
#if UNITY_EDITOR
    [CustomEditor(typeof(AssetGenerator))]
    public class AssetGeneratorEditor : Editor
    {

        IEnumerator SortAssets(string[] guids, string rootPath, Transform transform)
        {
            var lookup = new SortedDictionary<string, List<Sprite>>();
            var dimensions = new Dictionary<string, string>();
            foreach (string guid in guids)
            {
                var fullPath = AssetDatabase.GUIDToAssetPath(guid);
                var path = fullPath.Substring(rootPath.Length + 1);
                var fileName = path.Substring(path.LastIndexOf('/') + 1);
                var folderName = path.Substring(0, path.LastIndexOf('/'));
                fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
                Debug.Log($"Loading {fileName} in {folderName}");
                Texture2D texRef = AssetDatabase.LoadAssetAtPath<Texture2D>(fullPath);
                var sprite = Sprite.Create(texRef, new Rect(0, 0, texRef.width, texRef.height), new Vector2(1F, 1F), 100F, 0, SpriteMeshType.FullRect, Vector4.zero, false);
                    
                lookup.ComputeIfAbsent(folderName, k => new List<Sprite>()).Add(sprite);
                var sizes = Array.ConvertAll(fileName.Split('_'), int.Parse);
                    
                // TODO optimize, I've been lazy
                var existingSizes = Array.ConvertAll(dimensions.ComputeIfAbsent(folderName, k => "0_0").Split('_'), int.Parse);
                var maxX = Math.Max(sizes[0], existingSizes[0]);
                var maxY = Math.Max(sizes[1], existingSizes[1]);
                dimensions[folderName] = $"{maxX}_{maxY}";
                yield return null;
            }
            
            Debug.Log("Parsed Asset Database, creating GameObjects..");
            foreach (var entry in lookup)
            {
                var obj = new GameObject(entry.Key);
                obj.transform.SetParent(transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                var loader = obj.AddComponent<SpriteLoader>();
                loader.sprites = entry.Value.ToArray();
                var sizes = Array.ConvertAll(dimensions[entry.Key].Split('_'), int.Parse);
                loader.SetDimensions(sizes[1] + 1, sizes[0] + 1);
                loader.Regenerate();
                yield return null;
            }
            Debug.Log("Finished generating GameObjects");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (AssetGenerator) target;
            if (GUILayout.Button("LOAD ASSETS"))
            {
                //foreach (Transform child in script.transform)
                //{
                //    DestroyImmediate(child.gameObject, false);
                //}
                var rootPath = script.GetAssetPath();
                var guids = AssetDatabase.FindAssets("t:texture2D", new[] {rootPath});
                Debug.Log($"Found {guids.Length} files");

                EditorCoroutineUtility.StartCoroutine(SortAssets(guids, rootPath, script.transform), this);
            }
        }
    }
#endif
}
