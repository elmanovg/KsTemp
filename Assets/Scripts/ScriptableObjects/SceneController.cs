using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneController", menuName = "ProjectKsuha/SceneController")]
public class SceneController : ScriptableObject
{
    [SerializeField] string scenesOrder;

    public void ChangeScene()
    {
        SceneManager.LoadScene(scenesOrder);
    }
}