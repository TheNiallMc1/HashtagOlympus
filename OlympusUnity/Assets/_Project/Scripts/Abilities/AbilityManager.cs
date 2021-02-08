using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool isChannelling;
    public Combatant lastSingleTarget;

    [Header("Visuals")] 
    public GameObject particleEffects;
    public string abilityStateName;
    public string channelFinishTrigger;

    [Header("Player Controls")]
    private PlayerControls playerControls;
    private bool rightClick;
    private bool leftClick;
    
    [Header("Components")]
    private Combatant thisCombatant;
    private GodBehaviour thisGod;
    private Camera mainCam;
    public Animator anim;
    private ConeAoE coneAoE;

    private bool onCooldown;
    public TextMeshProUGUI cooldownText;

    public LayerMask ground;

    private void Awake()
    {
        ability = Instantiate(ability);

        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Mouse.RightClick.started += ctx => rightClick = true;
        playerControls.Mouse.RightClick.canceled += ctx => rightClick = false;
        
        playerControls.Mouse.LeftClick.started += ctx => leftClick = true;
        playerControls.Mouse.LeftClick.canceled += ctx => leftClick = false;
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
        if (isChannelling)
        {
            if (leftClick)
            {
                EndChannel();
            }
        }
    }

    public void EndChannel()
    {
        isChannelling = false;
        thisCombatant.DeactivateConeAreaMarker();
        Debug.Log("<color=green> Channeling ended </color>");

        anim.SetTrigger(channelFinishTrigger);
        coneAoE.DestroyImmediate();
        thisCombatant.DeactivateConeAreaMarker();
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

    public void StartCooldown()
    {
        onCooldown = true;
        thisGod.currentState = GodState.idle;
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
    
    // After target select mode
    private void StartAbility()
    {
        ExitTargetSelectMode();

        ability.targets = targets;

        anim.Play(abilityStateName);
        thisGod.attackAnimationIsPlaying = false;
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
    
    public void ExitTargetSelectMode()
    {
        if (ability.selectionType == SpecialAbility.eSelectionType.CircleAoE)
        {
            thisCombatant.DeactivateCircleAreaMarker();
        }
                
        if (ability.selectionType == SpecialAbility.eSelectionType.ConeAoE)
        {
            thisCombatant.DeactivateConeAreaMarker();
            ability.coneAlreadyExists = false;
        }
        
        targetSelectModeActive = false;

        GameManager.Instance.ExitTargetSelectMode();
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

        if (leftClick)
        {
            ExitTargetSelectMode();
            // thisGod.currentState = GodState.idle;
        }
    }

    private void AoECircleSelect()
    {
        // targets = GameManager.Instance.targetsInRange;

        if (rightClick)
        {
            Vector3 centre = thisCombatant.colliderHolder.transform.position;
            float abilityRange = ability.radius;

            Collider[] colliders = Physics.OverlapSphere(centre, abilityRange, ability.targetLayerMask);



            foreach (Collider targetCollider in colliders)
            {
                Combatant currentTarget = targetCollider.gameObject.GetComponentInParent<Combatant>();

                if (isTargetValid(currentTarget))
                {
                    targets.Add(currentTarget);
                }
            }

            if (!targets.Any())
            {
                ExitTargetSelectMode();
                thisGod.currentState = GodState.idle;

                return;
            }

            StartAbility(); 
        }

        if (leftClick)
        {
            ExitTargetSelectMode();
            thisGod.currentState = GodState.idle;
        }
    }

    private void AoEConeSelect()
    {
        Ray ray;
        ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, ground))

        {
            Vector3 lookingPoint = new Vector3(hit.point.x, 0, hit.point.z);
            ability.thisGod.transform.LookAt(lookingPoint);
        }

        if (rightClick && !ability.coneAlreadyExists)
        {
            ability.coneBuffer = 0.15f; // Offset time to let OnTriggerEnter activate
            ability.coneAlreadyExists = true;

            coneAoE = Instantiate(ability.coneAoE, thisCombatant.coneMarker.transform.position, thisCombatant.coneMarker.transform.rotation, thisCombatant.coneMarker.transform);
            
            coneAoE.targetTypes = ability.abilityCanHit;
            coneAoE.lifeTime = ability.coneLifetime;
            coneAoE.ability = this;
        }

        if (ability.coneAlreadyExists)
        {
            Debug.Log("Cone already exists");
            // When cone buffer hits zero, start the ability with the cone's targets
            ability.coneBuffer -= Time.deltaTime;

            if (ability.coneBuffer <= 0)
            {
                ability.coneBuffer = 0;
                targets = coneAoE.targetsInCone;
                Debug.Log("<color=green> Start Ability called </color>");
                StartAbility();
            }
        }

        if (leftClick && !ability.coneAlreadyExists)
        {
            Debug.Log("<color=green> Cone cancelled before cast </color>");
            ExitTargetSelectMode();
        }
    }

    public void ChannelAbilityTick()
    {
        Debug.Log("<color=blue> Tick effect called </color>");
        ability.targets = coneAoE.targetsInCone;
        ability.AbilityEffect();
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
