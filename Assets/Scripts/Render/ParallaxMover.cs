using System;
using UnityEngine;

namespace Render
{
    public class ParallaxMover : MonoBehaviour
    {
        public GameObject player;
        public Camera camera;
        public GameObject[] toMove;

        public Vector2 origin;
        public Vector2 scale;
        public Vector2 smoothing;
        private Vector3[] initialPositions;

        private void Awake()
        {
            for (int i = 0; i < toMove.Length; i++)
            {
                var current = toMove[i];
                initialPositions[i] = current.transform.position;
            }
        }

        private void Update()
        {
            for (int i = 0; i < toMove.Length; i++)
            {
                var current = toMove[i];
                float distanceX = initialPositions[i].z;
                
            }
        }
    }
}