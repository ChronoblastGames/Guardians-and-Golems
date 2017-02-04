using System.Collections;
using UnityEngine;

public class Ability : GolemAbility 
{
    public override void CastAbility(Vector3 aimVec, string teamColor)
    {
        Quaternion newRotation = Quaternion.LookRotation(aimVec);

        GameObject newAbility = Instantiate(ability, transform.position + new Vector3(0, 1, 0), newRotation) as GameObject;
        
        if (teamColor == "Red")
        {
            newAbility.layer = 11;
        }
        else if (teamColor == "Blue")
        {
            newAbility.layer = 12;
        }
    }
}
