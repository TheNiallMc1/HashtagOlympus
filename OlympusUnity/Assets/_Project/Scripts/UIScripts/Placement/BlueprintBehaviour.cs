using UnityEngine;
using UnityEngine.InputSystem;

public class BlueprintBehaviour : MonoBehaviour
{
    public GameObject godGO;

    public Camera _cam;
    private RaycastHit _hit;
    private Vector3 _movePoint;

    private PlayerInput _playerControls;

    private Vector3 _newPosition;


    private void Awake()
    {
        _playerControls = new PlayerInput();
        _playerControls.Enable();

        _playerControls.Player.MouseClick.performed += ctx => SetDown();
    }

    private void Start()
    {
        _cam = Camera.main;

        if (!(_cam is null))
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out _hit, 50000.0f, 1 << 11))
            {
                _newPosition = _hit.point;
            }
        }
    }

    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        // if (Physics.Raycast(ray, out _hit, 50000.0f, (1 << 11)))
        if (Physics.Raycast(ray, out _hit))
        {
            _newPosition = new Vector3(_hit.point.x, 0f, _hit.point.z);
            transform.position = _newPosition;
        }
    }

    private void SetDown()
    {
        _newPosition.y = 0;

        godGO = PlacementManager.Instance.ReturnCurrentGod();
        godGO.transform.SetParent(null);
        godGO.transform.position = new Vector3(_newPosition.x, 0, _newPosition.z);


        if (gameObject != null)
        {
            _playerControls.Disable();
            PlacementManager.Instance.IncreaseCount();
            Destroy(gameObject);
        }
    }
}