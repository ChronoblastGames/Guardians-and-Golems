using System.Collections;
using UnityEngine;

public class GolemBaseWeapon : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;

    [Header("Weapon Collider Attributes")]
    public Collider weaponCollider;
    private Quaternion weaponCurrentRotation;   
    private Quaternion weaponSwingStart;
    private Quaternion weaponSwingEnd;
    private float swingSpeed;

    [Header("Weapon Quick Attack Attributes")]
    public MeleeDamageType meleeDamageType;
    public float quickAttackDamage;
    public float quickAttackSpeed;
    public float swingArc;

    [Header("Weapon State Attributes")]
    public bool isAttacking;
    public bool isSwinging;

    private void Start()
    {
        InitializeWeapon();
    }

    private void Update()
    {
        ManageSwinging();
    }

    void InitializeWeapon()
    {
        golemPlayerController = GetComponent<GolemPlayerController>();
    }

    void ManageSwinging()
    {
        if (isSwinging)
        {
            weaponCollider.transform.localRotation = Quaternion.Slerp(weaponCollider.transform.localRotation, weaponSwingEnd, Time.deltaTime * swingSpeed);

            if (weaponCollider.transform.localRotation == weaponSwingEnd)
            {
                isSwinging = false;
                weaponCollider.enabled = false;
                weaponCollider.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void QuickAttack()
    {
        weaponCurrentRotation = Quaternion.Euler(0, transform.localRotation.y, 0);
        weaponSwingStart = Quaternion.Euler(0, swingArc / 2, 0);
        weaponSwingEnd = Quaternion.Euler(0, -swingArc / 2, 0);
        weaponSwingStart *= weaponCurrentRotation;
        weaponSwingEnd *= weaponCurrentRotation;
        weaponCollider.transform.localRotation = weaponSwingStart;
        swingSpeed = quickAttackSpeed;
        weaponCollider.enabled = true;
        isSwinging = true;
    }

    public void HeavyAttack()
    {

    }

    public enum MeleeDamageType
    {
        SLASH,
        STAB,
        SMASH,
        NONE,
    }


}
