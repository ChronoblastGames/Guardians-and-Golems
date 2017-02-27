using System.Collections;
using UnityEngine;

public class BaseStaticAbility : GolemAbilityBase
{
    private TimerClass activeTimer;

    [Header("Ability Attributes")]
    public bool isRaising = false;
    public bool isLowering = false;
    public bool startCounting;

    private float t;

    private Vector3 activeVec;

    private void Update()
    {
        RaiseWall();

        ManageWall();
    }

    public override void InitializeAbility()
    {
        activeTimer = new TimerClass();

        activeVec = transform.position + new Vector3(0, abilityValues.raiseAmount, 0);

        isRaising = true;
    }

    void RaiseWall()
    {
        if (isRaising)
        {
            transform.position = Vector3.Lerp(transform.position, activeVec, t);

            t += Time.deltaTime * abilityValues.raiseSpeed;

            if (transform.position == activeVec)
            {
                isRaising = false;
                startCounting = true;
                activeTimer.ResetTimer(abilityValues.wallActiveTime);
                t = 0;
                activeVec = transform.position - new Vector3(0, abilityValues.raiseAmount, 0);
            }
        }

        if (isLowering)
        {
            transform.position = Vector3.Lerp(transform.position, activeVec, t);

            t += Time.deltaTime * abilityValues.raiseSpeed;

            if (transform.position == activeVec)
            {
                Destroy(gameObject);
            }
        }
    }

    void ManageWall()
    {
        if (startCounting)
        {
            if (activeTimer.TimerIsDone())
            {
                isLowering = true;
            }
        }    
    }
}
