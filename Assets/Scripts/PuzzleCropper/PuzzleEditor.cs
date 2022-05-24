using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PuzzleCropper))]
public class PuzzleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PuzzleCropper myTarget = (PuzzleCropper)target;

        if (GUILayout.Button("Generate Nodes"))
        {
            myTarget.UpdateAllImages();
        }
    }
}