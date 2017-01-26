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
        xAxis = Input.GetAxis("HorizontalPlayer" + PlayerNumber);
        zAxis = Input.GetAxis("VerticalPlayer" + PlayerNumber);

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        if (Input.GetKeyDown(ABILITY_1))
        {

        }

        if (Input.GetKeyDown(ABILITY_2))
        {

        }

        if (Input.GetKeyDown(ABILITY_3))
        {

        }

        if (Input.GetKeyDown(ABILITY_4))
        {

        }
#endif
    }
}
