using System;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Gods and God Selection")] public List<GodBehaviour> allPlayerGods;
    public bool godSelected;
    public GodBehaviour currentlySelectedGod;
    private int currentGodIndex;
    public Dictionary<int, GodBehaviour> godDict;

    public LayerMask moveRayMask;
    public LineDrawer lD;

    [Header("Respect")] public int currentRespect;
    public TMP_Text respectDisplay;
    public String respectText;
    public int summonRespectThreshold;
    private bool canSummon;

    [Header("Target Selection")] public bool targetSelectModeActive;
    public LayerMask combatantLayerMask;
    public AbilityManager currentAbility;
    public Combatant combatantUsingAbility;
    public List<Combatant> targetsInRange = new List<Combatant>();

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
//        respectText = respectDisplay.text + " ";
      //  respectDisplay.text = respectText + currentRespect;
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
        Debug.Log("godDict = "+godDict.Count);
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

    #region Target Selection

    private void FixedUpdate()
    {
        if (targetSelectModeActive && currentAbility != null)
        {
            switch (currentAbility.ability.selectionType)
            {
                case SpecialAbility.eSelectionType.Single:
                    SingleTargetSelectMode();
                    break;

                case SpecialAbility.eSelectionType.CircleAoE:
                    AreaCircleSelect();
                    break;

                case SpecialAbility.eSelectionType.ConeAoE:
                    AreaConeSelect();
                    break;

                case SpecialAbility.eSelectionType.Self:
                    currentAbility.targetSelectModeActive = true;
                    break;
            }
        }
    }


    public void EnterTargetSelectMode(AbilityManager thisAbility)
    {
        // MOVE CAMERA TO GOD

        currentAbility = thisAbility;
        combatantUsingAbility = currentAbility.GetComponent<Combatant>();

        targetSelectModeActive = true;
    }

    public void ExitTargetSelectMode()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f;

        foreach (Combatant target in targetsInRange)
        {
            target.DeactivateTargetIcon();
        }

        targetsInRange.Clear();
        combatantUsingAbility = null;
        currentAbility = null;
        targetSelectModeActive = false;
    }

    private void SingleTargetSelectMode()
    {
        Vector3 centre = combatantUsingAbility.colliderHolder.transform.position;
        float abilityRange = currentAbility.ability.abilityRange;

        Collider[] colliders = Physics.OverlapSphere(centre, abilityRange, currentAbility.ability.targetLayerMask);

        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.5f;

        foreach (Collider targetCollider in colliders)
        {
            Combatant currentTarget = targetCollider.gameObject.GetComponentInParent<Combatant>();

            if (isTargetValid(currentTarget))
            {
                targetsInRange.Add(currentTarget);
            }
        }

        if (!targetsInRange.Any())
        {
            ExitTargetSelectMode();
            return;
        }

        foreach (Combatant target in targetsInRange)
        {
            target.ActivateTargetIcon();
        }

        currentAbility.targetSelectModeActive = true;
    }

    private void AreaCircleSelect()
    {
        combatantUsingAbility.ActivateCircleAreaMarker(currentAbility.ability.radius);

        Time.timeScale = 0.15f;
        Time.fixedDeltaTime = 0.15f;
        
        currentAbility.targetSelectModeActive = true;
    }

    private void AreaConeSelect()
    {
        combatantUsingAbility.ActivateConeAreaMarker();

        Debug.Log("activated cone area");
        
        Time.timeScale = 0.15f;
        Time.fixedDeltaTime = 0.15f;

        currentAbility.targetSelectModeActive = true;
        Debug.Log("AreaConeSelect from GameManager ran");
    }

    private bool isTargetValid(Combatant currentTarget)
    {
        if (currentTarget != null)
        {
            bool isInList = targetsInRange.Contains(currentTarget);
            bool canBeHit = currentAbility.ability.abilityCanHit.Contains(currentTarget.targetType);

            return !isInList && canBeHit;
        }
        else
        {
            return false;
        }
    }

    #endregion

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
        
        //Toggle outine
        foreach (var god in allPlayerGods)
        {
            god.ToggleOutlineOnOff(false);   
        }
        godToSelect.ToggleOutlineOnOff(true);

        InterimUIManager.Instance.UpdateHUD(currentlySelectedGod);
    }

    private void MoveGod()
    {
        if (currentlySelectedGod == null || targetSelectModeActive)
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
        // respectDisplay.text = respectText + currentRespect;

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