using System.Collections;
using UnityEngine;

public class GuardianInputManager : MonoBehaviour 
{
    private GuardianPlayerController guardianController;

    [Header("Guardian Player Values")]
    public string PlayerName;
    public string PlayerNumber;
    public string teamColor;

    [Header("Guardian Input Values")]
    [HideInInspector]
    public float xAxis;
    [HideInInspector]
    public float zAxis;

    [HideInInspector]
    public float aimXAxis;
    [HideInInspector]
    public float aimZAxis;

    private float modifierAxis;

    private Vector3 aimVec;

    private void Start()
    {
        PlayerSetup();
    }

    private void Update()
    {
        GetInput();

        CalculateAimVec();
    }

    void PlayerSetup()
    {
        guardianController = GetComponent<GuardianPlayerController>();
    }

    void GetInput()
    {

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        xAxis = Input.GetAxis("HorizontalPlayer" + PlayerNumber + "Win");
        zAxis = Input.GetAxis("VerticalPlayer" + PlayerNumber + "Win");

        aimXAxis = Input.GetAxis("HorizontalAimPlayer" + PlayerNumber + "Win");
        aimZAxis = Input.GetAxis("VerticalAimPlayer" + PlayerNumber + "Win");
        modifierAxis = Input.GetAxis("ModifierAxisPlayer" + PlayerNumber + "Win");

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 4"))
        {
            guardianController.UseAbility(2, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 4"))
        {
            guardianController.UseAbility(0, aimVec, teamColor);
        }

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 5"))
        {
            guardianController.UseAbility(3, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 5"))
        {
            guardianController.UseAbility(1, aimVec, teamColor);
        }

#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

        xAxis = Input.GetAxis("HorizontalPlayer" + PlayerNumber + "OSX");
        zAxis = Input.GetAxis("VerticalPlayer" + PlayerNumber + "OSX");

        aimXAxis = Input.GetAxis("HorizontalAimPlayer" + PlayerNumber + "OSX");
        aimZAxis = Input.GetAxis("VerticalAimPlayer" + PlayerNumber + "OSX");
        modifierAxis = Input.GetAxis("ModifierAxis" + PlayerNumber + "OSX");

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 13") || Input.GetKeyDown(ABILITY_1))
        {
            guardianController.UseAbility(2, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 13") || Input.GetKeyDown(ABILITY_1))
        {
            guardianController.UseAbility(0, aimVec, teamColor);
        }

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 14") || Input.GetKeyDown(ABILITY_1))
        {
            guardianController.UseAbility(3, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 14") || Input.GetKeyDown(ABILITY_2))
        {
            guardianController.UseAbility(1, aimVec, teamColor);
        }

#endif
    }

    void CalculateAimVec()
    {
        if (aimXAxis != 0 || aimZAxis != 0)
        {
            aimVec = new Vector3(aimXAxis, 0, aimZAxis);
            aimVec.Normalize();
        }
        else
        {
            aimVec = Vector3.zero;
        }
    }
}
