using UnityEngine;

public class Crouchhint : MonoBehaviour
{
    public GameObject anzeige4;


    void Start()
    {
        anzeige4 = GameObject.Find("/Hints/anzeige4");
        anzeige4.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            //anzeige = GameObject.FindGameObjectWithTag("UIElements");
            anzeige4.SetActive(true);

        }
        Debug.Log("TRIGGER aufgerufen");
    }
    void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            anzeige4.SetActive(false);

        }
        Debug.Log("TRIGGER exit");
    }

}
