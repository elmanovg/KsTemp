using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TestConfiguration", menuName = "ProjectKsuha/TestConfiguration")]
public class TestConfiguration : ScriptableObject
{
    private const int SIZE = 6;

    [Serializable]
    public class SingleTestClass
    {
        public Sprite mainImage;
        public int correctAnswer;

        public Sprite[] optionsImages = new Sprite[6];
    }
    [SerializeField] List<SingleTestClass> tests;
    public List<SingleTestClass> GetTests => tests; 
    void OnValidate()
    {
        foreach (var test in tests)
        {
            if (test.optionsImages.Length != SIZE)
            {
                Debug.LogWarning("Don't change the 'buttons' field's array size!");
                Array.Resize(ref test.optionsImages, SIZE);
            }
        }
    }
}
