using System.Collections;
using UnityEngine;

public class GolemDodgeAbilityBase : MonoBehaviour
{
    [HideInInspector]
    public GolemPlayerController golemPlayerController;
    [HideInInspector]
    public Collider dodgeCollider;

    [Header("Player Dodge Attributes")]
    public AnimationCurve dodgeCurve;

    private float dodgeTimer;

    public float dodgeTime;

    public float dodgeSpeed;

    [Header("Player Dodge Damage Attributes")]
    public DamageType dodgeDamageType;

    public float dodgeDamage;

    public float effectStrength;
    public float effectTime;
    public float effectFrequency;

    public StatusEffect dodgeEffect;

    private void Start()
    {
        golemPlayerController = transform.parent.parent.GetComponent<GolemPlayerController>();

        dodgeCollider = GetComponent<Collider>();
    }

    public virtual void StartDodge(Vector3 interceptVec)
    {
        StartCoroutine(Dodge(interceptVec, dodgeSpeed, dodgeTime, dodgeCurve));
    }

    public virtual IEnumerator Dodge(Vector3 interceptVec, float dodgeSpeed, float dodgeTime, AnimationCurve dodgeCurve)
    {
        return null;
    }

    public virtual void EndDodge()
    {
        Debug.Log("Base End Dodge Called, Error");
    }
}
