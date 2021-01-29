using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DockSwitcherButton : MonoBehaviour
{
    public TMP_Text godNameDisplay;
    private Button myButton;
    public int godKey;
    public Sprite currentSprite;

    private void Start()
    {
        myButton = GetComponent<Button>();
    }

    public void SetCurrentGod(int key, string godName, Sprite newSprite)
    {
        godKey = key;
        godNameDisplay.text = godName;
        myButton.image.sprite = newSprite;
        
        Debug.Log("switch button: "+godName);
    }

    public void SendSwitchInfo()
    {
        Debug.Log("I am sending key: "+godKey);
        
        InterimUIManager.Instance.UpdateHUD(godKey);
    }
}

