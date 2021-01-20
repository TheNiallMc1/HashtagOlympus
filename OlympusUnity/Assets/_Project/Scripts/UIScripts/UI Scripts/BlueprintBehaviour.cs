﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlueprintBehaviour : MonoBehaviour
{
    public GameObject prefab;
    
    private Camera _cam;
    private RaycastHit _hit;
    private Vector3 _movePoint;

    private PlayerInput _playerControls;

    private Vector3 _newPosition;


    void Awake()
    {
        _playerControls = new PlayerInput();
        _playerControls.Enable();

        _playerControls.Player.MouseClick.performed += ctx => SetDown();
        // prefab = BekahsGM.Instance.currentGods[ImprovedPlacementManager.Instance.currentGodIndex];
    }

    private void Start()
    {
        _cam = Camera.main;
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hit,50000.0f,(1<<8)))
        {
            _newPosition = _hit.point;
        }
    }

    private void Update()
    {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out _hit, 50000.0f, (1 << 8)))
            {
                _newPosition = new Vector3(_hit.point.x, 0f, _hit.point.z);
                transform.position = _newPosition;
            }
        
    }

    public void SetDown()
    {
        
        _newPosition.y = 0;
            Debug.Log("Instantiating ");
            //Instantiate(prefab, _hit.point, transform.rotation);
            prefab.transform.position = _newPosition;
            
          
            if (gameObject != null)
            {
               // _playerControls.Player.MouseClick.performed -= ctx => SetDown();
                _playerControls.Disable();
                Destroy(gameObject);
            }
    }
    
    /*
   public void GetSelection(int choice)
    {
        Debug.Log("Selection recieved : "+choice);
        prefab = BekahsGM.Instance.currentGods[choice];
        _canPlace = true;
    }
    */
    
}