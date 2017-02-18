using System.Collections;
using UnityEngine;

[System.Serializable]
public class BaseProjectileAbility : MonoBehaviour
{
    private TimerClass projectileTimer;

    private Rigidbody myRB;
    private GameObject trailRenderer;

    [Header("Ability Values")]
    public AbilityValues abilityValues;

    public bool hasTime;

    private void Update()
    {
        CheckTimer();
    }

    void CheckTimer()
    {
        if (hasTime)
        {
            if (projectileTimer.TimerIsDone())
            {
                HideSelf();
            }
        }
    }

    public void InitializeAbility()
    {
        myRB = GetComponent<Rigidbody>();

        trailRenderer = transform.GetChild(0).gameObject;

        if (abilityValues.projectileActiveTime > 0)
        {
            projectileTimer = new TimerClass();

            projectileTimer.ResetTimer(abilityValues.projectileActiveTime);

            hasTime = true;
        }

        UseAbility();
    }

    void UseAbility()
    {
        myRB.AddForce(transform.forward * abilityValues.projectileSpeed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolemRed") || other.gameObject.CompareTag("GolemBlue"))
        {
            other.gameObject.GetComponent<GolemResources>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType);
            HideSelf();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        HideSelf();
    }

    public void HideSelf ()
    {
       trailRenderer.transform.parent = null;
       gameObject.SetActive(false);
       Invoke("DestroyTrail", 1);
    }

    void DestroyTrail ()
    {
        Destroy(trailRenderer.gameObject);
        Destroy(gameObject);
    }
}
