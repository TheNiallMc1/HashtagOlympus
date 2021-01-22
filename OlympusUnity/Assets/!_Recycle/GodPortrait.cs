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
        currentlySelected = GameManager.Instance.currentlySelectedGod == correspondingGod;
    }

    public void ClickToSelectGod()
    {
        print("clicked");
        //GameManager.Instance.SelectGod(correspondingGod);
        GameManager.Instance.SelectGod(correspondingGod);
    }
}