using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileController : MonoBehaviour {

    [SerializeField] private UserStorage userStorage;
    [SerializeField] private TextMeshProUGUI login;
    [SerializeField] private TextMeshProUGUI password;
    [SerializeField] private TextMeshProUGUI fullName;
    [SerializeField] private TextMeshProUGUI sex;
    [SerializeField] private TextMeshProUGUI age;
    [SerializeField] private Image egg1;
    [SerializeField] private Image egg2;
    [SerializeField] private Image egg3;
    [SerializeField] private Image egg4;
    [SerializeField] private Image egg5;
    [SerializeField] private Image egg6;

    [SerializeField] private TextMeshProUGUI scorePercent;
    [SerializeField] private Image dino;

    [SerializeField] private Image sexFaceTrue;
    [SerializeField] private Image sexFaceFalse;


    private int maxScore = 3;
    

    private Image [] eggs;

    

    private string hidePassword(string s, int showN) {
        if (s.Length >= showN) {
           string res = s.Substring(0, showN);
           string rest = new string('*', s.Length - showN);
           return res + rest;
        } else {
           return new string ('*', s.Length);
        }
    }

    private void ActivateEggs(int score) {
        float score_ = (float)maxScore / (float)score;
        int cntEggs = (int)((float)eggs.Length / score_);
        for (int i = 0; i < cntEggs; i++)
        {
            if (i < eggs.Length) {
                eggs[i].gameObject.SetActive(true);
            }
        }
    }

    private float GetHeight(GameObject obj) {
        var p1 = obj.transform.TransformPoint(0, 0, 0);
        var p2 = obj.gameObject.transform.TransformPoint(1, 1, 0);
        var w = p2.x - p1.x;
        var h = p2.y - p1.y;
        return h;
    }

    private void rescaleDino(int score) {
        
        Vector3 scale = new Vector3((float)score / (float)maxScore, (float)score / (float)maxScore, 1);
        var h1 = GetHeight(dino.gameObject);
        dino.gameObject.transform.localScale = scale;
        var h2 = GetHeight(dino.gameObject);
        
        Vector3 positionChange = new Vector3(0, -(h1 - h2)*220, 0);
        dino.gameObject.transform.position += positionChange;
    }

    void Start () {
        login.SetText(userStorage.login);
        password.SetText(hidePassword(userStorage.password, 2));
        fullName.SetText(userStorage.name + " " + userStorage.surname + " " + userStorage.fname);
        if (userStorage.isBoy) { 
            sex.SetText("мужской");
            sexFaceTrue.gameObject.SetActive(true);
            sexFaceFalse.gameObject.SetActive(false);
        } else { 
            sex.SetText("женский"); 
            sexFaceTrue.gameObject.SetActive(false);
            sexFaceFalse.gameObject.SetActive(true);
        }
        age.SetText(userStorage.age.ToString());

        eggs = new Image [] {egg1, egg2, egg3, egg4, egg5, egg6};
        foreach (var egg in eggs)
        { egg.gameObject.SetActive(false); }
        ActivateEggs(userStorage.score);
        scorePercent.SetText((userStorage.score * 100 / maxScore).ToString() + "%");
        rescaleDino(userStorage.score);
    }

    void Update () {

    }
}