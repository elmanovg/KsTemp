using System;
using System.Collections.Generic;
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

    [SerializeField] private VerticalLayoutGroup containter;
    [SerializeField] private GameObject puzzlePrefab;

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
        var containerRect = containter.GetComponent<RectTransform>();
        var gridRect = gridNode.GetComponent<RectTransform>();
        var sourceRatio = gridRect.sizeDelta.y / gridRect.sizeDelta.x;
        var smallBigRatio =
            targetTransforms[0].sizeDelta.x / targetTransforms[splits[difficulty].Item1 - 1].sizeDelta.x;
        var puzzleContainerSize = containerRect.sizeDelta.y /
                                  (splits[difficulty].Item1 - (difficulty == Difficulty.easy ? 0 : 1));
        containerRect.sizeDelta =
            new Vector2(puzzleContainerSize * (splits[difficulty].Item2 + (difficulty == Difficulty.easy ? 0 : 1)),
                containerRect.sizeDelta.y);

        while (containter.transform.childCount > 0)
        {
            DestroyImmediate(containter.transform.GetChild(0).gameObject);
        }

        var counter = 0;
        for (var i = 0; i < splits[difficulty].Item1; i++)
        {
            var imagesLine = Instantiate(line, containter.transform, false);
            for (var j = 0; j < splits[difficulty].Item2 + (difficulty == Difficulty.easy ? 0 : 1); j++)
            {
                var imageContainer = new GameObject("ImageContainer", typeof(RectTransform));
                imageContainer.transform.SetParent(imagesLine.transform);
                imageContainer.transform.localScale = new Vector3(1, 1, 1);
                imageContainer.transform.position = imageContainer.transform.parent.TransformPoint(Vector3.zero);
                imageContainer.transform.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(puzzleContainerSize, puzzleContainerSize);
                var image = Instantiate(puzzlePrefab, imageContainer.transform, false);
                image.transform.position = imageContainer.transform.TransformPoint(Vector3.zero);
                var edgesRatio = targetTransforms[counter].sizeDelta.x * 1f / targetTransforms[counter].sizeDelta.y;
                var finalSize = puzzleContainerSize /
                                (counter % splits[difficulty].Item1 == splits[difficulty].Item1 - 1
                                    ? smallBigRatio
                                    : 1);
                image.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(finalSize, finalSize / edgesRatio);
                var puzzleController = image.GetComponent<SinglePuzzleController>();

                var targetTransform = targetTransforms[counter].gameObject.GetComponent<Image>();
                puzzleController.init(counter, this, Vector3.zero, targetTransform.sprite,
                    imgSize.x * 1f / puzzleContainerSize);
                counter++;
                if (counter >= splits[difficulty].Item2 * splits[difficulty].Item1)
                {
                    break;
                }
            }

            if (counter >= splits[difficulty].Item2 * splits[difficulty].Item1)
            {
                break;
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(containter.GetComponent<RectTransform>());
    }

    public Vector3 GetTransformPosition(int i)
    {
        var transform = targetTransforms[i];
        var result = transform.TransformPoint(Vector3.zero);
        result.z = 0;
        return result;
    }
}