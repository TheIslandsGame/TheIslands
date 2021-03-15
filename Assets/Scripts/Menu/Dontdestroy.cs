using UnityEngine;

namespace Menu
{
    public class Dontdestroy : MonoBehaviour
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
}