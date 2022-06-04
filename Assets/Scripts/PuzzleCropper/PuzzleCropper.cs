using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PuzzleCropper : MonoBehaviour
{
    [SerializeField] private GameObject gridNode;
    [SerializeField] private Texture2D source;

    [SerializeField] private float scale;
    [SerializeField] private String path;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject image;
    [SerializeField] private float opacity;
    [SerializeField] private Difficulty difficulty;

    [SerializeField] private GridLayoutGroup containter;

    private List<RectTransform> targetTransforms = new();

    enum Difficulty
    {
        easy,
        medium,
        hard
    }
    
    private Dictionary<Difficulty, float> paddings = new()
    {
        { Difficulty.easy, 22 },
        { Difficulty.medium, 18 },
        { Difficulty.hard, 16 }
    };

    private Dictionary<Difficulty, Tuple<int, int>> splits = new()
    {
        { Difficulty.easy, new Tuple<int, int>(5, 4) },
        { Difficulty.medium, new Tuple<int, int>(8, 5) },
        { Difficulty.hard, new Tuple<int, int>(10, 6) }
    };

    private Dictionary<Difficulty, int> spacings = new()
    {
        { Difficulty.easy, 74 },
        { Difficulty.medium, 58 },
        { Difficulty.hard, 48 }
    };

    private void Start()
    {
        UpdateAllImages();
    }

    public void UpdateAllImages()
    {
        var splitHorizontal = splits[difficulty].Item1;
        var splitVertical = splits[difficulty].Item2;
        var padding = paddings[difficulty];
        while (gridNode.transform.childCount != 0)
        {
            DestroyImmediate(gridNode.transform.GetChild(0).gameObject);
        }

        targetTransforms.Clear();
        var imgs = new List<Image>();
        for (int i = 0; i < splitVertical; i++)
        {
            var newLine = Instantiate(line, gridNode.transform, false);
            newLine.GetComponent<HorizontalLayoutGroup>().spacing = -spacings[difficulty] * scale;
            for (int j = 0; j < splitHorizontal; j++)
            {
                var newImage = Instantiate(image, newLine.transform, false);
                imgs.Add(newImage.GetComponent<Image>());
                targetTransforms.Add(newImage.GetComponent<RectTransform>());
            }
        }

        gridNode.GetComponent<RectTransform>().sizeDelta = new Vector2(source.width * scale, source.height * scale);
        for (int i = 0; i < imgs.Count; i++)
        {
            var texture =
                Resources.Load("Puzzle/" + path + "_" + splitHorizontal + "x" + splitVertical + "/" + (i + 1)) as
                    Texture2D;
            if (texture == null)
            {
                Debug.Log("Wrong path: " + path);
                return;
            }

            var yPadding = i / splitHorizontal == splitVertical - 1 ? 0 : padding;
            var xPadding = i % splitHorizontal == splitHorizontal - 1 ? 0 : padding;
            var sprite = Sprite.Create(texture,
                new Rect(0, yPadding, texture.width - xPadding, texture.height - yPadding),
                new Vector2(0.5f, 0.5f));
            imgs[i].sprite = sprite;
            imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, opacity);
            imgs[i].GetComponent<RectTransform>().sizeDelta = new Vector2((texture.width - xPadding) * scale,
                (texture.height - yPadding) * scale);
        }

        gridNode.GetComponent<VerticalLayoutGroup>().spacing = -spacings[difficulty] * scale;
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridNode.GetComponent<RectTransform>());
    }

    public void Test()
    {
        var imgSize = targetTransforms[0].sizeDelta;
        var ratio = (containter.GetComponent<RectTransform>()).sizeDelta.y /
                    (gridNode.GetComponent<RectTransform>()).sizeDelta.x;
        var puzzleContainerSize = Math.Max(imgSize.x * ratio, imgSize.y * ratio);
        var puzzlePadding = puzzleContainerSize * 0.1f;
        containter.spacing = new Vector2(puzzlePadding, puzzlePadding);
        containter.constraintCount = splits[difficulty].Item2;
    }

    public Vector3 GetTransformPosition(int i)
    {
        var transform = targetTransforms[i];
        var result = transform.TransformPoint(Vector3.zero);
        result.z = 0;
        return result;
    }
}