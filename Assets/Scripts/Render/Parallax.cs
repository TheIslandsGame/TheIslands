using System;
using UnityEngine;

// For usage apply the script directly to the element you wish to apply parallaxing
// Based on Brackeys 2D parallaxing script http://brackeys.com/
namespace Render
{
    public class Parallax : MonoBehaviour
    {
        Transform cam; // Camera reference (of its transform)
        Vector3 previousCamPos;

        public float distanceX = 0f; // Distance of the item (z-index based) 
        public float distanceY = 0f;

        public float smoothingX = 1f; // Smoothing factor of parrallax effect
        public float smoothingY = 1f;

        private void Start()
        {
            cam = GameObject.FindWithTag("MainCamera").transform;
        }

        void Update () {
            if (distanceX != 0f) {
                float parallaxX = (previousCamPos.x - cam.position.x) * distanceX;
                var transform1 = transform;
                var position = transform1.position;
                Vector3 backgroundTargetPosX = new Vector3(position.x + parallaxX, 
                    position.y, 
                    position.z);
			
                // Lerp to fade between positions
                transform.position = Vector3.Lerp(position, backgroundTargetPosX, smoothingX * Time.deltaTime);
            }

            if (distanceY != 0f) {
                float parallaxY = (previousCamPos.y - cam.position.y) * distanceY;
                var position = transform.position;
                Vector3 backgroundTargetPosY = new Vector3(position.x, 
                    position.y + parallaxY, 
                    position.z);
			
                position = Vector3.Lerp(position, backgroundTargetPosY, smoothingY * Time.deltaTime);
                transform.position = position;
            }

            previousCamPos = cam.position;	
        }
    }
}