using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSwitchButton : MonoBehaviour
{

    public Image mainSprite;


    public void Start()
    {
        mainSprite = GetComponent<Image>();
    }
    
    public void UpdateMainSprite()
    {
       mainSprite.sprite = GameManager.Instance.currentlySelectedGod.gameObject.GetComponent<Combatant>().characterSprite;

    }

    public void SnapToCurrentGod()
    {
        Debug.Log("main clciked!");
        if (GameManager.Instance.currentlySelectedGod != null)
        {
            CameraController.Instance.FollowPlayer(GameManager.Instance.currentlySelectedGod);
        }
        else
        {
            Debug.Log("i'm in the else please work");
            GameManager.Instance.currentlySelectedGod = GameManager.Instance.allPlayerGods[0];
            CameraController.Instance.FollowPlayer(GameManager.Instance.currentlySelectedGod);
            InterimUIManager.Instance.UpdateHUD(0);
        }
    }
}
