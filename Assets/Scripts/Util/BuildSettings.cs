using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    //[RequireComponent(typeof(String[]))]
    //[ExecuteInEditMode]
    public class BuildSettings : MonoBehaviour
    {
        public Scene[] scenes;

        private void Start()
        {
            #if UNITY_EDITOR
            foreach (var scene in scenes)
            {
                AddScene(scene.path);
            }
            #endif
        }

        public void AddScene(String path)
        {
            var original = EditorBuildSettings.scenes;
            var newSettings = new EditorBuildSettingsScene[original.Length + 1];
            Array.Copy(original, newSettings, original.Length);
            newSettings[newSettings.Length - 1] = new EditorBuildSettingsScene(path, true);
            EditorBuildSettings.scenes = newSettings;
        }
    }
}