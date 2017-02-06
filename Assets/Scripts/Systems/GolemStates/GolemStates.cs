using System.Collections;
using UnityEngine;

public class GolemStates : MonoBehaviour 
{
    [Header("Current Combat State")]
    public CombatStates combatStates;

    [Header("Current Character State")]
    public GolemCharacterStates golemState;

    private void Start()
    {
        combatStates = CombatStates.IDLE;

        golemState = GolemCharacterStates.IDLE;
    }

    public void ChangeCombatState(CombatStates newState)
    {
        combatStates = newState;
    }  

    public void ChangeCharacterState(GolemCharacterStates newState)
    {
        golemState = newState;
    }

    public enum CombatStates
    {
        IDLE,
        LIGHTATTACK,
        HEAVYATTACK,
        BLOCK,
        DEAD
    }

    public enum GolemCharacterStates
    {
        IDLE,
        MOVING,
        ATTACKING,
        CASTING,
        ROLL,
        DEAD
    }
}
