using System;
using UnityEngine;

namespace Util.Chunks
{
    public class ChunkManager : MonoBehaviour
    {
        public String levelName;

        public int chunks;

        private int chunkXSize = 300;

        private void Awake()
        {
            for (int i = 1; i <= chunks; i++)
            {
                var chunkName = $"lv{levelName}_{i}";
                var loader = new GameObject($"ChunkLoader@[{chunkName}]").AddComponent<ChunkLoader>();
                var chunkTransform = loader.transform;
                chunkTransform.position = new Vector3(i * chunkXSize, 0F, 0F);
                Debug.Log($"Loading chunk {chunkName} at {chunkTransform.position}");
                loader.Load(chunkName);
            }
        }
    }
}