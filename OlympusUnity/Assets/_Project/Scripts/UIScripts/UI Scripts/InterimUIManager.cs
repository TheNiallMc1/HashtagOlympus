using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterimUIManager : MonoBehaviour
{
    
    private static InterimUIManager _instance;
    public static InterimUIManager Instance => _instance;

    private Dictionary<int, GodBehaviour> allGods;
    private GameObject[] characterDocks;
    private GameObject[] switchButtons;

    public GodBehaviour currentGod;
    public RespectBuff respectBuff;
    public TMP_Text godNameDisplay;
    public TMP_Text godHealthDisplay;

    public Button reviveButton;
    public Button strButton;

    private void Awake()
    {
        // Creating singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    
       // reviveButton.gameObject.SetActive(false);
        allGods = new Dictionary<int, GodBehaviour>();
        characterDocks = GameObject.FindGameObjectsWithTag("CharacterDock");

        if (characterDocks == null) return;
        characterDocks[1].gameObject.SetActive(false);
        characterDocks[2].gameObject.SetActive(false);
        Debug.Log("dock 0 active, 1&2 inactive");
        
        switchButtons = GameObject.FindGameObjectsWithTag("DockSwitch");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignCharacterDocks(Dictionary<int, GodBehaviour> activeGods)
    {
        allGods = activeGods;

        for (int i = 0; i <characterDocks.Length; i++)
        {
            characterDocks[i].GetComponent<CharacterDock>().DockSetUp(activeGods[i]);
            Debug.Log("sending: "+activeGods[i].godName);
        }
        Debug.Log("dock set up complete");
        
        switchButtons[0].GetComponent<DockSwitcherButton>().SetCurrentGod(1, activeGods[1].godName);
        switchButtons[1].GetComponent<DockSwitcherButton>().SetCurrentGod(2, activeGods[2].godName);
    }

    
    public void UpdateHUD(GodBehaviour selectedGod)
    {
           // GameManager.Instance.SelectGod(selectedGod);
           
            //if param is GodBehaviour, need to find key by value
            var myKey = allGods.FirstOrDefault(x => x.Value == selectedGod).Key;

            for (int i = 0; i < characterDocks.Length; i++)
            {
                characterDocks[i].gameObject.SetActive(false);
            }
            
            characterDocks[myKey].gameObject.SetActive(true);
            
            ReOrderButtons(myKey);
            
        }

    public void UpdateHUD(int key)
    {
        GameManager.Instance.SelectGod(allGods[key]);
        
        for (int i = 0; i < characterDocks.Length; i++)
        {
            characterDocks[i].gameObject.SetActive(false);
        }
            
        characterDocks[key].gameObject.SetActive(true);
        
        ReOrderButtons(key);
    }

    public void ReOrderButtons(int activeKey)
    {
        //reorder buttons
        List <int> remainder = new List<int>();
        remainder.Add(0);
        remainder.Add(1);
        remainder.Add(2);
        remainder.Remove(activeKey);
        
        List <int> currentButtonOrder = new List<int>();
        currentButtonOrder.Add(switchButtons[0].GetComponent<DockSwitcherButton>().godKey);
        currentButtonOrder.Add(switchButtons[1].GetComponent<DockSwitcherButton>().godKey);
        currentButtonOrder.Remove(activeKey);
        remainder.Remove(currentButtonOrder[0]);
        currentButtonOrder.Add(remainder[0]);
        
        switchButtons[0].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[0], allGods[currentButtonOrder[0]].godName);
        switchButtons[1].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[1], allGods[currentButtonOrder[1]].godName);
    }
        

    void ShowReviveButton()
    {
       // reviveButton.gameObject.SetActive(true);
    }

    public void ReviveButton()
    {
        //currentGod.Revive();
        //reviveButton.gameObject.SetActive(false);
        //GameManager.Instance.RemoveRespect(currentGod.costToRespawn);
        //Debug.Log("revive pressed");
    }

    public void StrengthBuff()
    {
        respectBuff.ApplyBuff(currentGod, 30, ref currentGod.attackDamage, 10);
        StartCoroutine(StrengthBuffCoolDown());
    }
    
    IEnumerator StrengthBuffCoolDown()
    {
        yield return new WaitForSeconds(2f);
        respectBuff.RemoveBuff(currentGod, 0, ref currentGod.attackDamage, 10);

    }
}
