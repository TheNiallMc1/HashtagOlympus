using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
[RequireComponent(typeof(GodBehaviour))]
public class AbilityManager : MonoBehaviour
{
    [Header("Editor Gizmos")] 
    public bool displayRadius;
    public Color radiusColour;
    
    [Header("Ability Info")]
    public SpecialAbility ability;
    private List<Combatant> targets = new List<Combatant>();
    private bool targetSelectModeActive = false;
    public bool isChanneled = false;

    [Header("Visuals")] 
    public GameObject particleEffects;
    public string abilityStateName;
    public string channelAnimTrigger;

    [Header("Player Controls")]
    private PlayerControls playerControls;
    private bool leftClick;
    private bool rightClick;
    private Vector2 mousePosition;
    
    [Header("Components")]
    private Combatant thisCombatant;
    private GodBehaviour thisGod;
    private Camera mainCam;
    public Animator anim;
    ConeAoE coneAoE;

    [Header("Cooldown Info")]
    private Coroutine cooldownCoroutine;
    private bool onCooldown = false;
    public TextMeshProUGUI cooldownText;

    private void Awake()
    {
        ability = Instantiate(ability);

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
        mainCam = Camera.main;
        thisCombatant = GetComponent<Combatant>();
        ability.thisGod = GetComponent<GodBehaviour>();
        thisGod = ability.thisGod;
        anim = GetComponentInChildren<Animator>();

        cooldownText.text = ability.abilityName;
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

    private void OnEnable()
    {
        if (ability.remainingCooldownTime < ability.abilityCooldown && ability.remainingCooldownTime > 0)
        {
            cooldownText.text = ability.remainingCooldownTime.ToString(); 
        }
        
        else
        {
            cooldownText.text = ability.abilityName; 
        }
        
    }

    public void EnterTargetSelectMode()
    {
        if (!onCooldown && !targetSelectModeActive && thisGod.currentState != GodState.knockedOut)
        {
            Debug.Log("Enter target select mode");
            // ACTIVATE TARGET SELECT MODE SHADERS
            targetSelectModeActive = true;
        }
        
        else if (targetSelectModeActive)
        {
            targetSelectModeActive = false;
        }
    }

    // After target select mode
    void StartAbility()
    {
        targetSelectModeActive = false;

        ability.targets = targets;

        StartCooldown();

        // Trigger animation
        //anim.SetTrigger(animTrigger);
        anim.Play(abilityStateName);
        thisGod.attackAnimationIsPlaying = false;
        //ability.StartAbility(); // CALLED BY ANIMATION EVENT
        // particleEffects.SetActive(true); // CALLED BY ANIMATION EVENT


    }

    public void StartCooldown()
    {
        onCooldown = true;
        ability.remainingCooldownTime = ability.abilityCooldown;
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }

    void StartChannelling()
    {
        targetSelectModeActive = false;
        
        ability.targets = targets;
        
        // Trigger animation
        
        ability.StartAbility(); // CALLED BY ANIMATION EVENT
        // particleEffects.SetActive(true); // CALLED BY ANIMATION EVENT
        
        onCooldown = true;
        ability.remainingCooldownTime = ability.abilityCooldown;
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }

    IEnumerator ChannelTargetSelectCoroutine()
    {
        // Check targets on a cycle to keep list updated
        targets = coneAoE.targetsInCone;
        yield return null;
    }
    
    IEnumerator CooldownCoroutine()
    {
        if (this.isActiveAndEnabled)
        {
            cooldownText.text = ability.remainingCooldownTime.ToString();;
        }
        
        yield return new WaitForSecondsRealtime(1f);
        ability.remainingCooldownTime -= 1;
        
        if (this.isActiveAndEnabled)
        {
            cooldownText.text = ability.remainingCooldownTime.ToString();;
        }
        

        if(ability.remainingCooldownTime <= 0)
        {
            ability.remainingCooldownTime = 0;
            onCooldown = false;
            cooldownCoroutine = null;
            
            if (this.isActiveAndEnabled)
            {
                cooldownText.text = ability.abilityName;
            }
            
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
                currentTarget = hit.transform.gameObject.GetComponentInParent<Combatant>();

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
            Combatant currentTarget = targetCollider.gameObject.GetComponentInParent<Combatant>();

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
            ability.coneBuffer = 0.15f; // Offset time to let OnTriggerEnter activate
            ability.coneAlreadyExists = true;

            coneAoE = Instantiate(ability.coneAoE, transform.position, Quaternion.Euler(90f, 0, 0), thisCombatant.colliderHolder.transform);
            coneAoE.targetTypes = ability.abilityCanHit;
            coneAoE.lifeTime = ability.coneLifetime;
        }
        else
        {
            // When cone buffer hits zero, start the ability with the cone's targets
            ability.coneBuffer -= Time.deltaTime;

            if (ability.coneBuffer <= 0)
            {
                ability.coneBuffer = 0;
                targets = coneAoE.targetsInCone;

                StartAbility();
                ability.coneAlreadyExists = false;
            }
        }
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
        Gizmos.color = radiusColour;
        
        float radius = ability.radius;
        
        if (displayRadius && ability != null)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        
    }
}
