using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    public void OnExit() 
    {
        Application.Quit();
    }

    public void OnBack() {
        sceneController.ChangeScene();
    }
}
