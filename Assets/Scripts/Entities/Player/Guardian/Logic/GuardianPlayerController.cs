﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GuardianInputController))]
[System.Serializable]
public class GuardianPlayerController : GuardianStats 
{
    private CrystalManager crystalManager;

    private GuardianInputController guardianInputController;
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

    public float startDelay;

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
        ManageAbilityCasting();
    }

    void PlayerSetup()
    {
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        guardianInputController = GetComponent<GuardianInputController>();
        guardianResources = GetComponent<GuardianResources>();
        guardianCooldown = GetComponent<GuardianCooldownManager>();

        guardianState = GetComponent<Animator>();

        selectionTimer = new TimerClass();

        selectionTimer.ResetTimer(selectionDelay);

        playerTeam = guardianInputController.playerTeam;

        StartCoroutine(StartDelay(startDelay));
    }

    void GatherInput()
    {
        xAxis = guardianInputController.xAxis;
        zAxis = guardianInputController.zAxis;

        SearchForConduit(xAxis, zAxis);
    }

    void ManageAbilityCasting()
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

                if (selectionTimer.TimerIsDone() && !isUsingAbility && canMove)
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
        transform.position = conduit.transform.position;
        guardianModel.transform.position = conduit.transform.position + new Vector3(0, hoverHeight, 0);
        attachedConduit = conduit;
        conduitController = attachedConduit.GetComponent<ConduitController>();
        selectionTimer.ResetTimer(selectionDelay);

        conduitController.GuardianAttachToConduit(playerTeam);
    }

    void DeattachFromConduit()
    {
        conduitController.GuardianDeattachToConduit(playerTeam);

        conduitController = null;
        attachedConduit = null;

        isCapturingOrb = false;
    }

    public void CaptureCurrentConduit()
    {
        if (attachedConduit != null)
        {
            if (conduitController.CanCaptureConduit(playerTeam))
            {
                conduitController.ConduitCapturingSetup(playerTeam);

                guardianState.SetTrigger("StartCapture");
            }
            else if (conduitController.CanIncreaseCapture(playerTeam))
            {
                conduitController.CapturingConduit(playerTeam);
            }
        }
    }

    public void CaptureConduit(GameObject conduitToCapture)
    {
        conduitCapturedList.Add(conduitToCapture);

        crystalManager.CaptureCrystal(playerTeam);
    }

    public void FinishCapture()
    {
        guardianState.SetTrigger("EndCapture");
    }

    public void UseAbility(int abilityNumber, PlayerTeam teamColor, float holdTime)
    {
        if (canUseAbility && !isUsingAbility && attachedConduit != null && guardianCooldown.GlobalCooldownReady() && guardianCooldown.CanUseAbility(abilityNumber))
        {
            if (crystalManager.TryCast(guardianAbilites[abilityNumber].crystalCost, teamColor, PlayerType.GUARDIAN))
            {
                GameObject spawnObj = conduitController.centerCrystal;

                guardianState.SetTrigger("UseAbility");
                guardianState.SetTrigger("Ability" + (abilityNumber + 1));

                guardianAbilites[abilityNumber].CastGuardianAbility(teamColor, holdTime, spawnObj, gameObject);
                guardianCooldown.QueueGlobalCooldown();
                guardianCooldown.QueueAbilityCooldown(abilityNumber);
            }
        }     
    }
    
    private IEnumerator StartDelay(float delay)
    {
        canMove = false;

        yield return new WaitForSeconds(delay);

        canMove = true;
    }
}
