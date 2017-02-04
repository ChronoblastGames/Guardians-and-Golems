using System.Collections;
using UnityEngine;

public class GolemCombatStateMachine : MonoBehaviour 
{
    [Header("Current Combat State")]
    public CombatStates combatStates;

    private void Start()
    {
        combatStates = CombatStates.IDLE;
    }

    public enum CombatStates
    {
        IDLE,
        LIGHTATTACK,
        HEAVYATTACK,
        ROLL,
        BLOCK,
    }

    public void ChangeState(CombatStates newState)
    {
        combatStates = newState;
    }  
}
