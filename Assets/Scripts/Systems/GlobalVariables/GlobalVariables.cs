using System.Collections;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    [Header("General Global Variables")]
    public float gameStartDelay;

    [Header("Golem Global Variables")]
	public float golemGlobalCooldown;
    [Space(10)]
    public float golemAttackFollowUpTime;
    [Space(10)]
    public float golemStaggerTime;
    [Space(10)]
    public float golemRecoveryTime;
    [Space(10)]
    public float golemRespawnTime;

    [Header("Guardian Global Variables")]
    public float guardianGlobalCooldown;

    [Header("Orb Global Variables")]
    public float spellOrbCoolDown;


    [Header("Command Values")]
    public float loseConduitCommandCost;
    public float winConduitCommandCost;
    [Space(10)]
    public float golemDeathCommandCost;
    public float golemKillCommandCost;
}
