using System.Collections;
using UnityEngine;

public class EarthGolemAbility3 : GolemAbilityBase
{
    [Header("Ability Attributes")]
    public LayerMask conduitMask;

    public override void InitializeAbility()
    {
        CheckArea();
    }

    void CheckArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, abilityValues.zoneRadius, conduitMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
            {
                Debug.Log(hitColliders[i].gameObject.name);
                hitColliders[i].GetComponent<OrbController>().DisableOrb(abilityValues.effectTime, PlayerTeam.RED);
            }
            else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                hitColliders[i].GetComponent<OrbController>().DisableOrb(abilityValues.effectTime, PlayerTeam.BLUE);
            }
        }
    }
}
