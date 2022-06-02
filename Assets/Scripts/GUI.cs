using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GUI : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI textHeaderTurn;
    [SerializeField] private TextMeshProUGUI textHeaderPlayerName;
    [SerializeField] private TextMeshProUGUI textHeaderMapName;
    [SerializeField] private Slider sliderMalaise;
    [SerializeField] private Slider sliderInsanity;
    [SerializeField] private Slider sliderTerror;
    [SerializeField] private Slider sliderHeartache;
    [SerializeField] private TextMeshProUGUI sliderMalaiseText;
    [SerializeField] private TextMeshProUGUI sliderInsanityText;
    [SerializeField] private TextMeshProUGUI sliderTerrorText;
    [SerializeField] private TextMeshProUGUI sliderHeartacheText;
    [SerializeField] private TextMeshProUGUI textAvatarName;
    [SerializeField] private TextMeshProUGUI textAvatarClass;
    [SerializeField] private Image imageAvatar; // TODO


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
