using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private PlayerControls playerControls;
    private Camera cam;
    private Camera currentCam;
    public Camera overViewCam; 
    private UIManager uiManager;
    
    // Gods and God Selection
    public List<GodBehaviour> allPlayerGods;
    public bool godSelected;
    public GodBehaviour currentlySelectedGod;
    private int currentGodIndex;

    public LineDrawer lD;

    public bool posOneSet;
    
    // Respect
    public int currentRespect;
    
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
        cam = Camera.main;
        currentCam = cam;
        posOneSet = false;
        
        // Controls
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Movement.MouseClick.performed += context => ClickSelect();
        playerControls.GodSelection.CycleThroughGods.performed += context => CycleSelect();
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
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("check for raycast");
            bool targetIsGod = hit.collider.CompareTag("God");
            
            Debug.Log(""+hit.point.x+", "+hit.point.y+", "+hit.point.z);
            
            // Select a new god
            if (targetIsGod)
            {
                GodBehaviour thisGod = hit.collider.gameObject.GetComponentInParent<GodBehaviour>();
                SelectGod(thisGod);

                print("Selected: " + thisGod.godName);
            }
            
            if (currentlySelectedGod != null)
            {
                currentlySelectedGod.lastClickedPosition = hit.point;
                
                currentlySelectedGod.SwitchState(GodState.moveToArea);
                lD.SetEndPos(hit.point);
                DeselectGod();
              
            }
        }
    }

    public void SelectGod(GodBehaviour godToSelect)
    {
        Debug.Log("selected god");
        //DeselectGod();
        
        godSelected = true;
        currentlySelectedGod = godToSelect;
        currentlySelectedGod.ToggleSelection(true);
        
        uiManager.UpdateCurrentGodText();
    }

    public void DeselectGod()
    {
        if (currentlySelectedGod != null)
        {
            godSelected = false;
            currentlySelectedGod.ToggleSelection(false);
            currentlySelectedGod = null;
        
            uiManager.UpdateCurrentGodText();
        }
    }

    public void AddRespect(int valueToAdd)
    {
        currentRespect += valueToAdd;
        
        uiManager.UpdateCurrentGodText();
    }
    
    public void RemoveRespect(int valueToRemove)
    {
        int newValue = currentRespect - valueToRemove;

        if (newValue > 0)
        {
            currentRespect = newValue;
            uiManager.UpdateCurrentGodText();
        }
        
        if (newValue <= 0)
        {
            currentRespect = 0;
            uiManager.UpdateCurrentGodText();
        }
    }

    public void SwitchCam(Camera cameraToChangeTo)
    {
        currentCam = cameraToChangeTo;
    }
}
