using System.Collections;
using UnityEngine;

public class Fireball_Ability : GolemAbility
{
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
