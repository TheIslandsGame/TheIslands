using System;
using UnityEngine;

namespace Player
{
    public class SpawnPoint : MonoBehaviour
    {

        private GameObject player;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            var pos = transform.position;
            player.transform.position.Set(pos.x, pos.y, pos.z);
            Debug.Log(pos);
            player.GetComponent<RespawnPlayer>().UpdateCheckpoint();
        }
    }
}