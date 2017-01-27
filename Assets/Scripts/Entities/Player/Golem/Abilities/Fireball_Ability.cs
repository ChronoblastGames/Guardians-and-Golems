﻿using System.Collections;
using UnityEngine;

[System.Serializable]
public class Fireball_Ability : GolemAbility
{
    private Rigidbody myRB;

    [Header("Projectile Attributes")]
    public GameObject projectileVisual;

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
