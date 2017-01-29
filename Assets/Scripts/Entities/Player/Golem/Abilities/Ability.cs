using System.Collections;
using UnityEngine;

public class Ability : GolemAbility 
{
    public override void CastAbility()
    {
        GameObject newAbility = Instantiate(ability, transform.position + transform.forward * spawnDistanceFromPlayer + new Vector3(0, 1, 0), transform.rotation) as GameObject;
    }
}
