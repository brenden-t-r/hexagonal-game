using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Prefabs prefabs;
    [SerializeField] private Animator animator;
    public string playerName;
    public PlayerClass playerClass;
    public GameObject prefab;
    public float malaise;
    public float insanity;
    public float terror;
    public float heartache;

    // Start is called before the first frame update
    void Start()
    {
        playerName = PlayerData.playerName;
        playerClass = PlayerData.playerClass;
        prefab = prefabs.GetPrefab(playerClass.GetPrefabName());
        animator.runtimeAnimatorController = prefab.GetComponent<Animator>().runtimeAnimatorController;
        malaise = 0.3f;
        insanity = 0.1f;
        terror = 0.4f;
        heartache = 0.2f;
    }

    public interface PlayerClass
    {
        string GetName();
        string GetAvatarFileName();
        string GetPrefabName();
    }

    public class GothGirl : PlayerClass
    {

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
    }
    
    public class Devil : PlayerClass
    {

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
    }
}
