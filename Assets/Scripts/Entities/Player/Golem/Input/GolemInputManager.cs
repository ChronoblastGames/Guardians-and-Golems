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

    [Header("Golem Input Variables")]
    public string PlayerName;
    public string PlayerNumber;

    public float xAxis;
    public float zAxis;

    public bool isUsingKeyBoard;

    [Header("Input Axis Booleans")]
    public bool isAbilityButton1Down;
    public bool isAbilityButton2Down;
    public bool isAbilityButton3Down;

    private void Start()
    {
        PlayerSetup();
    }

    private void Update()
    {
        GetInput();
    }

    void PlayerSetup()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();
    }

    void GetInput()
    {
        if (!isUsingKeyBoard)
        {
            xAxis = Input.GetAxis("HorizontalPlayer" + PlayerNumber);
            zAxis = Input.GetAxis("VerticalPlayer" + PlayerNumber);
        }
        else
        {
            xAxis = Input.GetAxis("Horizontal");
            zAxis = Input.GetAxis("Vertical");
        }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 2")) //Abilities
        {
            golemPlayerController.UseAbility(1);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 0"))
        {
            golemPlayerController.UseAbility(2);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 1"))
        {
            golemPlayerController.UseAbility(3);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 5")) //Basic Attack
        {
            Debug.Log("Making a Basic Attack!");
        }


#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

        if (Input.GetAxisRaw("Ability1Player" + PlayerNumber + "OSX") > 0)
        {
            Debug.Log("Use Ability 1");
        }

        if (Input.GetAxisRaw("Ability2Player" + PlayerNumber + "OSX") > 0)
        {
            Debug.Log("Use Ability 2");
        }

        if (Input.GetAxisRaw("Ability3Player" + PlayerNumber + "OSX") > 0)
        {
            Debug.Log("Use Ability 3");
        }

#endif
    }
}
