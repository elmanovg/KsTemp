using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Button colorButtonR;
    [SerializeField] private Button colorButtonG;
    [SerializeField] private Button colorButtonB;
    [SerializeField] private Button colorButtonC;
    [SerializeField] private Button colorButtonM;
    [SerializeField] private Button colorButtonY;

    [SerializeField] private Image colorDisabledR;
    [SerializeField] private Image colorDisabledG;
    [SerializeField] private Image colorDisabledB;
    [SerializeField] private Image colorDisabledC;
    [SerializeField] private Image colorDisabledM;
    [SerializeField] private Image colorDisabledY;

    [SerializeField] private Image dinoR;
    [SerializeField] private Image dinoG;
    [SerializeField] private Image dinoB;
    [SerializeField] private Image dinoC;
    [SerializeField] private Image dinoM;
    [SerializeField] private Image dinoY;
    [SerializeField] private Image dinoDefault;
    
    private Button[] colorButtons;
    private Image [] colorDisabled;

    private Image [] dinos; 

    int active = -1;

    void Start () {
        colorButtons = new Button[] {colorButtonR, colorButtonG, colorButtonB,
                                     colorButtonC, colorButtonM, colorButtonY};

        colorDisabled = new Image[] {colorDisabledR, colorDisabledG, colorDisabledB, 
                                     colorDisabledC, colorDisabledM, colorDisabledY};

        foreach (var img in colorDisabled)
        {   
            img.gameObject.SetActive(false);
        }

        dinos = new Image []{dinoR, dinoG, dinoB, dinoC, dinoM, dinoY};
        foreach (var dino in dinos) { 
           dino.gameObject.SetActive(false);
        }

        colorButtonR.onClick.AddListener(() => clickOnColorButtonR());
        colorButtonG.onClick.AddListener(() => clickOnColorButtonG());
        colorButtonB.onClick.AddListener(() => clickOnColorButtonB());
        colorButtonC.onClick.AddListener(() => clickOnColorButtonC());
        colorButtonM.onClick.AddListener(() => clickOnColorButtonM());
        colorButtonY.onClick.AddListener(() => clickOnColorButtonY());
    }

    void Update () {
    }

    void clickOnColorButton(int index) {
        if (active >= 0) {
            colorButtons[active].gameObject.SetActive(true);
            colorDisabled[active].gameObject.SetActive(false);
            dinos[active].gameObject.SetActive(false);
        } else {
            dinoDefault.gameObject.SetActive(false);
        }
        colorButtons[index].gameObject.SetActive(false);
        colorDisabled[index].gameObject.SetActive(true);
        dinos[index].gameObject.SetActive(true);
        active = index;
    }

    void clickOnColorButtonR() {
        clickOnColorButton(0);
    }

    void clickOnColorButtonG() {
        clickOnColorButton(1);
    }
    
    void clickOnColorButtonB() {
        clickOnColorButton(2);
    }
    
    void clickOnColorButtonC() {
        clickOnColorButton(3);
    }
    
    void clickOnColorButtonM() {
        clickOnColorButton(4);
    }
    
    void clickOnColorButtonY() {
        clickOnColorButton(5);
    }
}