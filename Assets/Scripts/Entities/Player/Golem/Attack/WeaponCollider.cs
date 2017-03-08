using System.Collections;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("Collider Attributes")]
    private DamageType damageType;

    private float damageValue;

    private Collider weaponCol;

    void Start()
    {
        InitializeDetection();
    }

    void InitializeDetection()
    {
        weaponCol = GetComponent<Collider>();
    }

    public void SetValues(DamageType attackType, float attackDamage)
    {
        damageType = attackType;
        damageValue = attackDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("GolemRed"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(damageValue, damageType, gameObject);
                weaponCol.enabled = false;
            }
        }
        else if (gameObject.layer == LayerMask.NameToLayer("GolemBlue"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("GolemRed"))
            {
                other.gameObject.GetComponent<GolemResources>().TakeDamage(damageValue, damageType, gameObject);
                weaponCol.enabled = false;
            }
        } 
    }
}
