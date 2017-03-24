using System.Collections;
using UnityEngine;

public class GuardianInputManager : MonoBehaviour 
{
    private GuardianPlayerController guardianController;

    [Header("Guardian Player Values")]
    public PlayerNum playerNum;
    public PlayerTeam playerTeam;

    public string PlayerName;
    public int PlayerNumber;

    [Header("Guardian Input Values")]
    [HideInInspector]
    public float xAxis;
    [HideInInspector]
    public float zAxis;
    public float holdTime;

    [HideInInspector]
    public float aimXAxis;
    [HideInInspector]
    public float aimZAxis;

    private float modifierAxis;
    public float multicastAxis;

    [HideInInspector]
    public Vector3 aimVec;

    private string inputAxisX = "";
    private string inputAxisZ = "";
    private string inputAimAxisX = "";
    private string inputAimAxisZ = "";
    private string inputModifierAxis = "";
    private string inputMulticastAxis = "";
    private string inputCaptureButton = "";
    private string inputAbility1Button = "";
    private string inputAbility2Button = "";

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

        switch (playerNum)
        {
            case PlayerNum.PLAYER_1:
                PlayerNumber = 1;
                break;
            case PlayerNum.PLAYER_2:
                PlayerNumber = 2;
                break;
            case PlayerNum.PLAYER_3:
                PlayerNumber = 3;
                break;
            case PlayerNum.PLAYER_4:
                PlayerNumber = 4;
                break;
        }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        inputAxisX = "HorizontalPlayer" + PlayerNumber + "Win";
        inputAxisZ = "VerticalPlayer" + PlayerNumber + "Win";

        inputAimAxisX = "HorizontalAimPlayer" + PlayerNumber + "Win";
        inputAimAxisZ = "VerticalAimPlayer" + PlayerNumber + "Win";

        inputModifierAxis = "ModifierAxisPlayer" + PlayerNumber + "Win";
        inputMulticastAxis = "BlockAxisPlayer" + PlayerNumber + "Win";

        inputCaptureButton = "joystick " + PlayerNumber + " button 2";

        inputAbility1Button = "joystick " + PlayerNumber + " button 4";
        inputAbility2Button = "joystick " + PlayerNumber + " button 5";
#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

        inputAxisX = "HorizontalPlayer" + PlayerNumber + "OSX";
        inputAxisZ = "VerticalPlayer" + PlayerNumber + "OSX";

        inputAimAxisX = "HorizontalAimPlayer" + PlayerNumber + "OSX";
        inputAimAxisZ = "VerticalAimPlayer" + PlayerNumber + "OSX";

        inputModifierAxis = "ModifierAxisPlayer" + PlayerNumber + "OSX";
        inputMulticastAxis = "BlockAxisPlayer" + PlayerNumber + "OSX";

        inputCaptureButton = "joystick " + PlayerNumber + " button 18";

        inputAbility1Button = "joystick " + PlayerNumber + " button 13";
        inputAbility2Button = "joystick " + PlayerNumber + " button 14";

#endif

    }

    void GetInput()
    {
        xAxis = Input.GetAxis(inputAxisX);
        zAxis = Input.GetAxis(inputAxisZ);

        aimXAxis = Input.GetAxis(inputAimAxisX);
        aimZAxis = Input.GetAxis(inputAimAxisZ);

        modifierAxis = Input.GetAxis(inputModifierAxis);
        multicastAxis = Input.GetAxis(inputMulticastAxis);

        if (Input.GetKeyDown(inputCaptureButton))
        {
            guardianController.CaptureOrb();
        }

        if (modifierAxis != 0 && multicastAxis != 0 && Input.GetKeyUp(inputAbility1Button))
        {
            guardianController.UseAbilityFromAllOrbs(2, playerTeam, holdTime);
        }
        else if (modifierAxis != 0 && Input.GetKeyUp(inputAbility1Button))
        {
            guardianController.UseAbility(2, playerTeam, holdTime);
        }
        else if (multicastAxis != 0 && Input.GetKeyUp(inputAbility1Button))
        {
            guardianController.UseAbilityFromAllOrbs(0, playerTeam, holdTime);
        }
        else if (Input.GetKeyUp(inputAbility1Button))
        {
            guardianController.UseAbility(0, playerTeam, holdTime);
        }

        if (modifierAxis != 0 && multicastAxis != 0 && Input.GetKeyUp(inputAbility2Button))
        {
            guardianController.UseAbilityFromAllOrbs(3, playerTeam, holdTime);
        }
        else if (modifierAxis != 0 && Input.GetKeyUp(inputAbility2Button))
        {
            guardianController.UseAbility(3, playerTeam, holdTime);
        }
        else if (multicastAxis != 0 && Input.GetKeyUp(inputAbility2Button))
        {
            guardianController.UseAbilityFromAllOrbs(1, playerTeam, holdTime);
        }
        else if (Input.GetKeyUp(inputAbility2Button))
        {
            guardianController.UseAbility(1, playerTeam, holdTime);
        }
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
