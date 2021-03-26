using System;
using UnityEngine;

namespace Player
{
    public class RespawnPlayer : MonoBehaviour
    {
        public float yThreshold = -2;
        public Vector3 latestCheckpoint;
        public Collider2D collider;
        [SerializeField] private LayerMask water;
        [SerializeField] private LayerMask kill;

        private LayerMask all;

        public void UpdateCheckpoint()
        {
            latestCheckpoint = gameObject.transform.position;
        }

        private void Awake()
        {
            all = new LayerMask {value = water.value | kill.value};
            
        }

        private void Start()
        {
            UpdateCheckpoint();
        }

        private void Reset()
        {
            latestCheckpoint = this.transform.position;
        }

        public void DoRespawn(string cause)
        {
            gameObject.transform.position = latestCheckpoint;
            Debug.Log($"Respawning Player due to {cause}");
        }

        private void FixedUpdate()
        {
            if (gameObject.transform.position.y < yThreshold)
            {
                DoRespawn("void");
            }
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(collider.gameObject.transform.position, 0.5F, all);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject == gameObject) continue;
                DoRespawn("environmental hazard");
                break;
            }
        }
    }
}