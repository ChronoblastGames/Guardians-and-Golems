using System.Collections;
using UnityEngine;

public class ShardController : MonoBehaviour 
{
    private TimerClass shardTimer;

    [Header("Shard Attributes")]
    public DamageType damageType;
    public float damageValue;

    public StatusEffect statusEffect;

    [Header("Movement Attributes")]
    public float moveHeight;
    public float moveSpeed;
    private float t;

    private Vector3 targetPos;

    private bool canMove = false;

    private void Start()
    {
        InitializeShard();
    }

    private void Update()
    {
        MoveShard();
    }

    void InitializeShard()
    {
        shardTimer = new TimerClass();

        targetPos = transform.position + new Vector3(0, moveHeight, 0);

        canMove = true;
    }

    void MoveShard()
    {
        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, t);

            t += Time.deltaTime * moveSpeed;

            if (transform.position == targetPos)
            {
                canMove = false;
                t = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }
}
