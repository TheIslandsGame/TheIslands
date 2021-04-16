using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Checkpoint : MonoBehaviour
    {
        public GameObject player;

        // Start is called before the first frame update
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                player = GameObject.FindWithTag("Player");
                var pos = transform.position;
                player.transform.position.Set(pos.x, pos.y, pos.z);
                Debug.Log(pos);
                player.GetComponent<RespawnPlayer>().UpdateCheckpoint();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}