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
    
    // God Selection
    private bool godSelected;
    private GodBehaviour currentlySelectedGod;
    

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
            // Select a new god
            if (!godSelected && hit.collider.CompareTag("God"))
            {
                currentlySelectedGod = hit.collider.gameObject.GetComponentInParent<GodBehaviour>();
                
                godSelected = true;
                currentlySelectedGod.ToggleSelection(godSelected);
                
                print("Selected: " + currentlySelectedGod.godName);
            }
            
            // Move the currently selected god
            if (godSelected && !hit.collider.CompareTag("God"))
            {
                currentlySelectedGod.ToggleSelection(godSelected);
                currentlySelectedGod.MoveToTarget(hit.point);
                
                print("Moved: " + currentlySelectedGod.godName);

                godSelected = false;
                currentlySelectedGod.ToggleSelection(godSelected);
                currentlySelectedGod = null;
            }
        }
    }  
}
