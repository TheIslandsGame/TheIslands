using UnityEngine;

public class Movehint : MonoBehaviour
{
    public GameObject anzeige3;


    void Start()
    {
        anzeige3 = GameObject.Find("/Hints/anzeige3");
        anzeige3.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            //anzeige = GameObject.FindGameObjectWithTag("UIElements");
            anzeige3.SetActive(true);

        }
        Debug.Log("TRIGGER aufgerufen");
    }
    void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            anzeige3.SetActive(false);

        }
        Debug.Log("TRIGGER exit");
    }

}
