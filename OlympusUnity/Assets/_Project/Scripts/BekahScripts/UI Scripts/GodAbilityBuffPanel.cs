using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodAbilityBuffPanel : MonoBehaviour
{

    public GodBehaviour currentGod;

    public TMP_Text godNameDisplay;
    public TMP_Text godHealthDisplay;

    public Button reviveButton;
    public Button strButton;
    


    private void Awake()
    {
        if(currentGod != null)
        {
            godNameDisplay.text = currentGod.godName;
            godHealthDisplay.text = currentGod.currentHealth + "/" + currentGod.maxHealth;
        }
        reviveButton.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGod != null)
        {
            godNameDisplay.text = currentGod.godName;
            godHealthDisplay.text = currentGod.currentHealth + "/" + currentGod.maxHealth;

            if (currentGod.isKOed)
            {
                ShowReviveButton();
            }
        }
    }

    void ShowReviveButton()
    {
        reviveButton.gameObject.SetActive(true);
    }

    public void ReviveButton()
    {
        currentGod.Revive();
        reviveButton.gameObject.SetActive(false);
        GameManager.Instance.RemoveRespect(currentGod.costToRespawn);
        Debug.Log("revive pressed");
    }

    public void StrengthBuff()
    {
        Debug.Log("buffing");
        currentGod.attackDamage += 10;
        GameManager.Instance.RemoveRespect(50);
        StartCoroutine(BuffCoolDown());
    }

    public void StrengthDebuf()
    {
        Debug.Log("debuffing");
        currentGod.attackDamage -= 10;
    }

    IEnumerator BuffCoolDown()
    {
        yield return new WaitForSeconds(2f);
        StrengthDebuf();

    }
    
    public void HealBuff()
    {
        
    }
}
