using System.Collections;
using UnityEngine;

public class GolemBaseWeapon : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;

    private TimerClass attackTimer;

    private Animator golemStateMachine;

    private WeaponCollider weapon;

    [Header("Weapon Collider Attributes")]
    public Collider weaponCollider;

    [Header("Weapon Attack Attributes")]
    public DamageType meleeDamageType;
    public float attackDamage;
    public int attackCount = 1;

    [Header("Weapon Debug")]
    public GameObject weaponIndicator;

    private void Start()
    {
        InitializeWeapon();
    }

    void Update()
    {
        ManageAttacking();
    }

    void InitializeWeapon()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();

        golemStateMachine = transform.GetChild(0).GetComponent<Animator>();

        weapon = weaponCollider.GetComponent<WeaponCollider>();

        attackTimer = new TimerClass();
    }

    void ManageAttacking()
    {
        if (attackTimer.TimerIsDone())
        {
            ResetAttack();
        }
    }

    public void Attack()
    {
        switch(attackCount)
        {
            case 1:
                attackCount++;
                break;
            case 2:
                attackCount++;
                break;
            case 3:
                attackCount++;
                break;
        }
    }

    void ResetAttack()
    {
        attackCount = 1;
    }
}
