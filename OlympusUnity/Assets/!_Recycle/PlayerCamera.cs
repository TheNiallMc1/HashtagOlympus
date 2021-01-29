using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // [SerializeField] CinemachineVirtualCamera freeCam;

    //private CameraControls cameraControls;

    //private bool movingCameraUp;
    //private bool isCamActive = true;

    //private void Awake()
    //{
    //    cameraControls = new CameraControls();
    //    cameraControls.Enable();

    //    cameraControls.Camera.MoveCameraUp.started += ctx => movingCameraUp = true;
    //    cameraControls.Camera.MoveCameraUp.canceled += ctx => movingCameraUp = false;
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (movingCameraUp && freeCam.GetComponentInParent<CameraController>().currentPlayer == transform.parent.gameObject && isCamActive)
    //    {
    //        freeCam.transform.position = transform.position;

    //        isCamActive = false;
    //    }
    //}


}
