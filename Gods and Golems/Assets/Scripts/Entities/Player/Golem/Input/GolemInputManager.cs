using System.Collections;
using UnityEngine;

[System.Serializable]
public class GolemInputManager : MonoBehaviour 
{
    [Header("Golem Controls")] //Keycodes that relate to use of Special Abilities (KEYBOARD)
    public KeyCode ABILITY_1;
    public KeyCode ABILITY_2;
    public KeyCode ABILITY_3;
    public KeyCode ABILITY_4;

    [Header("Golem Input Variables")]
    public string PlayerName;

    public int PlayerNumber;

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
        if (PlayerNumber == 0)
        {
            Debug.LogError("Player " + gameObject.name + " does not have a Player Number!");
        }
    }

    void GetInput()
    {
        xAxis = Input.GetAxis("HorizontalPlayer" + PlayerNumber);
        zAxis = Input.GetAxis("VerticalPlayer" + PlayerNumber);

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
    }
}
