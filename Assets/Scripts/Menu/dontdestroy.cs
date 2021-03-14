using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroy : MonoBehaviour
{

    static bool created = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true; // which runs "else" statement exactly the next frame )))
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}