using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer _lineR;

    private PlayerControls playerControls;

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
        playerControls.Movement.LeftMouseClick.performed += context => SetMousePos();
    
    }
    
    void SetMousePos()
    {
        //clickCount++;

        //if (clickCount == 2)
        //{
        //    _lineR.SetPosition(0, new Vector3(0,0,0));
        //    _lineR.SetPosition(1, new Vector3(0, 0, 0));
        //}
    }

    public void SetEndPos(Vector3 endPos)
    {
        if (GameManager.Instance.godSelected)
        {
            Debug.Log("receiving coords");
            _lineR.SetPosition(0,
                new Vector3(GameManager.Instance.currentlySelectedGod.gameObject.transform.position.x, 100f,
                    GameManager.Instance.currentlySelectedGod.gameObject.transform.position.z));
            _lineR.SetPosition(1,
                new Vector3(endPos.x, 100f, endPos.z));

            clickCount = 0;
        }
    }
}
