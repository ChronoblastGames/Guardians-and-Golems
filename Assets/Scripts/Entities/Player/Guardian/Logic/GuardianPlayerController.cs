using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GuardianInputManager))]
[System.Serializable]
public class GuardianPlayerController : GuardianStats 
{
    private GuardianInputManager guardianInputManager;

    private float xAxis;
    private float zAxis;

    [Header("Guardian Orb Attributes")]
    public List<GameObject> orbList;

    public bool isCapturingOrb = false;

    [Header("Selection Values")]
    public GameObject selectedOrb;

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
                GameObject hitOrb = rayHit.collider.gameObject;
                selectedOrb = hitOrb;
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
                guardianAbilites[abilityNumber].CastAbility(aimVec, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }
            else
            {
                guardianAbilites[abilityNumber].CastAbility(transform.forward, teamColor);
                StartCoroutine(cdAbility.RestartCoolDownCoroutine());
            }
        }
    }

    public void CaptureOrb(PlayerTeam teamColor)
    {
        if (selectedOrb != null && !isCapturingOrb)
        {
            selectedOrb.GetComponent<OrbController>().StartOrbCapture(teamColor, gameObject);
        }
    }
}
