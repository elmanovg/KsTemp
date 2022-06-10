using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SinglePuzzleController : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private PuzzleCropper _puzzleCropper;
    [SerializeField] private Vector3 _position;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private float _scale;

    private void Start()
    {
        transform.position = transform.parent.TransformPoint(_position);
    }

    public void init(int index, PuzzleCropper puzzleCropper, Vector3 position, Sprite sprite, float scale)
    {
        _index = index;
        _puzzleCropper = puzzleCropper;
        _position = position;
        _scale = scale;
        var imgae = GetComponent<Image>();
        imgae.sprite = sprite;
        // imgae.preserveAspect = true;
    }

    private void OnMouseUp()
    {
        if (Vector3.Distance(transform.position, _puzzleCropper.GetTransformPosition(_index)) < 0.3)
        {
            transform.position = _puzzleCropper.GetTransformPosition(_index);
            Destroy(GetComponent<Collider2D>());
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = transform.parent.TransformPoint(_position);
        }
    }

    private Vector3 GetMousePosition()
    {
        if (Camera.main != null)
        {
            var result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = 0;
            return result;
        }

        return Vector3.zero;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition();
    }

    private void OnMouseDown()
    {
        Debug.Log("Down" + _position);
        transform.localScale = new Vector3(_scale, _scale, 1);
    }
}