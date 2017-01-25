using System.Collections;
using UnityEngine;

public enum DamageType {
    FIRE,
    ICE,
    WIND,
    EARTH
}

public abstract class GolemAbility : MonoBehaviour
{

    [Header("Ability Attributes")]
    public DamageType damageType;

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


