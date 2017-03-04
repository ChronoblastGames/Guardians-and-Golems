using System.Collections;
using UnityEngine;

public class GolemMelee : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;
    private GlobalVariables globalVariables;
    private TimerClass attackTimer;
    private WeaponCollider weapon;

    private Animator golemState;

    [Header("Weapon Collider Attributes")]
    public Collider weaponCollider;

    [Header("Weapon Attack Attributes")]
    public DamageType meleeDamageType;

    public float[] attackDamage;

    public float[] attackMovement;

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

        weapon = weaponCollider.GetComponent<WeaponCollider>();

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
                Vector3 moveVec = transform.forward * attackMovement[0];

                golemPlayerController.characterController.Move(moveVec * Time.deltaTime);
            }
            else if (golemState.GetCurrentAnimatorStateInfo(0).IsName("Melee.Attack2"))
            {
                Vector3 moveVec = transform.forward * attackMovement[1];

                golemPlayerController.characterController.Move(moveVec * Time.deltaTime);
            }
            else if (golemState.GetCurrentAnimatorStateInfo(0).IsName("Melee.Attack3"))
            {
                Vector3 moveVec = transform.forward * attackMovement[2];

                golemPlayerController.characterController.Move(moveVec * Time.deltaTime);
            }

            golemPlayerController.canDodge = false;
            golemPlayerController.canMove = false;
            golemPlayerController.canRotate = false;
            golemPlayerController.canUseAbilities = false;
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
        attackTimer.ResetTimer(meleeAnimations[0].length + 2f);
        golemState.SetTrigger("Attack");
        isAttacking = true;  
    }

    void Attack2()
    {
        attackTimer.ResetTimer(meleeAnimations[1].length + 2f);
        golemState.SetBool("Attack2", true);
    }

    void Attack3()
    {
        golemState.SetBool("Attack3", true);
    }

    void ResetAttack()
    {
        golemState.SetBool("Attack2", false);
        golemState.SetBool("Attack3", false);
        attackCount = 0;
        isAttacking = false;

        golemPlayerController.canDodge = true;
        golemPlayerController.canMove = true;
        golemPlayerController.canRotate = true;
        golemPlayerController.canUseAbilities = true;

        Debug.Log("Resetting Attack");
    }
}
