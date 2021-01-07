using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GodPortrait : MonoBehaviour
{
    public Sprite defaultImage;
    public Sprite selectedImage;

    private Image thisImage;
    
    public GodBehaviour correspondingGod;
    
    private bool currentlySelected;

    public void Start()
    {
        thisImage = GetComponent<Image>();
    }

    public void UpdateValues()
    {
        thisImage.sprite = correspondingGod.portraitSprite;
    }
}