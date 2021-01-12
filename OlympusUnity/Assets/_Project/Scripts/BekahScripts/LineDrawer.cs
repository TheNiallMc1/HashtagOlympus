using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class LineDrawer : MonoBehaviour
{

    private LineRenderer _lineR;

    private Vector3 _mousePos;

    private Vector3 _startMousePos;

    private float _distance;
    private PlayerControls playerControls;
    public Camera mapCam;
    public int clickCount;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineR = GetComponent<LineRenderer>();
        _lineR.positionCount = 2;
        clickCount = 0;

    }

    private void Awake()
    {
        // Controls
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Movement.MouseClick.performed += context => SetMousePos();
        //playerControls.GodSelection.CycleThroughGods.performed += context => CycleSelect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMousePos()
    {
        clickCount++;

        if (clickCount == 1)
        {
            _startMousePos = mapCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        if (clickCount == 2)
        {
            _mousePos = mapCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _lineR.SetPosition(0, new Vector3(_startMousePos.x, 100f, _startMousePos.z));
            _lineR.SetPosition(1, new Vector3(_mousePos.x,100f, _mousePos.z));
            _distance = (_mousePos - _startMousePos).magnitude;
            Debug.Log("distance: "+_distance);
        }

        if (clickCount > 2)
        {
            clickCount = 0;
            _lineR.SetPosition(0, new Vector3());
            _lineR.SetPosition(1, new Vector3());
        }
    }
}
