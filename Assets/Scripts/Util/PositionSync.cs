using UnityEngine;

namespace Util
{
    // Utility script to glue multiple objects to a single anchor object, maintaining a constant offset to the anchor
    public class PositionSync : MonoBehaviour
    {
        public GameObject anchor;
        public GameObject[] attachedObjects;
        private Vector3[] _offsets;

        private void Awake()
        {
            _offsets = new Vector3[attachedObjects.Length];
        }

        private void OnEnable()
        {
            for (var i = 0; i < attachedObjects.Length; i++)
            {
                Vector3 offset = attachedObjects[i].transform.position - anchor.transform.position;
                _offsets[i] = offset;
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (var i = 0; i < attachedObjects.Length; i++)
            {
                attachedObjects[i].transform.position = anchor.transform.position + _offsets[i];
            }
        }
    }
}
