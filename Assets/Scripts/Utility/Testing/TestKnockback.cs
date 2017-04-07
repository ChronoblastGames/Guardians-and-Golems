using System.Collections;
using UnityEngine;

public class TestKnockback : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolemRed"))
        {
            other.gameObject.GetComponent<GolemResources>().TakeDamage(0, DamageType.NONE, StatusEffect.KNOCKBACK, 15, 2, 0, gameObject);
        }
    }
}
