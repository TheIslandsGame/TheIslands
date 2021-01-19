using System;
using UnityEditor;
using UnityEngine;

namespace Util
{
    public class SpriteMerge : MonoBehaviour
    {
        public Sprite[] sprites;

        private String uuid = "000";

        private void Awake()
        {
            // FIXME cannot export with this?
            //uuid = GUID.Generate(); 
            float xPos = 0.0F;
            for (int i = 0; i < sprites.Length; i++)
            {
                var obj = new GameObject($"Combined Sprite #{i} ({uuid})", typeof(SpriteRenderer));
                var renderComponent = obj.GetComponent<SpriteRenderer>();
                renderComponent.sprite = sprites[i];
                obj.transform.position += new Vector3(xPos, 0, 0);
                obj.AddComponent<PolygonCollider2D>();
                xPos += renderComponent.sprite.bounds.size.x;
            }
        }
    }
}