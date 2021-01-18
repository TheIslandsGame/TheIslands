using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util.Chunks
{
    public class ChunkLoader : MonoBehaviour
    {
        public void Load(String chunkName)
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadSceneAsync(chunkName, LoadSceneMode.Additive);
        }
        
        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            StartCoroutine(MoveAfterLoad(scene));
        }
        
        private IEnumerator MoveAfterLoad(Scene scene)
        {
            while (!scene.isLoaded)
            {
                yield return new WaitForEndOfFrame();
            }

            var rootGameObjects = scene.GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                rootGameObject.transform.position += transform.position;
            }
            Destroy(this);
        }
    }
}