using System.Collections;
using UnityEngine;

public class BaseEngageAbility : MonoBehaviour 
{
    [Header("Ability Attributes")]
    public GameObject golemPlayer;

    public Vector3 interceptVec;

    public bool isEngauging;

    public AbilityValues abilityValues;

	void Start () 
    {
        AbilitySetup();
	}
	
	void Update () 
    {
        Enguage();
	}

    void AbilitySetup()
    {
        golemPlayer = transform.parent.parent.gameObject;
        isEngauging = true;
    }

    void Enguage()
    {
        if (isEngauging)
        {

        }
    }

}
