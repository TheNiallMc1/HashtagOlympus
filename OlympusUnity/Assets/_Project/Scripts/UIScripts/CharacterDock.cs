using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterDock : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Combatant godCombatant;
    private GodBehaviour godBehaviour;
    private CharacterToolTipInfo tooltipInfo;
    
   // public RespectBuff respectBuff;
    public TMP_Text godNameDisplay;
    public TMP_Text godHealthDisplay;

    public Button reviveButton;
    public Button strButton;
    public HealthBar healthBar;
    public RespectBuff respectBuff;

    public List<TooltipTrigger> abilityTooltips;
    public GameObject abilityButtons;
    public Image mainSprite;

    private void Awake()
    {
        reviveButton.gameObject.SetActive(false);
       // UpdateCharacterDock();
    }

    public void UpdateCharacterDock()
    {
        mainSprite.sprite = godCombatant.characterSprite;
        tooltipInfo = godCombatant.gameObject.GetComponent<CharacterToolTipInfo>();
        godNameDisplay.text = godCombatant.characterName;
        godHealthDisplay.text = godCombatant.currentHealth + "/" + godCombatant.maxHealth;
        healthBar.healthValue = godCombatant.currentHealth;

        /*foreach (var tooltip in abilityTooltips)
        {
            for (int i = 0; i < 11; i++)
            {
                tooltip.header = tooltipInfo.allTooltips[i];
                i++;
                tooltip.content = tooltipInfo.allTooltips[i];
                i++;
            }
        }*/

      UpdateTooltips();

        if (godBehaviour.isKOed)
        {
            ShowReviveButton();
        }
    }

    public void DockSetUp(GodBehaviour assignedGod)
    {
        //mainSprite.sprite = godCombatant.characterSprite;
        Debug.Log("setting up docks");
        godBehaviour = assignedGod;
        godCombatant = assignedGod.gameObject.GetComponent<Combatant>();
        godNameDisplay.text = godCombatant.characterName;
        godHealthDisplay.text = godCombatant.currentHealth + "/" + godCombatant.maxHealth;
        healthBar.healthValue = godCombatant.currentHealth;
        //UpdateTooltips();

        if (godBehaviour.isKOed)
        {
            ShowReviveButton();
        }
        //adding correct ability buttons
    }

    public void UpdateTooltips()
    {
        if (tooltipInfo.allTooltips.Count == 10)
        {
            abilityTooltips[0].header = tooltipInfo.allTooltips[0];
            abilityTooltips[0].content = tooltipInfo.allTooltips[1];

            abilityTooltips[1].header = tooltipInfo.allTooltips[2];
            abilityTooltips[1].content = tooltipInfo.allTooltips[3];

            abilityTooltips[2].header = tooltipInfo.allTooltips[4];
            abilityTooltips[2].content = tooltipInfo.allTooltips[5];

            abilityTooltips[3].header = tooltipInfo.allTooltips[6];
            abilityTooltips[3].content = tooltipInfo.allTooltips[7];

            abilityTooltips[4].header = tooltipInfo.allTooltips[8];
            abilityTooltips[4].content = tooltipInfo.allTooltips[9];
        }
    }
    
    void ShowReviveButton()
    {
        abilityButtons.gameObject.SetActive(false);
        reviveButton.gameObject.SetActive(true);
    }
    
    public void ReviveButton()
    {
        godBehaviour.Revive();
        abilityButtons.gameObject.SetActive(true);
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
