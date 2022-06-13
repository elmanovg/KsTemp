using UnityEngine;

[CreateAssetMenu(fileName = "UserStorage", menuName = "ProjectKsuha/UserStorage")]
public class UserStorage : ScriptableObject
{
    [SerializeField] public string login;
    [SerializeField] public string password;
}
