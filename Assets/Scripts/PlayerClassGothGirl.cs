using UnityEngine;

public class PlayerClassGothGirl : MonoBehaviour, Player.PlayerClass
{
    [SerializeField] private Sprite avatar;

    public string GetName()
    {
        return "Goth Girl";
    }

    public string GetAvatarFileName()
    {
        return "Goth_Avatar";
    }
        
    public string GetPrefabName()
    {
        return "Class_GothGirl";
    }

    public Sprite GetAvatar()
    {
        return avatar;
    }
}