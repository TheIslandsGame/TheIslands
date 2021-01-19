using UnityEngine;

namespace Player
{
    public class RespawnPlayer : MonoBehaviour
    {
        public float yThreshold = -2;
        public Vector3 latestCheckpoint;

        private void UpdateCheckpoint()
        {
            latestCheckpoint = gameObject.transform.position;
        }

        private void Start()
        {
            UpdateCheckpoint();
        }

        private void Reset()
        {
            latestCheckpoint = this.transform.position;
        }

        private void FixedUpdate()
        {
            if (gameObject.transform.position.y < yThreshold)
            {
                gameObject.transform.position = latestCheckpoint;
            }
        }
    }
}