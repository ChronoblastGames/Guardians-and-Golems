using System.Collections;
using UnityEngine;

public class EarthGolemDodgeAbility : GolemDodgeAbilityBase
{
    public override IEnumerator Dodge(Vector3 interceptVec, float dodgeSpeed, float dodgeTime, AnimationCurve dodgeCurve)
    {
        float dodgeTimer = 0;

        dodgeCollider.enabled = true;

        while (dodgeTimer <= dodgeTime)
        {
            dodgeTimer += Time.deltaTime / dodgeTime;

            float dodgeCurrentSpeed = dodgeSpeed * dodgeCurve.Evaluate(dodgeTimer);

            golemPlayerController.characterController.Move((interceptVec * dodgeCurrentSpeed) * Time.deltaTime);

            yield return null;
        }

        dodgeCollider.enabled = false;

        golemPlayerController.StopDodge();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (golemPlayerController.playerColor == PlayerTeam.RED)
        {
            if (other.gameObject.CompareTag("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(dodgeDamage, dodgeDamageType, dodgeEffect, effectStrength, effectTime, effectFrequency, gameObject);
                dodgeCollider.enabled = false;
            }
        }
        else if (golemPlayerController.playerColor == PlayerTeam.BLUE)
        {
            if (other.gameObject.CompareTag("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(dodgeDamage, dodgeDamageType, dodgeEffect, effectStrength, effectTime, effectFrequency, gameObject);
                dodgeCollider.enabled = false;
            }
        }
    }
}
