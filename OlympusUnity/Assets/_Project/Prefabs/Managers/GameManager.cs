using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private PlayerControls playerControls;
    private Camera cam;
    public Camera currentCam;
    public Camera overViewCam;

    // Gods and God Selection
    public List<GodBehaviour> allPlayerGods;
    public bool godSelected;
    public GodBehaviour currentlySelectedGod;
    private int currentGodIndex;
    public Dictionary<int, GodBehaviour> godDict;

    public LayerMask moveRayMask;
    public LineDrawer lD;

    // Respect
    public int currentRespect;
    public TMP_Text respectDisplay;
    public String respectText;
    public int summonRespectThreshold;
    private bool canSummon;

    private void Awake()
    {
        // Creating singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        cam = Camera.main;
        currentCam = cam;

        // Controls
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Movement.LeftMouseClick.performed += context => ClickSelect();
        playerControls.Movement.RightMouseClick.performed += context => MoveGod();
        playerControls.GodSelection.CycleThroughGods.performed += context => CycleSelect();

        canSummon = false;
    }

    private void Start()
    {
        respectText = respectDisplay.text + " ";
        respectDisplay.text = respectText + currentRespect;
        PopulateAllPlayerGods();
    }

    public void PopulateAllPlayerGods()
    {
        if (UberManager.Instance.selectedGods.Count == 3)
        {
            allPlayerGods = UberManager.Instance.selectedGods;
            GodListToDictionary();
        }
    }

    private void GodListToDictionary()
    {
        godDict = new Dictionary<int, GodBehaviour>();

        for (int i = 0; i < allPlayerGods.Count; i++)
        {
            godDict.Add(i, allPlayerGods[i]);
        }

        InterimUIManager.Instance.AssignCharacterDocks(godDict);
    }

    private void CycleSelect()
    {
        currentGodIndex += 1;

        if (currentGodIndex > allPlayerGods.Count - 1)
        {
            currentGodIndex = 0; // Loop back to zero if the new index exceeds the list count
            SelectGod(allPlayerGods[currentGodIndex]); // Set new god
        }

        else
        {
            SelectGod(allPlayerGods[currentGodIndex]);
        }
    }

    private void ClickSelect()
    {
        //consideration for ortho camera here

        Ray ray;

        if (currentCam == cam)
        {
            ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
        else
        {
            Vector3 scrPoint = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y,
                0);
            ray = currentCam.ScreenPointToRay(scrPoint);
        }

        // Return position of mouse click on screen. If it clicks a god, set that as currently selected god. otherwise, move current god
        if (!EventSystem.current.IsPointerOverGameObject()) // to ignore UI
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject objectHit = hit.collider.gameObject;

                GodBehaviour godHit = objectHit.GetComponentInParent<GodBehaviour>();

                if (godHit != null)
                {
                    SelectGod(godHit);
                }
            }
        }
    }

    private void MoveGod()

    {
        if (currentlySelectedGod == null)
        {
            return;
        }


        Ray ray;

        if (currentCam == cam)
        {
            ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
        else
        {
            Vector3 scrPoint = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y,
                0);

            ray = currentCam.ScreenPointToRay(scrPoint);
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, moveRayMask))

        {
            currentlySelectedGod.lastClickedPosition = new Vector3(hit.point.x, 0, hit.point.z);
            currentlySelectedGod.SwitchState(GodState.moveToArea);
            lD.SetEndPos(hit.point);
        }
    }

    public void SelectGod(GodBehaviour godToSelect)
    {
        if (currentlySelectedGod != null)
        {
            currentlySelectedGod.selectionCircle.SetActive(false);
            currentlySelectedGod.mouseDetectorCollider.SetActive(true);
        }

        godSelected = true;
        currentlySelectedGod = godToSelect;

        currentlySelectedGod.selectionCircle.SetActive(true);
        currentlySelectedGod.mouseDetectorCollider.SetActive(false);

        currentlySelectedGod.ToggleSelection(true);

        InterimUIManager.Instance.UpdateHUD(currentlySelectedGod);
    }

    public void DeselectGod()
    {
        if (currentlySelectedGod == null)
        {
            return;
        }

        currentlySelectedGod.selectionCircle.SetActive(false);
        currentlySelectedGod.mouseDetectorCollider.SetActive(true);
    }

    public void AddRespect(int valueToAdd)
    {
        currentRespect += valueToAdd;
        respectDisplay.text = respectText + currentRespect;

        CheckForSummon();
    }

    public void RemoveRespect(int valueToRemove)
    {
        int newValue = currentRespect - valueToRemove;

        if (newValue > 0)
        {
            currentRespect = newValue;
            respectDisplay.text = respectText + currentRespect;
        }

        if (newValue <= 0)
        {
            currentRespect = 0;
            respectDisplay.text = respectText + currentRespect;
        }

        respectDisplay.text = respectText + currentRespect;
    }

    private void CheckForSummon()
    {
        canSummon = currentRespect >= summonRespectThreshold;
    }

    public void SwitchCam(Camera cameraToChangeTo)
    {
        currentCam = cameraToChangeTo;
    }

    public void SetPlayerGods(List<GodBehaviour> godstoSet)
    {
        allPlayerGods = godstoSet;
    }
}