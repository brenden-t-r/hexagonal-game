using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    public GameObject GetPrefab(string name)
    {
        return prefabs.First(item => item.name.Contains(name));
    }
}
