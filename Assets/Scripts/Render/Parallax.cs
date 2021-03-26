using System;
using UnityEngine;

namespace Render
{
    public class Parallax : MonoBehaviour
    {
        // one square = 20.48 units
        private const float MainWidth = 245.76F; // width of the whole "main" layer, 12 squares
        public float width; // the width of this element, in units

        public float progress = 1.0F;
        public float toAdd = 0.0F;
        public float renderScale = 1.0F;

        private GameObject playerCamera;
        private Vector3 startPos;
        private Vector3 origin;

        private void Start()
        {
            playerCamera = GameObject.FindWithTag("MainCamera");
            startPos = playerCamera.transform.position;
            origin = transform.position;
        }

        private void Update()
        {
            renderScale = MainWidth / width; // 1 / scale
            var xDiff = startPos.x - playerCamera.transform.position.x;
            progress = Mathf.Clamp(xDiff / MainWidth, 0.0F, 1.0F); // left = 0.0, right = 1.0

            if (renderScale < 1.0F)
            {
                renderScale = 1.0F / renderScale;
            }

            toAdd = -progress * renderScale;

            transform.position = new Vector3(origin.x + toAdd * width, origin.y, origin.z);
        }
    }
}