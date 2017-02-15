using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GuardianInputManager))]
[System.Serializable]
public class GuardianPlayerController : GuardianStats 
{
    private GuardianInputManager guardianInputManager;
    private TimerClass switchTimer;

    private float xAxis;
    private float zAxis;

    [Header("Guardian Orb Attributes")]
    public List<GameObject> orbList;

    public PlayerTeam playerTeam;

    public bool isCapturingOrb = false;

    [Header("Selection Values")]
    private OrbController orbController;
    public GameObject selectedOrb;
    public GameObject attachedOrb;

    public float selectionDistance;

    private RaycastHit rayHit;

    public LayerMask orbMask;

    [Header("Debugging Values")]
    private BasicCooldown cdAbility;

    [Header("CoolDowns")]
    private float globalCooldownTime;

    void Start()
    {
        PlayerSetup();
    }

    private void Update()
    {
        GatherInput();
    }

    void PlayerSetup()
    {
        cdAbility = new BasicCooldown();

        globalCooldownTime = GameObject.FindObjectOfType<GeneralVariables>().globalCooldown;

        cdAbility.cdTime = globalCooldownTime;

        guardianInputManager = GetComponent<GuardianInputManager>();

        switchTimer = new TimerClass();

        playerTeam = guardianInputManager.playerTeam;
    }

    void GatherInput()
    {
        xAxis = guardianInputManager.xAxis;
        zAxis = guardianInputManager.zAxis;

        LookForOrb(xAxis, zAxis);
    }

    void LookForOrb(float xAxis, float zAxis)
    {
        if (xAxis != 0 || zAxis != 0)
        {
            Vector3 lookVec = new Vector3(xAxis, 0, zAxis);

            if (Physics.Raycast(transform.position, lookVec, out rayHit, selectionDistance, orbMask))
            {
                if (switchTimer.TimerIsDone())
                {
                    GameObject hitOrb = rayHit.collider.gameObject;
                    selectedOrb = hitOrb;
                    MoveToOrb(playerTeam);
                }     
            }
            else
            {
                selectedOrb = null;
            }
        }
        else
        {
            selectedOrb = null;
        }
    }

    public void UseAbility(int abilityNumber, Vector3 aimVec, PlayerTeam teamColor)
    {
        if (aimVec != null && (cdAbility.cdStateEngine.currentState == cdAbility.possibleStates[2]))
        {
            if (aimVec != Vector3.zero)
            {
                guardianAbilites[abilityNumber].CastAbility(aimVec, transform.position, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }
            else
            {
                guardianAbilites[abilityNumber].CastAbility(transform.forward, transform.position, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }
        }
    }

    public void MoveToOrb(PlayerTeam teamColor)
    {
        if (selectedOrb != null && !isCapturingOrb && selectedOrb != attachedOrb)
        {
            if (attachedOrb != null)
            {
                orbController.ResetOrb();             
            }

            orbController = selectedOrb.GetComponent<OrbController>();

            if (orbController.MoveToOrb(teamColor))
            {
                transform.position = selectedOrb.transform.position;
                attachedOrb = selectedOrb;
                orbController.orbState = OrbState.CONTROLLED;
                selectedOrb = null;
            }

            switchTimer.ResetTimer(0.5f);
        }
    }

    public void TakeOrbControl(PlayerTeam teamColor)
    {
        if (attachedOrb != null)
        {

        }
    }
}
