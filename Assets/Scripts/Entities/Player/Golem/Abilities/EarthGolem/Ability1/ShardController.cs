using System.Collections;
using UnityEngine;

public class ShardController : MonoBehaviour 
{
    [Header("Shard Attributes")]
    public DamageType damageType;
    public float damageValue;

    public StatusEffect statusEffect;

    private void OnTriggerEnter(Collider other)
    {
       
    }
}
