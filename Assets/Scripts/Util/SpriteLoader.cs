using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Util
{
    public class SpriteLoader : MonoBehaviour
    {
        public Sprite[] sprites;
        private bool generatePixelCollider;
        [Range(0.0F, 1.0F)] private float alphaCutoff;

        private void Regenerate()
        {
            foreach (Transform child in transform)
            {
#if UNITY_EDITOR
                DestroyImmediate(child.gameObject, false);
#else
                Destroy(child.gameObject);
#endif
            }
            var uuid = Guid.NewGuid();
            var xPos = 0.0F;
            for (var i = 0; i < sprites.Length; i++)
            {
                var obj = new GameObject($"Combined Sprite #{i} ({uuid})");
                var self = gameObject;
                obj.isStatic = self.isStatic;
                obj.tag = self.tag;
                var renderComponent = obj.AddComponent<SpriteRenderer>();
                var sprite = sprites[i];
                obj.transform.position += new Vector3(xPos, 0, 0);
                xPos += sprite.texture.width / sprite.pixelsPerUnit;
                renderComponent.sprite = sprite;
                obj.transform.parent = transform;
                if (generatePixelCollider)
                {
                    obj.AddComponent<PolygonCollider2D>();
                    PixelCollider2D pxlCollider = obj.AddComponent<PixelCollider2D>();
                    pxlCollider.alphaCutoff = alphaCutoff;
                    pxlCollider.Regenerate();
                }
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(SpriteLoader))]
        public class SpriteLoaderEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                SpriteLoader script = (SpriteLoader) target;
                script.generatePixelCollider = GUILayout.Toggle(script.generatePixelCollider, "Generate Pixel Collider");
                if (script.generatePixelCollider)
                {
                    script.alphaCutoff = EditorGUILayout.FloatField("Alpha Cutoff:", script.alphaCutoff);
                }
                if (GUILayout.Button("Create Sprites"))
                {
                    script.Regenerate();
                }
            }
        }
#endif
    }
}