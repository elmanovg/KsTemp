using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class FirstScript : MonoBehaviour
{
    [SerializeField] private RectTransform firstDialog;
    [SerializeField] private TMP_InputField field;

    public void OnClick()
    {
        change(field);
        firstDialog.gameObject.SetActive(false);
    }

    private void change(TMP_InputField s)
    {
        s.text = "123";
    }
}