using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string playerName;
    public PlayerClass playerClass;
    public float malaise;
    public float insanity;
    public float terror;
    public float heartache;

    // Start is called before the first frame update
    void Start()
    {
        playerName = "Brenden";
        playerClass = new GothGirl();
        malaise = 0.3f;
        insanity = 0.1f; 
        terror = 0.4f;
        heartache = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public interface PlayerClass
    {
        string GetName();
    }

    public class GothGirl : PlayerClass
    {

        public string GetName()
        {
            return "Goth Girl";
        }
    }
}
