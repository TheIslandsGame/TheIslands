using System;
using UnityEngine;

namespace Chunks
{
    public class ChunkManager : MonoBehaviour
    {
        public String levelName;

        public int chunks;

        private void Awake()
        {
            for (int i = 0; i < chunks; i++)
            {
                var chunkName = $"lv{levelName}_{i+1}";
                var loader = new GameObject($"ChunkLoader@[{chunkName}]").AddComponent<ChunkLoader>();
                var chunkTransform = loader.transform;
                chunkTransform.position = Vector3.zero;
                Debug.Log($"Loading chunk {chunkName} at {chunkTransform.position}");
                loader.Load(chunkName);
            }
        }
    }
}