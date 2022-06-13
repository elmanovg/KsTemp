using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour 
{
    [SerializeField] private TMP_InputField loginField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private UserStorage userStorage;
    [SerializeField] private SceneController psycScene;
    [SerializeField] private SceneController childScene;
    [SerializeField] private GameObject loginImage1;
    [SerializeField] private GameObject loginImage2;
    [SerializeField] private GameObject loginImage3;
    [SerializeField] private GameObject loginImage4;
    [SerializeField] private GameObject loginImage5;
    [SerializeField] private GameObject loginImage6;
    [SerializeField] private GameObject loginImage7;
    [SerializeField] private GameObject psycd;
    [SerializeField] private GameObject psyca;
    [SerializeField] private GameObject childd;
    [SerializeField] private GameObject childa;
    [SerializeField] private GameObject buttonOff3;
    [SerializeField] private GameObject buttonOn3;
    [SerializeField] private GameObject boy;
    [SerializeField] private GameObject girl;
    [SerializeField] private GameObject boya;
    [SerializeField] private GameObject girla;
    [SerializeField] private GameObject buttonOff4;
    [SerializeField] private GameObject buttonOn4;

    void Start() {
        loginImage2.SetActive(false);
        loginImage3.SetActive(false);
        loginImage4.SetActive(false);
        loginImage5.SetActive(false);
        loginImage6.SetActive(false);
        loginImage7.SetActive(false);

        psyca.SetActive(false);
        childa.SetActive(false);

        boya.SetActive(false);
        girla.SetActive(false);

        buttonOn3.SetActive(false);
        buttonOn4.SetActive(false);
    }

    public void OnClick1() 
    {
        userStorage.login = loginField.text;
        userStorage.password = passwordField.text;

        loginImage1.SetActive(false);
        loginImage3.SetActive(true);
    }

    
    public void OnClick2() {}
    

    public void Button3OnClick() 
    {
        loginImage3.SetActive(false);
        loginImage4.SetActive(true);
    }

    public void Button4OnClick()
    {
        loginImage4.SetActive(false);
        loginImage5.SetActive(true);
    }

    public void Button5OnClick()
    {
        loginImage5.SetActive(false);
        loginImage6.SetActive(true);
    }

    public void Button6OnClick()
    {
        loginImage6.SetActive(false);
        loginImage7.SetActive(true);
    }

    public void Button7OnClick() 
    {
        if (userStorage.isPsyc) {
            psycScene.ChangeScene();
        } else {
            childScene.ChangeScene();
        }
    }

    public void ForgotPasswordOnClick() 
    {
        loginImage1.SetActive(false);
        loginImage2.SetActive(true);
    }

    //---------------------------------

    public void PsycdClick() 
    {
        childa.SetActive(false);
        childd.SetActive(false);
        
        childd.SetActive(true);
        psyca.SetActive(true);

        ButtonOn(buttonOff3, buttonOn3);        
    }

    public void ChilddClick() 
    {
        psyca.SetActive(false);
        childd.SetActive(false);
        
        childa.SetActive(true);
        psycd.SetActive(true);

        ButtonOn(buttonOff3, buttonOn3);       
    }

    public void PsycaClick() 
    {
        psyca.SetActive(false);
        psycd.SetActive(true);

        ButtonOff(buttonOff3, buttonOn3);      
    }

    public void ChildaClick() 
    {
        childa.SetActive(false);
        childd.SetActive(true);

        ButtonOff(buttonOff3, buttonOn3);
    }

    //----------------------------
    public void BoyClick() 
    {
        girla.SetActive(false);
        
        girl.SetActive(true);
        boya.SetActive(true);

        ButtonOn(buttonOff4, buttonOn4);        
    }

    public void GirlClick() 
    {
        boya.SetActive(false);
        
        boy.SetActive(true);
        girla.SetActive(true);

        ButtonOn(buttonOff4, buttonOn4);       
    }

    public void BoyaClick() 
    {
        boya.SetActive(false);
        boy.SetActive(true);

        ButtonOff(buttonOff4, buttonOn4);      
    }

    public void GirlaClick() 
    {
        girla.SetActive(false);
        girl.SetActive(true);

        ButtonOff(buttonOff4, buttonOn4);
    }
    //------------------------------------

    private void ButtonOn(GameObject buttonOff, GameObject buttonOn) 
    {
        buttonOff.SetActive(false);
        buttonOn.SetActive(true);
    }

    private void ButtonOff(GameObject buttonOff, GameObject buttonOn) 
    {
        buttonOff.SetActive(true);
        buttonOn.SetActive(false);
    }
}
