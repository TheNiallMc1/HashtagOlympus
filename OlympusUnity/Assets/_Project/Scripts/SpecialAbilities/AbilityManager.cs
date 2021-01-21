using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Combatant))]
public class AbilityManager : MonoBehaviour
{
    private Combatant thisCombatant;
    
    public SpecialAbility ability;
    
    private bool onCooldown = false;
    private Coroutine cooldownCoroutine;

    private List<Combatant> targets = new List<Combatant>();

    private bool targetSelectModeActive = false;
    
    protected PlayerControls playerControls;
    private bool leftClick;
    private bool rightClick;
    private Vector2 mousePosition;
    
    public Camera mainCam;

    ConeAoE coneAoE;

    [Header("Testing")]
    public Text cooldownText;

    private void Awake()
    {
        ability = Instantiate(ability);

        mainCam = Camera.main;

        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Mouse.LeftClick.started += ctx => leftClick = true;
        playerControls.Mouse.RightClick.started += ctx => rightClick = true;

        playerControls.Mouse.MousePos.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();

        playerControls.Mouse.LeftClick.canceled += ctx => leftClick = false;
        playerControls.Mouse.RightClick.canceled += ctx => rightClick = false;
    }

    void Start()
    {
        thisCombatant = GetComponent<Combatant>();
        ability.thisGod = GetComponent<GodBehaviour>();
    }

    void Update()
    {
        if (targetSelectModeActive)
        {
            switch (ability.selectionType)
            {
                case SpecialAbility.eSelectionType.Single:
                    SingleTargetSelect();
                    break;

                case SpecialAbility.eSelectionType.CircleAoE:
                    AoECircleSelect();
                    break;

                case SpecialAbility.eSelectionType.ConeAoE:
                    AoEConeSelect();
                    break;

                case SpecialAbility.eSelectionType.Self:
                    SelfSelect();
                    break;
            }
        }
    }

    public void EnterTargetSelectMode()
    {
        if (!onCooldown && !targetSelectModeActive)
        {
            targetSelectModeActive = true;
        }
        
        else if (targetSelectModeActive)
        {
            targetSelectModeActive = false;
        }
    }

    void StartAbility()
    {
        targetSelectModeActive = false;

        ability.targets = targets;
        ability.StartAbility();

        onCooldown = true;
        ability.remainingCooldownTime = ability.abilityCooldown;
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }
    
    IEnumerator CooldownCoroutine()
    {
        cooldownText.text = ability.remainingCooldownTime.ToString();
        yield return new WaitForSecondsRealtime(1f);
        ability.remainingCooldownTime -= 1;
        

        if(ability.remainingCooldownTime <= 0)
        {
            ability.remainingCooldownTime = 0;
            onCooldown = false;
            cooldownCoroutine = null;
            cooldownText.text = "";
        }
        
        else
        {
            cooldownCoroutine = StartCoroutine(CooldownCoroutine());
        }
    }

    IEnumerator TickEffectCoroutine()
    {
        yield return null;
    }


    #region Target Selection
    
    void SingleTargetSelect()
    {
        Combatant currentTarget;

        Ray ray = mainCam.ScreenPointToRay(playerControls.Mouse.MousePos.ReadValue<Vector2>());
        RaycastHit hit;

        if (rightClick)
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                currentTarget = hit.transform.gameObject.GetComponent<Combatant>();

                if (currentTarget != null && ability.abilityCanHit.Contains(currentTarget.targetType))
                {
                    targets.Add(currentTarget);
                    StartAbility();
                }
            }
        }
    }

    // POLISH - Allow flexibility to place the centre in the case of Artemis/Zeus
    private void AoECircleSelect()
    {
        Vector3 centre = thisCombatant.colliderHolder.transform.position;

        Collider[] colliders = Physics.OverlapSphere(centre, ability.radius);

        foreach (Collider targetCollider in colliders)
        {
            Combatant currentTarget = targetCollider.gameObject.GetComponent<Combatant>();

            if (isTargetValid(currentTarget))
            {
                targets.Add(currentTarget);
            }
        }
        
        StartAbility();
    }

    private void AoEConeSelect()
    {

        if (!ability.coneAlreadyExists)
        {
            ability.coneBuffer = 0.15f;
            ability.coneAlreadyExists = true;

            coneAoE = Instantiate(ability.coneAoE, transform.position, Quaternion.Euler(90f, 0, 0), thisCombatant.colliderHolder.transform);
            coneAoE.targetTypes = ability.abilityCanHit;
        }
        else
        {
            ability.coneBuffer -= Time.deltaTime;

            if (ability.coneBuffer <= 0)
            {
                ability.coneBuffer = 0;
                targets = coneAoE.targetsInCone;

                StartAbility();
                ability.coneAlreadyExists = false;
            }
        }


        // DestroyImmediate(coneAoE);
    }

    private void SelfSelect()
    {
        targets.Add(thisCombatant);
        StartAbility();
    }

    public bool isTargetValid(Combatant currentTarget)
    {
        if(currentTarget != null)
        {
            bool isInList = targets.Contains(currentTarget);
            bool canBeHit = ability.abilityCanHit.Contains(currentTarget.targetType);

            return !isInList && canBeHit;
        }
        else
        {
            return false;
        }
    }
    
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ability.radius);
    }
}
