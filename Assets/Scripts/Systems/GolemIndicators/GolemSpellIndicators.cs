using System.Collections;
using UnityEngine;

public class GolemSpellIndicators : MonoBehaviour 
{
    private GolemInputManager golemInput;

    private float aimXAxis;
    private float aimZAxis;

    [Header("Golem Spell Indicator")]
    public GameObject spellIndicator;

    private void Start()
    {
        Initialize();
    }

    void Update () 
    {
        ManageAimIndicator();
	}

    void Initialize()
    {
        golemInput = GetComponent<GolemInputManager>();
    }

    void ManageAimIndicator()
    {
        aimXAxis = golemInput.aimXAxis;
        aimZAxis = golemInput.aimZAxis;

        spellIndicator.transform.position = golemInput.transform.position;

        if (aimXAxis != 0 || aimZAxis != 0)
        {
            spellIndicator.SetActive(true);

            Vector2 aimVector = new Vector2(aimXAxis, aimZAxis);
            float angle = Mathf.Atan2(aimZAxis, aimXAxis) * Mathf.Rad2Deg;

            angle -= 90f;

            spellIndicator.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            spellIndicator.SetActive(false);
        }
    }
}
