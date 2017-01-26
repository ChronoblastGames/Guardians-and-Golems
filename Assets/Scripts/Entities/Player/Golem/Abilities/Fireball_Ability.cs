using System.Collections;
using UnityEngine;

public class Fireball_Ability : GolemAbility
{
    [Header("Ability Visual")]
    public GameObject fireBallVisual;

    public override void CastAbility()
    {
        Debug.Log("Go Fireball");
    }
}
