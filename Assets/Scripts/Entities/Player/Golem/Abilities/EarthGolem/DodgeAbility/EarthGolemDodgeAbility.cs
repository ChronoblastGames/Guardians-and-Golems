using System.Collections;
using UnityEngine;

public class EarthGolemDodgeAbility : GolemDodgeAbilityBase
{
    public override IEnumerator Dodge(Vector3 interceptVec, float dodgeSpeed, float dodgeTime, AnimationCurve dodgeCurve)
    {
        float dodgeTimer = 0;

        while (dodgeTimer <= dodgeTime)
        {
            dodgeTimer += Time.deltaTime / dodgeTime;

            float dodgeCurrentSpeed = dodgeSpeed * dodgeCurve.Evaluate(dodgeTimer);

            golemPlayerController.characterController.Move((interceptVec * dodgeCurrentSpeed) * Time.deltaTime);

            yield return null;
        }

        golemPlayerController.StopDodge();
    }
}
