using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterDock : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Combatant godCombatant;
    private GodBehaviour godBehaviour;
   // public RespectBuff respectBuff;
    public TMP_Text godNameDisplay;
    public TMP_Text godHealthDisplay;

    public Button reviveButton;
    public Button strButton;
    public HealthBar healthBar;
    public RespectBuff respectBuff;

    private void Awake()
    {
        reviveButton.gameObject.SetActive(false);
       // UpdateCharacterDock();
    }

    public void UpdateCharacterDock()
    {
        godNameDisplay.text = godCombatant.characterName;
        godHealthDisplay.text = godCombatant.currentHealth + "/" + godCombatant.maxHealth;
        healthBar.healthValue = godCombatant.currentHealth;

        if (godBehaviour.isKOed)
        {
            ShowReviveButton();
        }
    }

    public void DockSetUp(GodBehaviour assignedGod)
    {
        Debug.Log("setting up docks");
        godBehaviour = assignedGod;
        godCombatant = assignedGod.gameObject.GetComponent<Combatant>();
        godNameDisplay.text = godCombatant.characterName;
        godHealthDisplay.text = godCombatant.currentHealth + "/" + godCombatant.maxHealth;
        healthBar.healthValue = godCombatant.currentHealth;

        if (godBehaviour.isKOed)
        {
            ShowReviveButton();
        }
        //adding correct ability buttons
    }
    
    void ShowReviveButton()
    {
        reviveButton.gameObject.SetActive(true);
    }
    
    public void ReviveButton()
    {
        godBehaviour.Revive();
        reviveButton.gameObject.SetActive(false);
        GameManager.Instance.RemoveRespect(godBehaviour.costToRespawn);
        Debug.Log("revive pressed");
    }

    public void StrengthBuff()
    {
        respectBuff.ApplyBuff(godCombatant, 30, ref godCombatant.attackDamage, 10);
        StartCoroutine(StrengthBuffCoolDown());
    }
    
    IEnumerator StrengthBuffCoolDown()
    {
        yield return new WaitForSeconds(2f);
        respectBuff.RemoveBuff(godCombatant, 0, ref godCombatant.attackDamage, 10);
    }
}
