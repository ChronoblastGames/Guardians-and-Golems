using System.Collections;
using UnityEngine;

[System.Serializable]
public class GolemInputManager : MonoBehaviour 
{
    private GolemPlayerController golemPlayerController;

    [Header("Golem Controls")] //Keycodes that relate to use of Special Abilities (KEYBOARD)
    public KeyCode ABILITY_1;
    public KeyCode ABILITY_2;
    public KeyCode ABILITY_3;
    public KeyCode ABILITY_4;
    public KeyCode MODIFIER;
    public KeyCode LIGHT_ATTACK;
    public KeyCode DODGE;

    [Header("Golem Input Variables")]
    public string PlayerName;
    public string PlayerNumber;
    public string teamColor;

    public float xAxis;
    public float zAxis;

    public float aimXAxis;
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

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 4") || Input.GetKeyDown(ABILITY_1))
        {
            golemPlayerController.UseAbility(2, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 4") || Input.GetKeyDown(ABILITY_1))
        {
            golemPlayerController.UseAbility(0, aimVec, teamColor);
        }

        if (modifierAxis != 0 && Input.GetKeyDown("joystick " + PlayerNumber + " button 5") || Input.GetKeyDown(ABILITY_1))
        {
            golemPlayerController.UseAbility(3, aimVec, teamColor);
        }
        else if (Input.GetKeyDown("joystick " + PlayerNumber + " button 5") || Input.GetKeyDown(ABILITY_2))
        {
            golemPlayerController.UseAbility(1, aimVec, teamColor);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 1") || Input.GetKeyDown(ABILITY_3))
        {
            golemPlayerController.Dodge();
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 2") || Input.GetKeyDown(LIGHT_ATTACK)) //Basic Attack
        {
            golemPlayerController.UseQuickAttack();
        }

#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

        aimXAxis = Input.GetAxis("HorizontalAimPlayer" + PlayerNumber + "OSX");
        aimZAxis = Input.GetAxis("VerticalAimPlayer" + PlayerNumber + "OSX");
        modifierAxis = Input.GetAxis("ModifierAxis" + PlayerNumber + "OSX");

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
