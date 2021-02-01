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
    public List<Combatant> targets = new List<Combatant>();
    public bool targetSelectModeActive;
    public bool isChanneled;
    public Combatant lastSingleTarget;

    [Header("Visuals")] 
    public GameObject particleEffects;
    public string abilityStateName;
    public string channelAnimTrigger;

    [Header("Player Controls")]
    private PlayerControls playerControls;
    private bool rightClick;
    
    [Header("Components")]
    private Combatant thisCombatant;
    private GodBehaviour thisGod;
    private Camera mainCam;
    public Animator anim;
    private ConeAoE coneAoE;

    private bool onCooldown;
    public TextMeshProUGUI cooldownText;

    private void Awake()
    {
        ability = Instantiate(ability);

        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Mouse.RightClick.started += ctx => rightClick = true;

        playerControls.Mouse.RightClick.canceled += ctx => rightClick = false;
    }

    private void Start()
    {
        mainCam = Camera.main;
        thisCombatant = GetComponent<Combatant>();
        ability.thisGod = GetComponent<GodBehaviour>();
        thisGod = ability.thisGod;
        anim = GetComponentInChildren<Animator>();

        if (cooldownText != null)
        {
            cooldownText.text = ability.abilityName;   
        }
        
    }

    private void Update()
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
        if (cooldownText != null)
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
    }

    // After target select mode
    private void StartAbility()
    {
        targetSelectModeActive = false;
        GameManager.Instance.ExitTargetSelectMode();

        ability.targets = targets;

        anim.Play(abilityStateName);
        thisGod.attackAnimationIsPlaying = false;
    }

    public void StartCooldown()
    {
        onCooldown = true;
        ability.remainingCooldownTime = ability.abilityCooldown;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        if (this.isActiveAndEnabled)
        {
            cooldownText.text = ability.remainingCooldownTime.ToString();
        }
        
        yield return new WaitForSecondsRealtime(1f);
        ability.remainingCooldownTime -= 1;
        
        if (this.isActiveAndEnabled)
        {
            cooldownText.text = ability.remainingCooldownTime.ToString();
        }
        

        if(ability.remainingCooldownTime <= 0)
        {
            ability.remainingCooldownTime = 0;
            onCooldown = false;

            if (this.isActiveAndEnabled)
            {
                cooldownText.text = ability.abilityName;
            }
            
        }
        
        else
        {
            StartCoroutine(CooldownCoroutine());
        }
    }
    
    #region Target Selection
    
    public void EnterTargetSelectMode()
    {
        thisGod.currentState = GodState.usingAbility;
        
        if (!onCooldown && !targetSelectModeActive && thisGod.currentState != GodState.knockedOut)
        {
            GameManager.Instance.EnterTargetSelectMode(this);
        }
        
        else if (targetSelectModeActive)
        {
            targetSelectModeActive = false;
        }
    }

    private void SingleTargetSelect()
    {
        List<Combatant> targetsInRange = GameManager.Instance.targetsInRange;

        Ray ray = mainCam.ScreenPointToRay(playerControls.Mouse.MousePos.ReadValue<Vector2>());
        Combatant currentTarget;
        
        if (rightClick)
        {
            if ( Physics.Raycast(ray, out RaycastHit hit, 100, ability.targetLayerMask) )
            {
                currentTarget = hit.transform.gameObject.GetComponentInParent<Combatant>();
                
                if (targetsInRange.Contains(currentTarget))
                {
                    lastSingleTarget = currentTarget;
                    
                    targets.Add(currentTarget);
                    StartAbility();
                }
            }
        }
    }

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
