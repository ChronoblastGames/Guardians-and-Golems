using System.Collections;
using UnityEngine;

public class GuardianSpellIndicators : MonoBehaviour 
{
    private GuardianInputManager guardianInput;

    private float aimXAxis;
    private float aimZAxis;

    [Header("Golem Spell Indicator")]
    public GameObject spellIndicator;

    private void Start()
    {
        Initialize();
    }

    void Update()
    {
        ManageAimIndicator();
    }

    void Initialize()
    {
        guardianInput = GetComponent<GuardianInputManager>();
    }

    void ManageAimIndicator()
    {
        aimXAxis = guardianInput.aimXAxis;
        aimZAxis = guardianInput.aimZAxis;

        spellIndicator.transform.position = guardianInput.transform.position;

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
