using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterimUIManager : MonoBehaviour
{
    #region Singleton
    public static InterimUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(InterimUIManager)) as InterimUIManager;
 
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static InterimUIManager instance;
    #endregion
   

    private Dictionary<int, GodBehaviour> allGods;
    public GameObject[] characterDocks;
    public GameObject[] switchButtons;

    public GodBehaviour currentGod;
    public RespectBuff respectBuff;

    private void Start()
    {
        allGods = new Dictionary<int, GodBehaviour>();
        //characterDocks = GameObject.FindGameObjectsWithTag("CharacterDock");

        if (characterDocks == null) return;
        characterDocks[1].gameObject.SetActive(false);
        characterDocks[2].gameObject.SetActive(false);
        Debug.Log("dock 0 active, 1&2 inactive");
        
       // switchButtons = GameObject.FindGameObjectsWithTag("DockSwitch");
        
    }
    
    public void AssignCharacterDocks(Dictionary<int, GodBehaviour> activeGods)
    {
        Debug.Log("number of entries in dict in UImanager: "+activeGods.Count);
        
        foreach (KeyValuePair<int, GodBehaviour> kvp in activeGods)
            Debug.Log("Key + Value: "+ kvp.Key +", "+ kvp.Value.godName);

        //foreach (KeyValuePair<int, GodBehaviour> kvp in activeGods)
        //    allGods.Add(kvp.Key,kvp.Value);
        
        allGods = activeGods;
        Debug.Log("all gods: "+allGods.Count);
        
        foreach (KeyValuePair<int, GodBehaviour> kvp in allGods)
            Debug.Log("Key + Value: "+ kvp.Key +", "+ kvp.Value.godName);


       // for (int i = 0; i <characterDocks.Length; i++)
        //{
        //    characterDocks[i].GetComponent<CharacterDock>().DockSetUp(activeGods[i]);
       //     Debug.Log("sending: "+activeGods[i].godName);
       // }
       
       characterDocks[0].GetComponent<CharacterDock>().DockSetUp(activeGods[0]);
       characterDocks[1].GetComponent<CharacterDock>().DockSetUp(activeGods[1]);
       characterDocks[2].GetComponent<CharacterDock>().DockSetUp(activeGods[2]);
       
        
        Debug.Log("dock set up complete");

        switchButtons[0].GetComponent<DockSwitcherButton>()
            .SetCurrentGod(1, activeGods[1].gameObject.GetComponent<Combatant>().characterName);
        switchButtons[1].GetComponent<DockSwitcherButton>().SetCurrentGod(2, activeGods[2].gameObject.GetComponent<Combatant>().characterName);
    }

    
    public void UpdateHUD(GodBehaviour selectedGod)
    {
           // GameManager.Instance.SelectGod(selectedGod);
           
            //if param is GodBehaviour, need to find key by value
            var myKey = allGods.FirstOrDefault(x => x.Value == selectedGod).Key;
            //CameraController.Instance.FollowPlayer(allGods[myKey].gameObject);
            CameraController.Instance.FollowPlayer(GameManager.Instance.godDict[myKey].gameObject.GetComponent<GodBehaviour>());
            
            for (int i = 0; i < characterDocks.Length; i++)
            {
                characterDocks[i].gameObject.SetActive(false);
            }
            
            characterDocks[myKey].gameObject.SetActive(true);
            characterDocks[myKey].GetComponent<CharacterDock>().UpdateCharacterDock();
            ReOrderButtons(myKey);
    }

    public void UpdateHUD(int key)
    {
        Debug.Log("key received in UpdateHUD: "+key);
        Debug.Log("dictionary count in UpdateHUD: "+allGods.Count);
        foreach (KeyValuePair<int, GodBehaviour> kvp in allGods)
            Debug.Log("Key + Value: "+ kvp.Key +", "+ kvp.Value.godName);
        
        //GameManager.Instance.SelectGod(allGods[key]);
        //CameraController.Instance.FollowPlayer(allGods[key].gameObject);
        
        GameManager.Instance.SelectGod(GameManager.Instance.godDict[key]);
        CameraController.Instance.FollowPlayer(GameManager.Instance.godDict[key].gameObject.GetComponent<GodBehaviour>());
        
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
        
        //switchButtons[0].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[0], allGods[currentButtonOrder[0]].gameObject.GetComponent<Combatant>().characterName);
        //switchButtons[1].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[1], allGods[currentButtonOrder[1]].gameObject.GetComponent<Combatant>().characterName);
        
        switchButtons[0].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[0], GameManager.Instance.godDict[currentButtonOrder[0]].gameObject.GetComponent<Combatant>().characterName);
        switchButtons[1].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[1], GameManager.Instance.godDict[currentButtonOrder[1]].gameObject.GetComponent<Combatant>().characterName);
    }
}
