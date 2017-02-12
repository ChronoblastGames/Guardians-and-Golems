using System.Collections;
using UnityEngine;

[System.Serializable]
public class GolemInputManager : MonoBehaviour 
{
    private GolemPlayerController golemPlayerController;

    [Header("Golem Player Values")]
    public string PlayerName;
    public string PlayerNumber;
    public string teamColor;

    [Header("Golem Input Values")]
    [HideInInspector]
    public float xAxis;
    [HideInInspector]
    public float zAxis;

    [HideInInspector]
    public float aimXAxis;
    [HideInInspector]
    public float aimZAxis;

    private float modifierAxis;

    [HideInInspector]
    public float blockAxis;

    [Header("Debug Values")]
    private bool isBlockAxisActive = false;

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
        golemPlayerController = GetComponent<GolemPlayerController>();    
    }

    void GetInput()
    {

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        xAxis = Input.GetAxis("HorizontalPlayer" + PlayerNumber + "Win");
        zAxis = Input.GetAxis("VerticalPlayer" + PlayerNumber + "Win");

        aimXAxis = Input.GetAxis("HorizontalAimPlayer" + PlayerNumber + "Win");
        aimZAxis = Input.GetAxis("VerticalAimPlayer" + PlayerNumber + "Win");
        modifierAxis = Input.GetAxis("ModifierAxisPlayer" + PlayerNumber + "Win");
        blockAxis = Input.GetAxis("BlockAxisPlayer" + PlayerNumber + "Win");

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 4"))
        {
            golemPlayerController.UseAbility(2, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 4"))
        {
            golemPlayerController.UseAbility(0, aimVec, teamColor);
        }

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 5"))
        {
            golemPlayerController.UseAbility(3, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 5"))
        {
            golemPlayerController.UseAbility(1, aimVec, teamColor);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 1"))
        {
            golemPlayerController.Dodge();
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 2"))
        {
            golemPlayerController.UseQuickAttack();
        }

        if (blockAxis != 0)
        {
            if (isBlockAxisActive == false)
            {
                isBlockAxisActive = true;
            }
            else if (isBlockAxisActive)
            {
                golemPlayerController.Block();
            }           
        }
        else if (blockAxis == 0)
        {
            if (isBlockAxisActive)
            {
                golemPlayerController.Unblock();
                isBlockAxisActive = false;
            } 
        }

#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

        xAxis = Input.GetAxis("HorizontalPlayer" + PlayerNumber + "OSX");
        zAxis = Input.GetAxis("VerticalPlayer" + PlayerNumber + "OSX");

        aimXAxis = Input.GetAxis("HorizontalAimPlayer" + PlayerNumber + "OSX");
        aimZAxis = Input.GetAxis("VerticalAimPlayer" + PlayerNumber + "OSX");
        modifierAxis = Input.GetAxis("ModifierAxis" + PlayerNumber + "OSX");
        blockAxis = Input.GetAxis("BlockAxisPlayer" + PlayerNumber + "OSX");

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 13") || Input.GetKeyDown(ABILITY_1))
        {
            golemPlayerController.UseAbility(2, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 13") || Input.GetKeyDown(ABILITY_1))
        {
            golemPlayerController.UseAbility(0, aimVec, teamColor);
        }

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 14") || Input.GetKeyDown(ABILITY_1))
        {
            golemPlayerController.UseAbility(3, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 14") || Input.GetKeyDown(ABILITY_2))
        {
            golemPlayerController.UseAbility(1, aimVec, teamColor);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 18") || Input.GetKeyDown(LIGHT_ATTACK)) //Basic Attack
        {
            golemPlayerController.UseQuickAttack();
        }

        if (blockAxis != 0)
        {
            golemPlayerController.Block();
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
