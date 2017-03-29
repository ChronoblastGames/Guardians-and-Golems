using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GuardianInputManager))]
[System.Serializable]
public class GuardianPlayerController : GuardianStats 
{
    private GuardianInputManager guardianInputManager;
    private GuardianResources guardianResources;
    private GuardianCooldownManager guardianCooldown;

    private ConduitController orbController;

    private TimerClass selectionTimer;

    private Animator guardianState;

    private float xAxis;
    private float zAxis;

    [Header("Guardian Orb Attributes")]
    public GameObject guardianModel;

    public List<GameObject> orbList;

    public PlayerTeam playerTeam;

    public float hoverHeight;

    [Header("Selection Values")]
    public GameObject attachedOrb;
    private GameObject selectedOrb;

    public float selectionDistance;

    public float selectionDelay;

    private RaycastHit rayHit;

    public LayerMask orbMask;

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

        SearchForOrb(xAxis, zAxis);
    }

    void ManageAttack()
    {
        if (orbList.Contains(attachedOrb))
        {
            if (orbController.orbState != OrbState.DISABLED)
            {
                canUseAbility = true;
            }
        }
        else
        {
            canUseAbility = false;
        }
    }

    void SearchForOrb(float xAxis, float zAxis)
    {
        if (xAxis != 0 || zAxis != 0)
        {
            Vector3 lookVec = new Vector3(xAxis, 0, zAxis);

            if (Physics.Raycast(transform.position, lookVec, out rayHit, selectionDistance, orbMask))
            {
                selectedOrb = rayHit.collider.gameObject;

                if (selectionTimer.TimerIsDone())
                {
                    if (selectedOrb != attachedOrb && attachedOrb != null)
                    {
                        DeattachFromOrb();
                    }

                    AttachToOrb(selectedOrb);
                }
            }
        }
    }

    void AttachToOrb(GameObject orb)
    {
        guardianModel.SetActive(true);

        transform.position = orb.transform.position;
        guardianModel.transform.position = orb.transform.position + new Vector3(0, hoverHeight, 0);
        attachedOrb = orb;
        orbController = attachedOrb.GetComponent<ConduitController>();
        selectionTimer.ResetTimer(selectionDelay);
        orbController.attachedGuardianColor.Add(playerTeam);
    }

    void DeattachFromOrb()
    {
        orbController.DeselectOrb(playerTeam);
        orbController = null;
        attachedOrb = null;
    }

    public void CaptureOrb()
    {
        if (attachedOrb != null && !isCapturingOrb)
        {
            orbController.StartOrbCapture(playerTeam, gameObject);

            guardianState.SetTrigger("StartCapture");
        }
    }

    public void FinishCapture()
    {
        guardianState.SetTrigger("EndCapture");
    }

    public void UseAbility(int abilityNumber, PlayerTeam teamColor, float holdTime)
    {
        if (canUseAbility && attachedOrb != null && guardianCooldown.GlobalCooldownReady() && guardianCooldown.CanUseAbility(abilityNumber))
        {
            GameObject spawnObj = orbController.centerCrystal;

            guardianState.SetTrigger("UseAbility");
            guardianState.SetTrigger("Ability" + (abilityNumber + 1));

            guardianAbilites[abilityNumber].CastGuardianAbility(teamColor, holdTime, spawnObj, gameObject);
            guardianCooldown.QueueGlobalCooldown();
            guardianCooldown.QueueAbilityCooldown(abilityNumber);
        }     
    }

    public void UseAbilityFromAllOrbs(int abilityNumber, PlayerTeam teamColor, float holdTime)
    {
        if (canUseAbility && attachedOrb != null)
        {
            for (int i = 0; i < orbList.Count; i++)
            {
                GameObject spawnObj = orbList[i].GetComponent<ConduitController>().centerCrystal;

                guardianAbilites[abilityNumber].CastGuardianAbility(teamColor, holdTime, spawnObj, gameObject);
            }

            guardianState.SetTrigger("UseAbility");
            guardianState.SetTrigger("Ability" + (abilityNumber + 1));

            guardianCooldown.QueueGlobalCooldown();
            guardianCooldown.QueueAbilityCooldown(abilityNumber);
        }
    }
}
