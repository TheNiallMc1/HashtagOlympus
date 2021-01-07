using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private PlayerControls playerControls;
    private Camera cam;
    private UIManager uiManager;
    
    // Gods and God Selection
    public List<GodBehaviour> allPlayerGods;
    private bool godSelected;
    public GodBehaviour currentlySelectedGod;
    
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
        
        // Controls
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Movement.MouseClick.performed += context => MoveToClick();
    }

    private void MoveToClick()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            bool targetIsGod = hit.collider.CompareTag("God");
            
            // Select a new god
            if (!godSelected && targetIsGod)
            {
                GodBehaviour thisGod = hit.collider.gameObject.GetComponentInParent<GodBehaviour>();
                SelectGod(thisGod);

                print("Selected: " + thisGod.godName);
            }
            
            // Move the currently selected god
            if (godSelected && !targetIsGod)
            {
                currentlySelectedGod.MoveToTarget(hit.point);
                DeselectGod();
            
                print("Moved: " + currentlySelectedGod.godName);
            }
        }
    }

    public void SelectGod(GodBehaviour godToSelect)
    {
        godSelected = true;
        currentlySelectedGod = godToSelect;
        currentlySelectedGod.ToggleSelection(true);
        
        uiManager.UpdateCurrentGodText();
    }

    public void DeselectGod()
    {
        godSelected = false;
        currentlySelectedGod.ToggleSelection(false);
        currentlySelectedGod = null;
        
        uiManager.UpdateCurrentGodText();
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
}
