using System.Collections;
using UnityEngine;

public class WeaponEffectController : MonoBehaviour
{
    [Header("Weapon Effect Attributes")]
    public float stayTime;

    void Start()
    {
        StartCoroutine(LastUntil(stayTime));
    }

    private IEnumerator LastUntil(float stayTime)
    {
        yield return new WaitForSeconds(stayTime);

        Destroy(gameObject);

        yield return null;
    }
}
