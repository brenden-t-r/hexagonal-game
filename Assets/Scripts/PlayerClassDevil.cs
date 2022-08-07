using UnityEngine;

public class PlayerClassDevil : MonoBehaviour, Player.PlayerClass
{
    [SerializeField] private Sprite avatar;

    public string GetName()
    {
        return "Lil' Devil";
    }

    public string GetAvatarFileName()
    {
        return "Devil_Avatar";
    }

    public string GetPrefabName()
    {
        return "Class_Devil";
    }

    
    public Sprite GetAvatar()
    {
        return avatar;
    }
}
