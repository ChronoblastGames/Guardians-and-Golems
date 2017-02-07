using System.Collections;
using UnityEngine;

public class BaseStaticAbility : MonoBehaviour 
{
    private TimerClass activeTimer;

    [Header("Ability Attributes")]
    public AbilityValues abilityValues;

    private float t;

    public bool isRaising;
    public bool startCounting;

    private Vector3 activeVec;

    private void Update()
    {
        RaiseWall();

        ManageWall();
    }

    public void InitializeWall()
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
                activeTimer.ResetTimer(abilityValues.activeTime);
            }
        }
    }

    void ManageWall()
    {
        if (startCounting)
        {
            if (activeTimer.TimerIsDone())
            {
                Destroy(gameObject);
            }
        }    
    }
}
