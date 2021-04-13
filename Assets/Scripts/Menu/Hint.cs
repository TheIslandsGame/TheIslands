using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
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

*/

namespace Menu
{
    public class Hint : MonoBehaviour
    {
        public GameObject Hints;
        public GameObject anzeige1;
        public GameObject anzeige2;


        void Start()
        {
            anzeige1 = GameObject.Find("/Hints/anzeige1");
            //anzeige2 = GameObject.Find("/Hints/anzeige2");
            anzeige1.SetActive(false);
            //anzeige2.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D player)
        {
            if (player.CompareTag("Player"))
            {
                //anzeige = GameObject.FindGameObjectWithTag("UIElements");
                anzeige1.SetActive(true);

            }
        }

    }
}