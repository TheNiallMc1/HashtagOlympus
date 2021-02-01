using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        set => instance = value;
    }

    private static InterimUIManager instance;

    #endregion


    private Dictionary<int, GodBehaviour> allGods;
    public GameObject[] characterDocks;
    public GameObject[] switchButtons;

    private void Start()
    {
        allGods = new Dictionary<int, GodBehaviour>();

        if (characterDocks == null) return;
        characterDocks[1].gameObject.SetActive(false);
        characterDocks[2].gameObject.SetActive(false);
    }

    public void AssignCharacterDocks(Dictionary<int, GodBehaviour> activeGods)
    {
        allGods = activeGods;

        characterDocks[0].GetComponent<CharacterDock>().DockSetUp(activeGods[0]);
        characterDocks[1].GetComponent<CharacterDock>().DockSetUp(activeGods[1]);
        characterDocks[2].GetComponent<CharacterDock>().DockSetUp(activeGods[2]);

        switchButtons[0].GetComponent<DockSwitcherButton>()
            .SetCurrentGod(1, activeGods[1].gameObject.GetComponent<Combatant>().characterName,
                activeGods[1].gameObject.GetComponent<Combatant>().characterSprite);
        switchButtons[1].GetComponent<DockSwitcherButton>().SetCurrentGod(2,
            activeGods[2].gameObject.GetComponent<Combatant>().characterName,
            activeGods[2].gameObject.GetComponent<Combatant>().characterSprite);
    }


    public void UpdateHUD(GodBehaviour selectedGod)
    {
        //if param is GodBehaviour, need to find key by value
        var myKey = allGods.FirstOrDefault(x => x.Value == selectedGod).Key;
        
        CameraController.Instance.FollowPlayer(GameManager.Instance.godDict[myKey].gameObject.GetComponent<GodBehaviour>());

        foreach (GameObject t in characterDocks)
        {
            t.gameObject.SetActive(false);
        }

        characterDocks[myKey].gameObject.SetActive(true);
        characterDocks[myKey].GetComponent<CharacterDock>().UpdateCharacterDock();
        ReOrderButtons(myKey);
    }

    public void UpdateHUD(int key)
    {
        GameManager.Instance.SelectGod(GameManager.Instance.godDict[key]);
        CameraController.Instance.FollowPlayer(GameManager.Instance.godDict[key].gameObject.GetComponent<GodBehaviour>());

        foreach (GameObject t in characterDocks)
        {
            t.gameObject.SetActive(false);
        }

        characterDocks[key].gameObject.SetActive(true);


        ReOrderButtons(key);
    }

    private void ReOrderButtons(int activeKey)
    {
        List<int> remainder = new List<int>();
        remainder.Add(0);
        remainder.Add(1);
        remainder.Add(2);
        remainder.Remove(activeKey);

        List<int> currentButtonOrder = new List<int>
        {
            switchButtons[0].GetComponent<DockSwitcherButton>().godKey,
            switchButtons[1].GetComponent<DockSwitcherButton>().godKey
        };
        
        currentButtonOrder.Remove(activeKey);
        remainder.Remove(currentButtonOrder[0]);
        currentButtonOrder.Add(remainder[0]);

        switchButtons[0].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[0],
            GameManager.Instance.godDict[currentButtonOrder[0]].gameObject.GetComponent<Combatant>().characterName,
            GameManager.Instance.godDict[currentButtonOrder[0]].gameObject.GetComponent<Combatant>().characterSprite);
        switchButtons[1].GetComponent<DockSwitcherButton>().SetCurrentGod(currentButtonOrder[1],
            GameManager.Instance.godDict[currentButtonOrder[1]].gameObject.GetComponent<Combatant>().characterName,
            GameManager.Instance.godDict[currentButtonOrder[1]].gameObject.GetComponent<Combatant>().characterSprite);
    }
}