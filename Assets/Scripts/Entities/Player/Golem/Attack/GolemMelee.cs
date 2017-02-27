using System.Collections;
using UnityEngine;

public class GolemMelee : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;
    private GlobalVariables globalVariables;

    private TimerClass attackTimer;

    private Animator golemState;

    private WeaponCollider weapon;

    [Header("Weapon Collider Attributes")]
    public Collider weaponCollider;

    [Header("Weapon Attack Attributes")]
    public DamageType meleeDamageType;

    public float attackDamage;

    private int attackCount = 0;

    public bool isAttacking = false;

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

        globalVariables = GameObject.FindObjectOfType<GlobalVariables>();

        golemState = GetComponent<Animator>();

        weapon = weaponCollider.GetComponent<WeaponCollider>();

        attackTimer = new TimerClass();
    }

    void ManageAttacking()
    {
        if (isAttacking)
        {
            if (attackTimer.TimerIsDone())
            {
                ResetAttack();
            }
        }    
    }

    public void Attack()
    {
        switch(attackCount)
        {
            case 0:
                attackCount++;
                attackTimer.ResetTimer(globalVariables.golemAttackFollowUpTime);
                golemState.SetTrigger("Attack1");
                isAttacking = true;
                break;
            case 1:
                attackCount++;
                attackTimer.ResetTimer(globalVariables.golemAttackFollowUpTime);
                golemState.SetTrigger("Attack2");
                break;
            case 2:
                attackCount++;
                golemState.SetTrigger("Attack3");
                ResetAttack();
                break;
        }
    }

    void ResetAttack()
    {
        attackCount = 1;
        isAttacking = false;
    }
}
