using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundOnOff : MonoBehaviour
{
    [SerializeField] private Button soundOnButton;
    [SerializeField] private Button soundOffButton;

    // Start is called before the first frame update
    void Start()
    {
        flipActivity(soundOffButton);
        soundOnButton.onClick.AddListener(() => clickOnSoundOn());
        soundOffButton.onClick.AddListener(() => clickOnSoundOn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void flipActivity(Button button) {
        bool isActive = button.gameObject.activeSelf;
        isActive = !isActive;
        button.gameObject.SetActive(isActive);
    }

    void clickOnSoundOn() {
        flipActivity(soundOnButton);
        flipActivity(soundOffButton);
    }

    void clickOffSoundOn() {
        flipActivity(soundOnButton);
        flipActivity(soundOffButton);
    }
}
