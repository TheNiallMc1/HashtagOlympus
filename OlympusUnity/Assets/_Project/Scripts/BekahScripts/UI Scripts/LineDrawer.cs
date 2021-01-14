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
    private Ray ray;
    
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
        if (GameManager.Instance.godSelected)
        {
            clickCount++;
            //Debug.Log("cc: "+clickCount);

            //check if 0, Vector3.zero
            //if (clickCount == 2)
            //{
                //Vector3 scrPoint = new Vector3(Mouse.current.position.ReadValue().y,300f, Mouse.current.position.ReadValue().x); 
                //ray = mapCam.ScreenPointToRay(scrPoint); 
                
                _mousePos = mapCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _lineR.SetPosition(0, new Vector3(GameManager.Instance.currentlySelectedGod.gameObject.transform.position.x, 100f, GameManager.Instance.currentlySelectedGod.gameObject.transform.position.z));
                _lineR.SetPosition(1, new Vector3(_mousePos.x, 100f, _mousePos.z));
                //_lineR.SetPosition(1, new Vector3(Mouse.current.position.ReadValue().x,Mouse.current.position.ReadValue().y, 0f));
                _distance = (_mousePos - _startMousePos).magnitude;
                Debug.Log("doing line");
           // }

           /* if (clickCount > 2)
            {
                
                Debug.Log("killing line");
                clickCount = 0;
                _lineR.SetPosition(0, new Vector3(0,0,0));
                _lineR.SetPosition(1, new Vector3(0,0,0));
            }
            */
            
        }
    }
}
