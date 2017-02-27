using System.Collections;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    [Header("General Global Variables")]
    public float spawnDelayTimer;

    [Header("Golem Global Variables")]
	public float golemGlobalCooldown;

    public float golemAttackFollowUpTime;

    public float golemStaggerTime;
    public float golemRecoveryTime;

    [Header("Guardian Global Variables")]
    public float guardianGlobalCooldown;

    [Header("Orb Global Variables")]
    public float spellOrbCoolDown;
}
