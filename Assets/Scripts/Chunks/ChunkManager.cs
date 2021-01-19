using System;
using UnityEngine;

namespace Chunks
{
    public class ChunkManager : MonoBehaviour
    {
        public String levelName;

        public int chunks;

        private int chunkXSize = 300;

        private void Awake()
        {
            for (int i = 0; i < chunks; i++)
            {
                var chunkName = $"lv{levelName}_{i+1}";
                var loader = new GameObject($"ChunkLoader@[{chunkName}]").AddComponent<ChunkLoader>();
                var chunkTransform = loader.transform;
                chunkTransform.position = new Vector3(i * chunkXSize, 0F, 0F);
                Debug.Log($"Loading chunk {chunkName} at {chunkTransform.position}");
                loader.Load(chunkName);
            }
        }
    }
}