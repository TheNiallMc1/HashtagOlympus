using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Combatant))]
public class AbilityManager : MonoBehaviour
{
    public SpecialAbility ability;
    

    bool onCooldown = false;
    Coroutine cooldownCoroutine;

    public List<Combatant> targets = new List<Combatant>();

    public bool targetSelectModeActive = false;

    [HideInInspector]
    public Combatant thisCombatant;
    protected PlayerControls playerControls;
    private bool leftClick;
    private bool rightClick;
    private Vector2 mousePosition;
    public Camera mainCam;

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

    // Start is called before the first frame update
    void Start()
    {
        thisCombatant = GetComponent<Combatant>();

        
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

    public void UseAbility()
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


    void ExecuteAbility()
    {
        targetSelectModeActive = false;

        ability.targets = targets;
        ability.ExecuteAbility();

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


        //onCooldown = true;
        //yield return new WaitForSecondsRealtime(ability.abilityCooldown);
        //onCooldown = false;
    }



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
                    ExecuteAbility();
                }
            }
        }

    }

    private void AoEConeSelect()
    {
        // throw new NotImplementedException();
    }

    private void SelfSelect()
    {
        // throw new NotImplementedException();
    }


    // POLISH - Allow flexibility to place the centre in the case of Artemis/Zeus
    public void AoECircleSelect()
    {
        Vector3 centre = thisCombatant.colliderHolder.transform.position;

        Collider[] colliders = Physics.OverlapSphere(centre, ability.radius);

        foreach (Collider collider in colliders)
        {
            Combatant currentTarget = collider.gameObject.GetComponent<Combatant>();

            if ( isTargetValid(currentTarget) )
            {
                targets.Add(currentTarget);
            }
        }

        
        ExecuteAbility();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ability.radius);
    }
}
