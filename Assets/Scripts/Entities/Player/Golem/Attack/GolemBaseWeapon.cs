using System.Collections;
using UnityEngine;

public class GolemBaseWeapon : MonoBehaviour
{
    private GolemPlayerController golemPlayerController;
    private WeaponCollider weapon;

    [Header("Weapon Collider Attributes")]
    public Collider weaponCollider;
    private Quaternion weaponCurrentRotation;   
    private Quaternion weaponSwingStart;
    private Quaternion weaponSwingEnd;
    private float swingSpeed;

    [Header("Weapon Quick Attack Attributes")]
    public DamageType meleeDamageType;
    public float quickAttackDamage;
    public float quickAttackSpeed;
    public float swingArc;

    [Header("Weapon State Attributes")]
    public bool isAttacking;
    public bool isSwinging;

    [Header("Weapon Debug")]
    public GameObject weaponIndicator;

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

        weapon = weaponCollider.GetComponent<WeaponCollider>();
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
                weaponIndicator.SetActive(false);
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
        weaponIndicator.SetActive(true);
        isSwinging = true;
        weapon.damageType = meleeDamageType;
        weapon.damageValue = quickAttackDamage;
    }
}
