using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum barType { healthBar, manaBar };

public class PlayerBar : MonoBehaviour
{
    private Slider Slider;
    public barType barType;
    // Start is called before the first frame update
    void Start()
    {
        Slider = GetComponent<Slider>();
        switch (this.barType)
        {
            case barType.healthBar:
                Slider.maxValue = PlayerController.MAX_HEALTH;
                break;
            case barType.manaBar:
                Slider.maxValue = PlayerController.MAX_MANA;   
                break;
        }
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.barType)
        {
            case barType.healthBar:
                Slider.value = GameObject.Find("Player").GetComponent<PlayerController>().getHealth();
                break;
            case barType.manaBar:
                Slider.value = GameObject.Find("Player").GetComponent<PlayerController>().getMana();
                break;
        }
    }
}
