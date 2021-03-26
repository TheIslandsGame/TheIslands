using System;
using System.ComponentModel;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Util
{
    public class SpriteLoader : MonoBehaviour
    {
        public Sprite[] sprites;
        [DefaultValue(false)] private bool generatePixelCollider;
        [Range(0.0F, 1.0F), DefaultValue(0.0F)] private float alphaCutoff;
        private int gridsizeX;
        private int gridsizeY;

        public void SetDimensions(int width, int height)
        {
            gridsizeX = width;
            gridsizeY = height;
        }

        public void Regenerate()
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
            var width = 0.0F;
            var height = 0.0F;
            var maxHeight = 0.0F;
            for (var x = 0; x < gridsizeX; x++)
            {
                for (var y = 0; y < gridsizeY; y++)
                {
                    var i = y * gridsizeX + x;
                    var obj = new GameObject($"Combined Sprite #{i} ({uuid})");
                    obj.transform.SetParent(transform);
                    var self = gameObject;
                    obj.tag = self.tag;
                    obj.transform.localScale = Vector3.one;
                    var renderComponent = obj.AddComponent<SpriteRenderer>();
                    var sprite = sprites[i];
                    height = y == 0 ? 0 : sprite.texture.height / sprite.pixelsPerUnit;
                    width += sprite.texture.width / sprite.pixelsPerUnit;
                    maxHeight = Math.Max(maxHeight, sprite.texture.height / sprite.pixelsPerUnit);
                    obj.transform.localPosition = new Vector3(width, height, 0);
                    renderComponent.sprite = sprite;
                    if (generatePixelCollider)
                    {
                        obj.AddComponent<PolygonCollider2D>();
                        PixelCollider2D pxlCollider = obj.AddComponent<PixelCollider2D>();
                        pxlCollider.alphaCutoff = alphaCutoff;
                        pxlCollider.Regenerate();
                    }
                    obj.isStatic = self.isStatic;
                }
            }
            if (!gameObject.TryGetComponent<BoxCollider2D>(out var rect))
            {
                rect = gameObject.AddComponent<BoxCollider2D>();
            }
            rect.size = new Vector2(width, maxHeight);
            var rSize = rect.size;
            rect.offset = new Vector2(rSize.x / 2, -rSize.y / 2);
            rect.isTrigger = true;
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