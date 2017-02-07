using System.Collections;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("Collider Attributes")]
    public DamageType damageType;

    private Collider weaponCol;

    void Start()
    {
        InitializeDetection();
    }

    void InitializeDetection()
    {
        weaponCol = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != gameObject.layer)
        {
            other.gameObject.GetComponent<GolemHealth>().TakeDamage(0, damageType);
            weaponCol.enabled = false;
        }
    }
}
