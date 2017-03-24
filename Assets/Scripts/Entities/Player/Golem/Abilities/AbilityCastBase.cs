using System.Collections;
using UnityEngine;

public class AbilityCastBase : MonoBehaviour 
{
    private TimerClass abilityTimer;

    public AbilityValues abilityValues;

    public bool isAbilityActive = false;

	public virtual void InitializeAbility()
    {

    }
}
