using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private PlayerControls playerControls;
    private Camera cam;
    public Camera currentCam;
    public Camera overViewCam; 
    private UIManager uiManager;
    
    
    // Gods and God Selection
    public List<GodBehaviour> allPlayerGods;
    public bool godSelected;
    public GodBehaviour currentlySelectedGod;
    private int currentGodIndex;

    public LineDrawer lD;

    // Respect
    public int currentRespect;
    public TMP_Text respectDisplay;
    private String respectText;
    public int summonRespectThreshold;
    private bool canSummon;
    
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

        uiManager = FindObjectOfType<UIManager>();
        
        cam = Camera.current;
        currentCam = cam;

        // Controls
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Movement.MouseClick.performed += context => ClickSelect();
        playerControls.GodSelection.CycleThroughGods.performed += context => CycleSelect();

        //respectText = respectDisplay.text+" ";
        respectDisplay.text = respectText+currentRespect;
        canSummon = false;
        
        
    }

    private void Start()
    {
        PopulateAllPlayerGods();
    }

    public void PopulateAllPlayerGods()
    {
        allPlayerGods = UberManager.Instance.selectedGods;
        GodListToDictionary();
    }

    private void GodListToDictionary()
    {
        Debug.Log("Just making sure i am running");
        Dictionary<int, GodBehaviour> godDict = new Dictionary<int, GodBehaviour>();

        for (int i = 0; i < allPlayerGods.Count; i++)
        { godDict.Add(i, allPlayerGods[i]);
        }

        InterimUIManager.Instance.BoomBoom();
        InterimUIManager.Instance.AssignCharacterDocks(godDict);
        InterimUIManager.Instance.BoomBoom();
    }

    private void CycleSelect()
    {
        
        currentGodIndex += 1;
        
        if (currentGodIndex > allPlayerGods.Count - 1)
        {
            currentGodIndex = 0; // Loop back to zero if the new index exceeds the list count
            SelectGod(allPlayerGods[currentGodIndex]); // Set new god
        }
        
        else
        {
            SelectGod(allPlayerGods[currentGodIndex]);
        }
    }
    
    private void ClickSelect()
    {
        //consideration for ortho camera here

        Ray ray;

        if (currentCam == cam)
        { 
            ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
        else
        {
            Vector3 scrPoint = new Vector3(Mouse.current.position.ReadValue().x,Mouse.current.position.ReadValue().y, 0); 
            ray = currentCam.ScreenPointToRay(scrPoint); 
        }

        // Return position of mouse click on screen. If it clicks a god, set that as currently selected god. otherwise, move current god
        // if (!EventSystem.current.IsPointerOverGameObject()) // to ignore UI
        // {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject objectHit = hit.collider.gameObject;
                
                Debug.Log("<color=yellow> Click Select: Raycast complete. Returning object: </color>" + hit.collider.gameObject);

                GodBehaviour godHit = objectHit.GetComponentInParent<GodBehaviour>();

                if (godHit != null)
                {
                    Debug.Log("<color=green> Click Select: God found </color>");
                    SelectGod(godHit);
                    Debug.Log("<color=green> Click Select: Selected god: </color>" + godHit.gameObject.name);
                }
                
                if (currentlySelectedGod != null)
                {
                    Debug.Log("<color=green> Click to Move: Selected position for player </color>");
                    currentlySelectedGod.lastClickedPosition = hit.point;
                    // currentlySelectedGod.navMeshAgent.isStopped = false;
                    currentlySelectedGod.SwitchState(GodState.moveToArea);
                    lD.SetEndPos(hit.point);
                }
            }
        //}
    }

    public void SelectGod(GodBehaviour godToSelect)
    {
        godSelected = true;
        currentlySelectedGod = godToSelect;
        Debug.Log("selected god : "+currentlySelectedGod.godName);
        currentlySelectedGod.ToggleSelection(true);
        
        //uiManager.UpdateCurrentGodText();
        
        
        InterimUIManager.Instance.UpdateHUD(currentlySelectedGod);
        
    }

    public void DeselectGod()
    {
        if (currentlySelectedGod != null)
        {
            godSelected = false;
            currentlySelectedGod.ToggleSelection(false);
            currentlySelectedGod = null;
        
           // uiManager.UpdateCurrentGodText();
        }
    }

    public void AddRespect(int valueToAdd)
    {
        currentRespect += valueToAdd;
        
        //uiManager.UpdateCurrentGodText();

        respectDisplay.text = respectText  + currentRespect;

        CheckForSummon();
    }
    
    public void RemoveRespect(int valueToRemove)
    {
        int newValue = currentRespect - valueToRemove;

        if (newValue > 0)
        {
            currentRespect = newValue;
           // uiManager.UpdateCurrentGodText();
        }
        
        if (newValue <= 0)
        {
            currentRespect = 0;
           // uiManager.UpdateCurrentGodText();
        }
        respectDisplay.text = respectText + currentRespect;
        //CheckForSummon();
    }

    public void CheckForSummon()
    {
        if (currentRespect >= summonRespectThreshold)
        {
            canSummon = true;
        }
        else
        {
            canSummon = false;
        }

        if (canSummon)
        {
            //turn on UI summon option
        }
        
    }

    public void SwitchCam(Camera cameraToChangeTo)
    {
        currentCam = cameraToChangeTo;
    }

    public void SetPlayerGods(List<GodBehaviour> godstoSet)
    {
        allPlayerGods = godstoSet;
    }
}
