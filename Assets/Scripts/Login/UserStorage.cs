using UnityEngine;

[CreateAssetMenu(fileName = "UserStorage", menuName = "ProjectKsuha/UserStorage")]
public class UserStorage : ScriptableObject
{
    [SerializeField] public string login;
    [SerializeField] public string password;
    [SerializeField] public bool isPsyc;
    [SerializeField] public string email;
    [SerializeField] public string surname;
    [SerializeField] public string name;
    [SerializeField] public string fname;
    [SerializeField] public int age;
    [SerializeField] public bool isBoy;
    [SerializeField] public int score;
}
