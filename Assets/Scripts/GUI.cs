using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GUI : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI textHeaderTurn;
    [SerializeField] TextMeshProUGUI textHeaderPlayerName;

    [SerializeField] TextMeshProUGUI textHeaderMapName;
    [SerializeField] Slider sliderMalaise;
    [SerializeField] private TextMeshProUGUI sliderMalaiseText;
    [SerializeField] Slider sliderInsanity;
    [SerializeField] private TextMeshProUGUI sliderInsanityText;
    [SerializeField] Slider sliderTerror;
    [SerializeField] private TextMeshProUGUI sliderTerrorText;
    [SerializeField] Slider sliderHeartache;
    [SerializeField] private TextMeshProUGUI sliderHeartacheText;
    [SerializeField] TextMeshProUGUI textAvatarName;
    [SerializeField] TextMeshProUGUI textAvatarClass;
    [SerializeField] Image imageAvatar;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textHeaderPlayerName.SetText(player.playerName);
        textAvatarName.SetText(player.playerName);
        textAvatarClass.SetText(player.playerClass.GetName());
        textHeaderMapName.SetText("Bob's City");
        textHeaderTurn.SetText("1");
        sliderMalaise.value = player.malaise;
        sliderMalaiseText.SetText($"{sliderMalaise.value:P0}");
        sliderTerror.value = player.terror;
        sliderTerrorText.SetText($"{sliderTerror.value:P0}");
        sliderInsanity.value = player.insanity;
        sliderInsanityText.SetText($"{sliderInsanity.value:P0}");
        sliderHeartache.value = player.heartache;
        sliderHeartacheText.SetText($"{sliderHeartache.value:P0}");
    }

    void OnGUI() {
        
    }
}
