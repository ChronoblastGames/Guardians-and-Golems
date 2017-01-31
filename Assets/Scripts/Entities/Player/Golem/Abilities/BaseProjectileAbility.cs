using System.Collections;
using UnityEngine;

[System.Serializable]
public class BaseProjectileAbility : GolemAbility
{
    private Rigidbody myRB;

    public bool belongsToRed;
    public bool belongsToBlue;

    private void Start()
    {
        FireballSetup();

        UseAbility();
    }

    void FireballSetup()
    {
        myRB = GetComponent<Rigidbody>();       
    }

    void UseAbility()
    {
        myRB.AddForce(transform.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GolemHealth>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
