using System.Collections;
using UnityEngine;

[System.Serializable]
public class BaseProjectileAbility : MonoBehaviour
{
    private Rigidbody myRB;
    private GameObject trailRenderer;

    [Header("Ability Values")]
    public AbilityValues abilityValues;

    private void Start()
    {
        AbilitySetup();
    }

    void AbilitySetup()
    {
        myRB = GetComponent<Rigidbody>();

        trailRenderer = transform.GetChild(0).gameObject;

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
            other.gameObject.GetComponent<GolemHealth>().TakeDamage(abilityValues.damageAmount, abilityValues.damageType);
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
