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
    public KeyCode LIGHT_ATTACK;

    [Header("Golem Input Variables")]
    public string PlayerName;
    public string PlayerNumber;

    public float xAxis;
    public float zAxis;

    public bool isUsingKeyboard;

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
        if (!isUsingKeyboard)
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

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 2") || Input.GetKeyDown(ABILITY_1)) //Abilities
        {
            golemPlayerController.UseAbility(0);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 0") || Input.GetKeyDown(ABILITY_2))
        {
            golemPlayerController.UseAbility(1);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 1") || Input.GetKeyDown(ABILITY_3))
        {
            golemPlayerController.UseAbility(2);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 5") || Input.GetKeyDown(LIGHT_ATTACK)) //Basic Attack
        {
            golemPlayerController.LightAttack();
        }


#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

    if (Input.GetKeyDown("joystick " + PlayerNumber + " button 18") || Input.GetKeyDown(ABILITY_1)) //Abilities
        {
            golemPlayerController.UseAbility(0);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 16") || Input.GetKeyDown(ABILITY_2))
        {
            golemPlayerController.UseAbility(1);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 17") || Input.GetKeyDown(ABILITY_3))
        {
            golemPlayerController.UseAbility(2);
        }

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 14") || Input.GetKeyDown(LIGHT_ATTACK)) //Basic Attack
        {
            Debug.Log("Making a Basic Attack!");
        }

#endif
    }
}
