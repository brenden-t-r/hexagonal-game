using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Avatars : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    public Sprite GetAvatar(string name)
    {
        return sprites.First(item => item.name.Contains(name));
    }
}
