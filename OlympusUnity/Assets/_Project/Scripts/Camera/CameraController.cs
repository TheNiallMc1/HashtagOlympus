using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera freeCam;
    private CameraControls cameraControls;

    public enum cameraMode { Free, Follow, Transition};
    public cameraMode camMode;
 
    [SerializeField] private float movementSpeed = 0.2f;
    [SerializeField] private float movementTime = 10f;

    [SerializeField] private float rotationAmount = 0.7f;
    [SerializeField] private float rotationTime = 6f;

    private Vector3 zoomAmount;
    public Vector3 newZoom;
    public Vector3 maxZoomOut = new Vector3(11, 58, -70);
    public Vector3 maxZoomIn = new Vector3(9, 24, -20);

    private Vector3 newPosition;
    private Quaternion newRotation;

    public bool movingCameraActive;

    private bool movingCameraUp;
    private bool movingCameraDown;
    private bool movingCameraLeft;
    private bool movingCameraRight;
    
    private bool rotatingCameraLeft;
    private bool rotatingCameraRight;

    private bool quickFocus;

    public float mouseScrollY;

    public float camSwitchSpeed = 1f;

    // min/max is the lowest/largest distance used for camera speed transition calculations
    public float minCamDuration = 0.5f;
    public float maxCamDuration = 1.2f;

    public float yZoomAdjust = 45;
    public float zZoomAdjust = 40;

    public float followSpeed = 10f;
    
    public GodBehaviour currentPlayer;
    public GodBehaviour lastPlayer;
    [SerializeField] private Vector2 heightLimit;
    [SerializeField] private Vector2 lenghtLimit;
    [SerializeField] private Vector2 widthLimit;
    [SerializeField] private Vector2 lLimit;
    [SerializeField] private Vector2 wLimit;
    [SerializeField] private bool regionBoundary;

    public static CameraController Instance { get; private set; } // public getter property, anyone can access it!


    private void OnDestroy()
    {
        if (Instance == this)
        {
            // if the this is the singleton instance that is being destroyed...
            Instance = null; // set the instance to null
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            // if the singleton instance has not yet been initialized
            Instance = this;
        }
        else
        {
            // the singleton instance has already been initialized
            if (Instance != this)
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
        cameraControls.Camera.QuickFocus.started += ctx => quickFocus = true;
        
        cameraControls.Camera.MouseScrollY.performed += ctx => mouseScrollY = ctx.ReadValue<float>();

        cameraControls.Camera.MoveCameraUp.canceled += ctx => movingCameraUp = false;
        cameraControls.Camera.MoveCameraDown.canceled += ctx => movingCameraDown = false;
        cameraControls.Camera.MoveCameraLeft.canceled += ctx => movingCameraLeft = false;
        cameraControls.Camera.MoveCameraRight.canceled += ctx => movingCameraRight = false;
        cameraControls.Camera.RotateCameraLeft.canceled += ctx => rotatingCameraLeft = false;
        cameraControls.Camera.RotateCameraRight.canceled += ctx => rotatingCameraRight = false;
        cameraControls.Camera.QuickFocus.canceled += ctx => quickFocus = false;

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
        if (transform.rotation.y > 45 && transform.rotation.y < 135 ||
            transform.rotation.y < -45 && transform.rotation.y > -135)
        {
            wLimit = lenghtLimit;
            lLimit = widthLimit;
        }
        else
        {
            wLimit = widthLimit;
            lLimit = lenghtLimit;
        }
        
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

                QuickFocus();

                break;

            case cameraMode.Follow:
                MoveCamera();
                if(currentPlayer != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPlayer.transform.position, Time.deltaTime * followSpeed);
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


    private void MoveCamera()
    {
        if (movingCameraUp)
        {
            //newPosition += (transform.forward * movementSpeed);
            var forward = transform.forward;
            newPosition.x = Mathf.Clamp(newPosition.x + (forward.x * movementSpeed), wLimit.x, wLimit.y);
            newPosition.z = Mathf.Clamp(newPosition.z + (forward.z * movementSpeed), lLimit.x, lLimit.y);
            ReleaseCamera();
            movingCameraActive = true;
        }

        if (movingCameraDown)
        {
            //newPosition += (transform.forward * -movementSpeed);
            var forward = transform.forward;
            newPosition.x = Mathf.Clamp(newPosition.x + (forward.x * -movementSpeed), wLimit.x, wLimit.y);
            newPosition.z = Mathf.Clamp(newPosition.z + (forward.z * -movementSpeed), lLimit.x, lLimit.y);
            ReleaseCamera();
            movingCameraActive = true;
        }

        if (movingCameraLeft)
        {
            //newPosition += (transform.right * -movementSpeed);
            var right = transform.right;
            newPosition.x = Mathf.Clamp(newPosition.x + (right.x * -movementSpeed), wLimit.x, wLimit.y);
            newPosition.z = Mathf.Clamp(newPosition.z + (right.z * -movementSpeed), lLimit.x, lLimit.y);
            ReleaseCamera();
            movingCameraActive = true;
        }

        if (movingCameraRight)
        {
            //newPosition += (transform.right * movementSpeed);
            var right = transform.right;
            newPosition.x = Mathf.Clamp(newPosition.x + (right.x * movementSpeed), wLimit.x, wLimit.y);
            newPosition.z = Mathf.Clamp(newPosition.z + (right.z * movementSpeed), lLimit.x, lLimit.y);
            ReleaseCamera();
            movingCameraActive = true;
        }
    }

    private void RotateCamera()
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

    private void MouseScroll()
    {
        if(mouseScrollY > 0)
        {
            zoomAmount = new Vector3(0, -mouseScrollY / yZoomAdjust, mouseScrollY / zZoomAdjust);
            if (newZoom.y > maxZoomIn.y || newZoom.z < maxZoomIn.z)
            {
                newZoom += zoomAmount;
            }
        }

        if (mouseScrollY < 0)
        {
            zoomAmount = new Vector3(0, mouseScrollY / yZoomAdjust, -mouseScrollY / zZoomAdjust);
            if(newZoom.y < maxZoomOut.y || newZoom.z > maxZoomOut.z)
            {
                newZoom -= zoomAmount;
            }
        }

        freeCam.transform.localPosition = Vector3.Lerp(freeCam.transform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    private void QuickFocus()
    {
        if (quickFocus)
        {
            FollowPlayer(lastPlayer);
        }
    }


    // Called when the a player icon is clicked to move over to that position
    public void FollowPlayer(GodBehaviour player)
    {
        lastPlayer = currentPlayer;
        currentPlayer = player;
        if (lastPlayer == null || currentPlayer != lastPlayer)
        {
            camMode = cameraMode.Transition;
        }

        lastPlayer = null;
        StartCoroutine(FollowPlayerRoutine());

    }

    private IEnumerator FollowPlayerRoutine()
    {
        Vector3 originalPosition = transform.position;
        float timeElapsed = 0;

        float distanceToMove = Vector3.Distance(originalPosition, currentPlayer.transform.position);
        float duration = distanceToMove * camSwitchSpeed;
        
        duration = Mathf.Clamp(duration, minCamDuration, maxCamDuration);

        while (timeElapsed < duration)
        {
            if(currentPlayer == null)
            {
                newPosition = transform.position;
                camMode = cameraMode.Free;

                yield break;
            }
            transform.position = Vector3.Lerp(originalPosition, currentPlayer.transform.position, Mathf.SmoothStep(0.0f, 1.0f, timeElapsed * duration));
            timeElapsed += Time.deltaTime;

            if (transform.position == currentPlayer.transform.position)
            {
                break;
            }

            yield return null;
        }
        
        camMode = cameraMode.Follow;
    }

    // To move back to free roam
    private void ReleaseCamera()
    {

        if (currentPlayer != null)
        {
            lastPlayer = currentPlayer;
            currentPlayer = null;
        }
        camMode = cameraMode.Free;
    }


}
