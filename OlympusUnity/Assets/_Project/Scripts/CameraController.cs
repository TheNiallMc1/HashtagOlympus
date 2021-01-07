using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera roamingCamera;

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


    private CameraControls cameraControls;

    private bool movingCameraUp;
    private bool movingCameraDown;
    private bool movingCameraLeft;
    private bool movingCameraRight;

    public Vector3 offset;

    private bool rotatingCameraLeft;
    private bool rotatingCameraRight;

    public float mouseScrollY;

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
        newZoom = roamingCamera.transform.localPosition;
    }

    private void Update()
    {
        // Look I am aware this is ugly as all hell but the new input system is really awkward with detecting holding down inputs
        MoveCamera();
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

        RotateCamera();
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);

        MouseScroll();
    }

    void MoveCamera()
    {
        if (movingCameraUp)
        {
            newPosition += (transform.forward * movementSpeed);
        }

        if (movingCameraDown)
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if (movingCameraLeft)
        {
            newPosition += (transform.right * -movementSpeed);
        }

        if (movingCameraRight)
        {
            newPosition += (transform.right * movementSpeed);
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
            zoomAmount = new Vector3(0, -mouseScrollY / 45, mouseScrollY / 30);
            if (newZoom.y > maxZoomIn.y || newZoom.z < maxZoomIn.z)
            {
                newZoom += zoomAmount;
                print("zooming in");
            }
            // newZoom += zoomAmount;
        }

        if (mouseScrollY < 0)
        {
            zoomAmount = new Vector3(0, mouseScrollY / 45, -mouseScrollY / 30);
            if(newZoom.y < maxZoomOut.y || newZoom.z > maxZoomOut.z)
            {
                newZoom -= zoomAmount;
                print("zooming out");
            }
            // newZoom -= zoomAmount;
        }

        roamingCamera.transform.localPosition = Vector3.Lerp(roamingCamera.transform.localPosition, newZoom, Time.deltaTime * movementTime);





            // roamingCamera.transform.localPosition += new Vector3(0, 0, mouseScrollY * zoomSpeed / 120);

            // zoomChange = new Vector3(mouseScrollY * zoomSpeed / 120, 0, mouseScrollY * zoomSpeed / 120);


        //    if (mouseScrollY > 0)
        //{
        //    Debug.Log("Scrolled Up");
        //    roamingCamera.transform.position = new Vector3(transform.position.x, transform.position.y - .3f, transform.position.z + .2f);
        //}

        //if(mouseScrollY < 0)
        //{
        //    Debug.Log("Scrolled Down");
        //    roamingCamera.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z - .2f);
        //}


        //roamingCamera.transform.localPosition = Vector3.Lerp
        //(roamingCamera.transform.localPosition,
        //newZoom,
        //Time.deltaTime * movementTime * mouseScrollY / 240);
    }



    public void MoveToSelected(Vector3 selectedPosition)
    {

        Vector3 testPosition = new Vector3(1185, 39, 291);
        offset = new Vector3(1.4f, 125.2f, 45.3f);
        newPosition = selectedPosition + offset;
        // ZoomIn();

        print("moving to " + newPosition);
    }
}
