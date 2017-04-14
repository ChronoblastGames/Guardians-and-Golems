using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GuardianInputManager))]
[System.Serializable]
public class GuardianPlayerController : GuardianStats 
{
    private CrystalManager crystalManager;

    private GuardianInputManager guardianInputManager;
    private GuardianResources guardianResources;
    private GuardianCooldownManager guardianCooldown;

    private ConduitController conduitController;

    private TimerClass selectionTimer;

    private Animator guardianState;

    private float xAxis;
    private float zAxis;

    [Header("Guardian Attributes")]
    public PlayerTeam teamColor;

    [Header("Guardian Orb Attributes")]
    public GameObject guardianModel;

    public List<GameObject> conduitCapturedList;

    public PlayerTeam playerTeam;

    public float hoverHeight;

    [Header("Selection Values")]
    public GameObject attachedConduit;
    private GameObject selectedConduit;

    public float selectionDistance;

    public float selectionDelay;

    private RaycastHit rayHit;

    public LayerMask conduitMask;

    [Header("Guardian Booleans")]
    public bool canMove = true;
    public bool canCaptureOrb = true;
    public bool isCapturingOrb = false;
    public bool canUseAbility = false;
    public bool isUsingAbility = false;

    void Start()
    {
        PlayerSetup();
    }

    private void Update()
    {
        GatherInput();
        ManageAttack();
    }

    void PlayerSetup()
    {
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        guardianInputManager = GetComponent<GuardianInputManager>();
        guardianResources = GetComponent<GuardianResources>();
        guardianCooldown = GetComponent<GuardianCooldownManager>();

        guardianState = GetComponent<Animator>();

        selectionTimer = new TimerClass();

        selectionTimer.ResetTimer(selectionDelay);

        playerTeam = guardianInputManager.playerTeam;

        guardianModel.SetActive(false);
    }

    void GatherInput()
    {
        xAxis = guardianInputManager.xAxis;
        zAxis = guardianInputManager.zAxis;

        SearchForConduit(xAxis, zAxis);
    }

    void ManageAttack()
    {
        if (conduitCapturedList.Contains(attachedConduit))
        {
            if (conduitController.conduitState != ConduitState.DISABLED)
            {
                canUseAbility = true;
            }
        }
        else
        {
            canUseAbility = false;
        }
    }

    void SearchForConduit(float xAxis, float zAxis)
    {
        if (xAxis != 0 || zAxis != 0)
        {
            Vector3 lookVec = new Vector3(xAxis, 0, zAxis);

            if (Physics.Raycast(transform.position, lookVec, out rayHit, selectionDistance, conduitMask, QueryTriggerInteraction.Ignore))
            {
                selectedConduit = rayHit.collider.gameObject;

                if (selectionTimer.TimerIsDone() && !isUsingAbility)
                {
                    if (selectedConduit != attachedConduit && attachedConduit != null)
                    {
                        DeattachFromConduit();
                    }

                    AttachToConduit(selectedConduit);
                }
            }
        }
    }

    void AttachToConduit(GameObject conduit)
    {
        guardianModel.SetActive(true);

        transform.position = conduit.transform.position;
        guardianModel.transform.position = conduit.transform.position + new Vector3(0, hoverHeight, 0);
        attachedConduit = conduit;
        conduitController = attachedConduit.GetComponent<ConduitController>();
        selectionTimer.ResetTimer(selectionDelay);
        conduitController.attachedGuardianColor.Add(playerTeam);
    }

    void DeattachFromConduit()
    {
        conduitController.DeselectConduit(playerTeam);
        conduitController = null;
        attachedConduit = null;
    }

    public void AttempToCaptureConduit()
    {
        if (attachedConduit != null && !isCapturingOrb)
        {
            if (conduitController.CanCaptureConduit())
            {
                conduitController.StartConduitCapture(playerTeam, gameObject);

                guardianState.SetTrigger("StartCapture");
            }
        }
    }

    public void FinishCapture()
    {
        guardianState.SetTrigger("EndCapture");
    }

    public void UseAbility(int abilityNumber, PlayerTeam teamColor, float holdTime)
    {
        if (canUseAbility && attachedConduit != null && guardianCooldown.GlobalCooldownReady() && guardianCooldown.CanUseAbility(abilityNumber))
        {
            if (crystalManager.TryCast(guardianAbilites[abilityNumber].crystalCost, teamColor, PlayerType.GUARDIAN))
            {
                GameObject spawnObj = conduitController.centerCrystal;

                guardianState.SetTrigger("UseAbility");
                guardianState.SetTrigger("Ability" + (abilityNumber + 1));

                guardianAbilites[abilityNumber].CastGuardianAbility(teamColor, holdTime, spawnObj, gameObject);
                guardianCooldown.QueueGlobalCooldown();
                guardianCooldown.QueueAbilityCooldown(abilityNumber);
                isUsingAbility = true;
            }
        }     
    }  
}
