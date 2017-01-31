using System.Collections;
using UnityEngine;

public class Ability : GolemAbility 
{
    public override void CastAbility(Vector3 aimVec)
    {
        Quaternion newRotation = Quaternion.LookRotation(aimVec);

        GameObject newAbility = Instantiate(ability, transform.position + aimVec * spawnDistanceFromPlayer + new Vector3(0, 1, 0), newRotation) as GameObject;
        
        if (newAbility.GetComponent<BaseProjectileAbility>())
        {
            if (belongsToPlayerRed)
            {
                newAbility.GetComponent<BaseProjectileAbility>().belongsToPlayerRed = true;
            }
            else if (belongsToPlayerBlue)
            {
                newAbility.GetComponent<BaseProjectileAbility>().belongsToPlayerBlue = true;
            }
        }
    }
}
