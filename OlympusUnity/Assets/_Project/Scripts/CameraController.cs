﻿using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System;

public class CameraController : MonoBehaviour
{
    private Camera freeCam;
    private CameraControls cameraControls;

    public enum cameraMode { Free, Follow, Transition};
    public cameraMode camMode;
 
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float movementTime = 2f;

    [SerializeField] private float rotationAmount = 10f;
    [SerializeField] private float rotationTime = 2f;

    private Vector3 zoomAmount;
    public Vector3 newZoom;
    public Vector3 maxZoomOut;
    public Vector3 maxZoomIn;
    public Vector3 offset;

    private Vector3 newPosition;
    private Quaternion newRotation;

    public bool movingCameraActive;

    private bool movingCameraUp;
    private bool movingCameraDown;
    private bool movingCameraLeft;
    private bool movingCameraRight;
    
    private bool rotatingCameraLeft;
    private bool rotatingCameraRight;

    public float mouseScrollY;

    public float camSwitchSpeed = 1f;

    // min/max is the lowest/largest distance used for camera speed transition calculations
    public float minCamSpeed = 0.2f;
    public float maxCamSpeed = 2.5f;

    public float yZoomAdjust = 45;
    public float zZoomAdjust = 30;
    
    public GameObject currentPlayer = null;
    public GameObject lastPlayer = null;

    private static CameraController _instance = null; // the private static singleton instance variable
    public static CameraController Instance { get { return _instance; } } // public getter property, anyone can access it!
    

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

        cameraControls = new CameraControls();
        cameraControls.Enable();

        cameraControls.Camera.MoveCameraUp.started += ctx => movingCameraUp = true;
        cameraControls.Camera.MoveCameraDown.started += ctx => movingCameraDown = true;
        cameraControls.Camera.MoveCameraLeft.started += ctx => movingCameraLeft = true;
        cameraControls.Camera.MoveCameraRight.started += ctx => movingCameraRight = true;
        cameraControls.Camera.RotateCameraLeft.started += ctx => rotatingCameraLeft = true;
        cameraControls.Camera.RotateCameraRight.started += ctx => rotatingCameraRight = true;
        
        cameraControls.Camera.MouseScrollY.performed += ctx => mouseScrollY = ctx.ReadValue<float>();

        cameraControls.Camera.MoveCameraUp.canceled += ctx => movingCameraUp = false;
        cameraControls.Camera.MoveCameraDown.canceled += ctx => movingCameraDown = false;
        cameraControls.Camera.MoveCameraLeft.canceled += ctx => movingCameraLeft = false;
        cameraControls.Camera.MoveCameraRight.canceled += ctx => movingCameraRight = false;
        cameraControls.Camera.RotateCameraLeft.canceled += ctx => rotatingCameraLeft = false;
        cameraControls.Camera.RotateCameraRight.canceled += ctx => rotatingCameraRight = false;

    }

    private void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;

        freeCam = GetComponentInChildren<Camera>();
        newZoom = freeCam.transform.localPosition;
    }

    private void Update()
    {
       
        if(currentPlayer != null && movingCameraActive)
        {
            camMode = cameraMode.Free;
            lastPlayer = currentPlayer;
            currentPlayer = null;
        }

        movingCameraActive = false;

        switch (camMode)
        {
            case cameraMode.Free:
                
                MoveCamera();
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

                RotateCamera();
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);

                MouseScroll();
                break;

            case cameraMode.Follow:
                MoveCamera();
                if(currentPlayer != null)
                {
                    transform.position = currentPlayer.transform.position;
                    newPosition = currentPlayer.transform.position;
                }

                RotateCamera();
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);

                MouseScroll();

                break;

            case cameraMode.Transition:
                MoveCamera();
                break;
        }

    }

    void MoveCamera()
    {
        if (movingCameraUp)
        {
            newPosition += (transform.forward * movementSpeed);
            ReleaseCamera();
            movingCameraActive = true;
        }

        if (movingCameraDown)
        {
            newPosition += (transform.forward * -movementSpeed);
            ReleaseCamera();
            movingCameraActive = true;
        }

        if (movingCameraLeft)
        {
            newPosition += (transform.right * -movementSpeed);
            ReleaseCamera();
            movingCameraActive = true;
        }

        if (movingCameraRight)
        {
            newPosition += (transform.right * movementSpeed);
            ReleaseCamera();
            movingCameraActive = true;
        }
    }

    void RotateCamera()
    {
        if (rotatingCameraLeft)
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (rotatingCameraRight)
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
    }

    void MouseScroll()
    {
        if(mouseScrollY > 0)
        {
            zoomAmount = new Vector3(0, -mouseScrollY / yZoomAdjust, mouseScrollY / zZoomAdjust);
            if (newZoom.y > maxZoomIn.y || newZoom.z < maxZoomIn.z)
            {
                newZoom += zoomAmount + offset;
            }
        }

        if (mouseScrollY < 0)
        {
            zoomAmount = new Vector3(0, mouseScrollY / yZoomAdjust, -mouseScrollY / zZoomAdjust);
            if(newZoom.y < maxZoomOut.y || newZoom.z > maxZoomOut.z)
            {
                newZoom -= zoomAmount - offset;
            }
        }

        freeCam.transform.localPosition = Vector3.Lerp(freeCam.transform.localPosition, newZoom, Time.deltaTime * movementTime);
    }


    // Called when the a player icon is clicked to move over to that position
    public void FollowPlayer(GameObject player)
    {
        lastPlayer = currentPlayer;
        currentPlayer = player;
        if (lastPlayer == null || currentPlayer != lastPlayer)
        {
            camMode = cameraMode.Transition;
        }

        StartCoroutine(FollowPlayerRoutine(player));

    }

    IEnumerator FollowPlayerRoutine(GameObject player)
    {
        Vector3 originalPosition = transform.position;
        float timeElapsed = 0;

        float distanceToMove = Vector3.Distance(originalPosition, currentPlayer.transform.position);
        float duration = distanceToMove * camSwitchSpeed;
        
        duration = Mathf.Clamp(duration, minCamSpeed, maxCamSpeed);

        while (timeElapsed < duration)
        {
            if(currentPlayer == null)
            {
                newPosition = transform.position;
                camMode = cameraMode.Free;

                yield break;
            }
            transform.position = Vector3.Lerp(originalPosition, currentPlayer.transform.position, timeElapsed * duration);
            timeElapsed += Time.deltaTime;

            if (transform.position == currentPlayer.transform.position)
            {
                break;
            }

            yield return null;
        }

        // transform.position = currentPlayer.transform.position;
        camMode = cameraMode.Follow;
    }

    // To move back to free roam
    public void ReleaseCamera()
    {

        if (currentPlayer != null)
        {
            lastPlayer = currentPlayer;
            currentPlayer = null;
        }
        camMode = cameraMode.Free;
    }




    // Polish

    // Use distance as a variable for camera transition speed (clamp speed) DONE
    // Acceleration for camera transition
    // Camera trailing behind player in follow mode
    // Camera always centered on the screen in free mode




    



}
