using System;
using UnityEngine;

namespace Player
{
    public class RespawnPlayer : MonoBehaviour
    {
        public float yThreshold = -2;
        public Vector3 latestCheckpoint;
        public Collider2D collisionTrigger;
        [SerializeField] private LayerMask killPlayer;

        public void UpdateCheckpoint()
        {
            latestCheckpoint = gameObject.transform.position;
        }

        private void Start()
        {
            UpdateCheckpoint();
        }

        private void Reset()
        {
            latestCheckpoint = gameObject.transform.position;
        }

        public void DoRespawn(string cause)
        {
            gameObject.transform.position = latestCheckpoint;
            Debug.Log($"Respawning Player due to {cause}");
        }

        private void FixedUpdate()
        {
            if (collisionTrigger.transform.position.y < yThreshold)
            {
                DoRespawn("void");
            }
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionTrigger.transform.position, 0.5F, killPlayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject == gameObject) continue;
                DoRespawn("environmental hazard");
                break;
            }
        }
    }
}