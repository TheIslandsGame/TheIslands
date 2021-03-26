using UnityEngine;

namespace Menu
{
    public class Hintriver : MonoBehaviour
    {
        public GameObject anzeige2;


        void Start()
        {
            anzeige2 = GameObject.Find("/Hints/anzeige2");
            anzeige2.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D player)
        {
            if (player.CompareTag("Player"))
            {
                //anzeige = GameObject.FindGameObjectWithTag("UIElements");
                anzeige2.SetActive(true);

            }
            Debug.Log("TRIGGER aufgerufen");
        }
        void OnTriggerExit2D(Collider2D player)
        {
            if (player.CompareTag("Player"))
            {
                anzeige2.SetActive(false);

            }
            Debug.Log("TRIGGER exit");
        }

    }
}