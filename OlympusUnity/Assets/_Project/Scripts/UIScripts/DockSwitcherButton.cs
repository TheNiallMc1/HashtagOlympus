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
    private int godKey;
    public Sprite currentSprite;
    [SerializeField]
    public List<Sprite> godSprites;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    public int GetGodKey()
    {
        return godKey;
    }

    public void SetCurrentGod(int key, string godName)
    {
        Debug.Log("setting button to : " + godName + godKey);
        godKey = key;
        // godNameDisplay.text = godName;
        //myButton.image.sprite = GameManager.Instance.godDict[key].gameObject.GetComponent<Combatant>().characterSprite;
        myButton.image.sprite = godSprites[key];
    }

    public void SendSwitchInfo()
    {
        Debug.Log("I am sending key: " + godKey);

        InterimUIManager.Instance.UpdateHUD(godKey);
    }
}