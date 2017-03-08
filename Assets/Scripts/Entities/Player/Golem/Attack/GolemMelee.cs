using System.Collections;
using UnityEngine;

public class GolemMelee : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;
    private GlobalVariables globalVariables;
    private TimerClass attackTimer;

    private Animator golemState;

    [Header("Weapon Collider Attributes")]
    public Collider[] weaponCollider;

    [Header("Weapon Attack Attributes")]
    public DamageType[] meleeDamageType;

    public float[] attackDamage;

    public StatusEffect[] attackEffects;

    public float[] effectStrength;

    public float[] attackMovementSpeed;

    private float attack1Time;
    private float attack2Time;
    private float attack3Time;

    public AnimationCurve[] attackMovementCurves;

    public AnimationClip[] meleeAnimations;

    public bool isAttacking = false;

    private int attackCount;

    private void Start()
    {
        InitializeWeapon();
    }

    void FixedUpdate()
    {
        ManageAttacking();
    }

    void InitializeWeapon()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();

        globalVariables = GameObject.FindObjectOfType<GlobalVariables>();

        golemState = GetComponent<Animator>();

        for (int i = 0; i < weaponCollider.Length; i++)
        {
            weaponCollider[i].GetComponent<WeaponCollider>();
        }

        attackTimer = new TimerClass();
    }

    void ManageAttacking()
    {     
        if (isAttacking)
        {
            if (attackTimer.TimerIsDone() || golemState.GetCurrentAnimatorStateInfo(0).IsName("Melee.Finished"))
            {
                ResetAttack();
            }

            if (golemState.GetCurrentAnimatorStateInfo(0).IsName("Melee.Attack1"))
            {
                attack1Time += Time.fixedDeltaTime / meleeAnimations[0].length;

                float attackMovement = attackMovementSpeed[0] * attackMovementCurves[0].Evaluate(attack1Time);

                golemPlayerController.characterController.Move(transform.forward * attackMovement);
            }
            else if (golemState.GetCurrentAnimatorStateInfo(0).IsName("Melee.Attack2"))
            {
                attack2Time += Time.fixedDeltaTime / meleeAnimations[1].length;

                float attackMovement = attackMovementSpeed[1] * attackMovementCurves[1].Evaluate(attack2Time);

                golemPlayerController.characterController.Move(transform.forward * attackMovement);
            }
            else if (golemState.GetCurrentAnimatorStateInfo(0).IsName("Melee.Attack3"))
            {
                attack3Time += Time.fixedDeltaTime / meleeAnimations[2].length;

                float attackMovement = attackMovementSpeed[2] * attackMovementCurves[2].Evaluate(attack3Time);

                golemPlayerController.characterController.Move(transform.forward * attackMovement);
            }  
        }        
    }

    public void QueueAttack()
    {
        if (attackCount < meleeAnimations.Length)
        {
            attackCount++;

            switch(attackCount)
            {
                case 1:
                    Attack1();
                    break;

                case 2:
                    Attack2();
                    break;

                case 3:
                    Attack3();
                    break;
            }
        }
    }

    void Attack1()
    {
        golemPlayerController.canDodge = false;
        golemPlayerController.canMove = false;
        golemPlayerController.canUseAbilities = false;

        attackTimer.ResetTimer(meleeAnimations[0].length + 3f);
        golemState.SetTrigger("Attack");

        weaponCollider[0].GetComponent<WeaponCollider>().SetValues(meleeDamageType[0], attackDamage[0], attackEffects[0], effectStrength[0]);

        isAttacking = true;  
    }

    void Attack2()
    {
        attackTimer.ResetTimer(meleeAnimations[1].length + 3f);

        weaponCollider[1].GetComponent<WeaponCollider>().SetValues(meleeDamageType[1], attackDamage[1], attackEffects[1], effectStrength[1]);

        golemState.SetBool("Attack2", true);
    }

    void Attack3()
    {
        weaponCollider[2].GetComponent<WeaponCollider>().SetValues(meleeDamageType[2], attackDamage[2], attackEffects[2], effectStrength[2]);

        golemState.SetBool("Attack3", true);
    }

    void ResetAttack()
    {
        golemState.SetBool("Attack2", false);
        golemState.SetBool("Attack3", false);

        attackCount = 0;
        attack1Time = 0;
        attack2Time = 0;
        attack3Time = 0;
  
        golemPlayerController.canDodge = true;
        golemPlayerController.canMove = true;
        golemPlayerController.canUseAbilities = true;

        isAttacking = false;
    }

    public void EnableWeaponCollider(int weaponNumber)
    {
        weaponCollider[weaponNumber].enabled = true;
    }

    public void DisableWeaponCollider(int weaponNumber)
    {
        weaponCollider[weaponNumber].enabled = false;
    }
}
