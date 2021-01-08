using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GodPortrait : MonoBehaviour
{
    private Image thisImage;
    
    public GodBehaviour correspondingGod;
    
    private bool currentlySelected;

    public void Start()
    {
        thisImage = GetComponent<Image>();
    }

    public void UpdateValues()
    {
        if (GameManager.Instance.currentlySelectedGod == correspondingGod)
        {
            currentlySelected = true;
        }

        thisImage.sprite = currentlySelected ? correspondingGod.portraitSprite : correspondingGod.portraitSpriteSelected;
    }

    public void ClickToSelectGod()
    {
        print("clicked");
        GameManager.Instance.SelectGod(correspondingGod);
    }
}