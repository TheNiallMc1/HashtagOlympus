using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System;

public class CameraController : MonoBehaviour
{
    private Camera freeCam;
    private CameraControls cameraControls;
    [SerializeField] GameObject ball;

    public enum cameraMode { Free, Follow, Transition};
 
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float movementTime = 2f;

    [SerializeField] private float rotationAmount = 10f;
    [SerializeField] private float rotationTime = 2f;

    private Vector3 zoomAmount;
    public Vector3 newZoom;
    public Vector3 maxZoomOut;
    public Vector3 maxZoomIn;
    
    private Vector3 newPosition;
    private Quaternion newRotation;

    private bool movingCameraUp;
    private bool movingCameraDown;
    private bool movingCameraLeft;
    private bool movingCameraRight;

    public Vector3 offset;

    private bool rotatingCameraLeft;
    private bool rotatingCameraRight;

    public float mouseScrollY;

    public float camSwitchDuration = 0.8f;


    public bool freeCamActive = true;
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
        // Look I am aware this is ugly as all hell but the new input system is really awkward with detecting holding down inputs

        //switch (cameraMode)
        //{
        //    case 
        //}

        if (!freeCamActive)
        {
            transform.position = currentPlayer.transform.position;
            newPosition = currentPlayer.transform.position;
        }

        MoveCamera();
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

        RotateCamera();
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);

        MouseScroll();


        // ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

    }

    void MoveCamera()
    {
        if (movingCameraUp)
        {
            newPosition += (transform.forward * movementSpeed);
            ReleaseCamera();

            // cameraMovementDetected = true;
            //freeCamActive = true;
            //SwitchToFreeCam();
        }

        if (movingCameraDown)
        {
            newPosition += (transform.forward * -movementSpeed);
            ReleaseCamera();

            // cameraMovementDetected = true;
            // freeCamActive = true;
            //SwitchToFreeCam();
        }

        if (movingCameraLeft)
        {
            newPosition += (transform.right * -movementSpeed);
            ReleaseCamera();

            // cameraMovementDetected = true;
            //freeCamActive = true;
            //SwitchToFreeCam();
        }

        if (movingCameraRight)
        {
            newPosition += (transform.right * movementSpeed);
            ReleaseCamera();

            // cameraMovementDetected = true;
            //freeCamActive = true;
            //SwitchToFreeCam();
        }
    }

    void RotateCamera()
    {
        if (rotatingCameraLeft)
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
            // cameraMovementDetected = true;
        }

        if (rotatingCameraRight)
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
            // cameraMovementDetected = true;
        }
    }

    void MouseScroll()
    {
        if(mouseScrollY > 0)
        {
            zoomAmount = new Vector3(0, -mouseScrollY / 45, mouseScrollY / 30);
            if (newZoom.y > maxZoomIn.y || newZoom.z < maxZoomIn.z)
            {
                newZoom += zoomAmount;
            }
        }

        if (mouseScrollY < 0)
        {
            zoomAmount = new Vector3(0, mouseScrollY / 45, -mouseScrollY / 30);
            if(newZoom.y < maxZoomOut.y || newZoom.z > maxZoomOut.z)
            {
                newZoom -= zoomAmount;
            }
        }

        freeCam.transform.localPosition = Vector3.Lerp(freeCam.transform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    
    public void MoveToSelected(Vector3 selectedPosition)
    {

        Vector3 testPosition = new Vector3(1185, 39, 291);
        offset = new Vector3(1.4f, 125.2f, 45.3f);
        newPosition = selectedPosition + offset;
        // ZoomIn();

        print("moving to " + newPosition);
    }


    public void FollowPlayer(GameObject player)
    {
        // freeCam.m_Follow = player.transform;
        // freeCam.m_LookAt = player.transform;

        StartCoroutine(FollowPlayerRoutine(player));


    }

    IEnumerator FollowPlayerRoutine(GameObject player)
    {
        currentPlayer = player;
        Vector3 originalPosition = transform.position;
        float timeElapsed = 0;

        while(timeElapsed < camSwitchDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, currentPlayer.transform.position, timeElapsed / camSwitchDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = currentPlayer.transform.position;
        freeCamActive = false;
    }

    public void ReleaseCamera()
    {
        if(currentPlayer != null)
        {
            currentPlayer = null;
        }

        // freeCam.transform.position = maxZoomIn;
        freeCamActive = true;
        // freeCam.m_Follow = null;
        // freeCam.m_LookAt = null;
    }













    //public void MoveToPlayerCharacter(GameObject player)
    //{
    //    // Deactivate other cameras
    //    if (currentPlayer != null)
    //    {
    //        currentPlayer.GetComponentInChildren<CinemachineVirtualCamera>().m_Priority = 0;
    //    }
    //    freeCam.m_Priority = 0;
    //    freeCamActive = false;

    //    // Activate new player camera
    //    player.GetComponentInChildren<CinemachineVirtualCamera>().m_Priority = 1;

    //    // Update current player
    //    currentPlayer = player;

    //    // Lock free cam controls
    //}

    //public void SwitchToFreeCam()
    //{
    //    // Reset bool
    //   //  cameraMovementDetected = false;
        
    //    // Update free cam position
    //    // freeCam.transform.position = currentPlayer.GetComponentInChildren<CinemachineVirtualCamera>().transform.position;

    //    // Deactivate current player cam
    //    //if (currentPlayer != null)
    //    //{
    //    //    currentPlayer.GetComponentInChildren<CinemachineVirtualCamera>().m_Priority = 0;
    //    //}

    //    //currentPlayer = null;
    //    //// Acitvate free cam
    //    //freeCam.m_Priority = 1;

    //    //freeCamActive = true;
    //    // Debug.Break();
    //}




}
