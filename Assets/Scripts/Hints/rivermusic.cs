using UnityEngine;

public class rivermusic : MonoBehaviour
{
        public AudioSource soundSource;


        void OnTriggerEnter2D(Collider2D player)
        {
            if (player.CompareTag("Player"))
            {
            soundSource.Play();
            Debug.Log("MUSIK aufgerufen");
        }
            
        }
    /*
        void OnTriggerExit2D(Collider2D player)
        {
            if (player.tag == "Player")
            {
                anzeige2.SetActive(false);

            }
            Debug.Log("TRIGGER exit");
        }
    */
 }