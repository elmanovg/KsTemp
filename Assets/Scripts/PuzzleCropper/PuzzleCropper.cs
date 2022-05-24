using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class PuzzleCropper : MonoBehaviour
{
    [SerializeField] private Sprite source;
    [SerializeField] private Image[] imgs;
    [SerializeField] private int splitHorizontal;
    [SerializeField] private int splitVertical;

    [SerializeField] private GameObject gridNode;

    [SerializeField] private int padding;
    [SerializeField] private int indent;
    [SerializeField] private Vector2 pos;
    private Rect fragmentRect;
    private Vector2 spritePivot;
    private Texture texture;
    private void Start()
    {
        texture = source.texture;
    }

    public void UpdateAllImages()
    {
        imgs = gridNode.GetComponentsInChildren<Image>();

        fragmentRect = new Rect(0, 0, texture.width / splitHorizontal, texture.height / splitVertical - padding);
        spritePivot = new Vector2(0.5f, 0.5f);
        for (int i = 0; i < imgs.Length; i++)
        {
            while (imgs[i].transform.childCount > 0)
            {
                GameObject.DestroyImmediate(imgs[i].transform.GetChild(0).gameObject);
            }
            piece(i % splitHorizontal, i / splitHorizontal, imgs[i]);
            addColliders(i % splitHorizontal, i / splitHorizontal, imgs[i]);
        }
    }
    private void piece(int x, int y, Image img)
    {
        var currtentRect = new Rect(fragmentRect);
        currtentRect.x += (fragmentRect.width + indent) * x;
        currtentRect.y += fragmentRect.height * y;
        if (y > 1)
        {
            currtentRect.y += (padding + indent) * (y - 1);
        }
        if (x == splitHorizontal - 1)
        {
            currtentRect.width -= padding + indent;
        }
        if (y == 0)
        {
            currtentRect.height -= indent;
        }
        else
        {
            currtentRect.height += padding;
        }
        var sprite = Sprite.Create(source.texture, currtentRect, spritePivot);
        var gridTransform = gridNode.GetComponent<RectTransform>();
        gridTransform.sizeDelta = new Vector2(gridTransform.rect.width, gridTransform.rect.width / (texture.width * 1f / texture.height));
        var ratio = texture.width * 1f / gridTransform.rect.width;

        var grid = img.GetComponent<RectTransform>();
        grid.sizeDelta = new Vector2(gridTransform.rect.width / splitHorizontal - (x == splitHorizontal - 1 ? padding / ratio + indent / ratio : 0),
                                    gridTransform.rect.height / splitVertical - (y == 0 ? padding / ratio + indent / ratio : 0));

        img.sprite = sprite;
    }

    private void addColliders(int x, int y, Image img)
    {
        var gridTransform = gridNode.GetComponent<RectTransform>();
        gridTransform.sizeDelta = new Vector2(gridTransform.rect.width, gridTransform.rect.width / (texture.width * 1f / texture.height));
        var ratio = texture.width * 1f / gridTransform.rect.width;
        var segmentProcent = 0.5f;
        if (x < splitHorizontal - 1)
        {
            var node = new GameObject("right");
            node.transform.parent = img.transform;
            node.transform.localPosition = Vector3.zero;
            var collider = node.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f * padding / ratio, fragmentRect.height * segmentProcent / ratio);
            collider.offset = new Vector2(1f * fragmentRect.width / 2 / ratio - 1f * padding / ratio / 2, 0);
        }

        if (x > 0)
        {
            var node = new GameObject("left");
            node.transform.parent = img.transform;
            node.transform.localPosition = Vector3.zero;
            var collider = node.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f * padding / ratio, fragmentRect.height * segmentProcent / ratio);
            collider.offset = new Vector2(-(1f * fragmentRect.width / 2 / ratio - 1f * padding / ratio / ((x == splitHorizontal - 1) ? 1 : 2) - ((x == splitHorizontal - 1) ? indent / ratio : 0)), 0);
        }

        if (y < splitVertical - 1)
        {
            var node = new GameObject("top");
            node.transform.parent = img.transform;
            node.transform.localPosition = Vector3.zero;
            var collider = node.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(fragmentRect.width * segmentProcent / ratio, 1f * padding / ratio);
            collider.offset = new Vector2(0, 1f * fragmentRect.height / 2 / ratio - 1f * padding / ratio / (y == 0 ? 1 : 2) + ((y == 0) ? 0 : indent / ratio));
        }
        if (y > 0)
        {
            var node = new GameObject("bottom");
            node.transform.parent = img.transform;
            node.transform.localPosition = Vector3.zero;
            var collider = node.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(fragmentRect.width * segmentProcent / ratio, 1f * padding / ratio);
            collider.offset = new Vector2(0, -(1f * fragmentRect.height / 2 / ratio - 1f * padding / ratio / (y == 0 ? 1 : 2) + ((y == 0) ? 0 : indent / ratio)));
        }
    }
}
