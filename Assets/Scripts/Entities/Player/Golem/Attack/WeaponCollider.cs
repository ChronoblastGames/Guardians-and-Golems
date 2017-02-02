using System.Collections;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [Header("Collider Attributes")]

    public bool belongsToPlayerRed;
    public bool belongsToPlayerBlue;

    private Collider weaponCol;

    void Start()
    {
        InitializeDetection();
    }

    void InitializeDetection()
    {
        weaponCol = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (belongsToPlayerRed)
        {
            if (other.gameObject.layer == 9)
            {
                Debug.Log("Hit Player Blue");
                weaponCol.enabled = false;
            }
        }
        else if (belongsToPlayerBlue)
        {
            if (other.gameObject.layer == 8)
            {
                Debug.Log("Hit Player Red");
                weaponCol.enabled = false;
            }
        }
    }
}
