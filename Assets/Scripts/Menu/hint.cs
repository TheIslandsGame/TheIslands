using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class hint : MonoBehaviour
    {
        public GameObject anzeige;


        void Start()
        {
            anzeige = GameObject.FindWithTag("UIElements");
            anzeige.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D player)
        {
            if (player.tag == "Player")
            {
                //anzeige = GameObject.FindGameObjectWithTag("UIElements");
                anzeige.SetActive(true);

            }
            Debug.Log("TRIGGER aufgerufen");
        }

    }
}