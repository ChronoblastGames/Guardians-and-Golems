using System.Collections;
using UnityEngine;

[System.Serializable]
public class BaseProjectileAbility : GolemAbility
{
    private Rigidbody myRB;

    public bool canDealDamage;

    GameObject trail;

    private void Start()
    {
        FireballSetup();

        UseAbility();

        trail = transform.GetChild(0).gameObject;
    }

    void FireballSetup()
    {
        myRB = GetComponent<Rigidbody>();       
    }

    void UseAbility()
    {
        myRB.AddForce(transform.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GolemHealth>().TakeDamage(damageAmount);
            HideSelf();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        HideSelf();
    }

    public void HideSelf ()
    {
       trail.transform.parent = null;
       gameObject.SetActive(false);
       Invoke("DestroyTrail", 1);
    }

    public void DestroyTrail ()
    {
        Destroy(trail);
        Destroy(gameObject);
    }
}
