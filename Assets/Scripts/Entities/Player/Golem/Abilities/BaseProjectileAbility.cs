using System.Collections;
using UnityEngine;

[System.Serializable]
public class BaseProjectileAbility : GolemAbility
{
    private Rigidbody myRB;

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
        if (other.gameObject.layer == 8)
        {
            Debug.Log("Hit Player");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
