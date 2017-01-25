using System.Collections;
using UnityEngine;

public abstract class GolemAbility : MonoBehaviour
{
    [Header("Ability Attributes")]
    public string damageType;

    [Header("Range Attributes")]
    public float spawnDistanceFromPlayer;
    public float rangeDistance;

    public bool isMelee;
    public bool isRanged;

    [Header("Use Attributes")]
    public float healthCost;
    public float manaCost;

    [Header("Has Effect")]
    public bool canStun;
    public bool canSlow;
    public bool canDrainHealth;
    public bool canDrainMana;
    public bool canBlind;

    public virtual void CastAbility()
    {
        Debug.Log("Base Method Called");
    }

}
