using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    private static PlacementManager _instance = null; // the private static singleton instance variable
    public static PlacementManager Instance { get { return _instance; } } // public getter property, anyone can access it!
   // public List<GameObject> godPrefabs;
    public GameObject currentSelection;
    
    private Camera _cam;
    private RaycastHit _hit;
    private Vector3 _movePoint;

    private PlayerInput _playerControls;

    private Vector3 _newPosition;

    private bool _canPlace = false;
    


    void OnDestroy()
    {
        if (_instance == this)
        {
            // if the this is the singleton instance that is being destroyed...
            _instance = null; // set the instance to null
        }
    }


    void Awake()
    {
        if (_instance == null)
        {
            // if the singleton instance has not yet been initialized
            _instance = this;
        }
        else
        {
            // the singleton instance has already been initialized
            if (_instance != this)
            {
                // if this instance of GameManager is not the same as the initialized singleton instance, it is a second instance, so it must be destroyed!
                Destroy(gameObject); // watch out, this can cause trouble!
            }
        }
        
        _playerControls = new PlayerInput();
        _playerControls.Enable();

        _playerControls.Player.MouseClick.performed += ctx => SetDown();
       
    }

    private void Start()
    {
        _cam = Camera.main;
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hit))
        {
            _newPosition = _hit.point;
        }
    }

    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hit))
        {
            _newPosition = _hit.point;
        }

      
    }

    public void SetDown()
    {
        if (_canPlace)
        {
            Debug.Log("Instantiating ");
            Instantiate(currentSelection, _hit.point, transform.rotation);
            //Destroy(gameObject);
            _canPlace = false;
        }

    }
   public void GetSelection(int choice)
    {
        currentSelection = BekahsGM.Instance.currentGods[choice];
        _canPlace = true;
    }
    
}
