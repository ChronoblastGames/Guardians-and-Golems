using System.Collections;
using UnityEngine;

public class GuardianSpellIndicators : MonoBehaviour 
{
    private GuardianInputManager guardianInput;

    private float aimXAxis;
    private float aimZAxis;

    [Header("Golem Spell Indicator")]
    public IndicatorType spellIndicatorType;

    public GameObject spellIndicator;

    public bool isSpellIndicatorEnabled;

    [Header("Golem Spell Materials")]
    public Material spellIndicatorCircle;
    public Material spellIndicatorPoint;
    public Material spellIndicatorArrow;
    public Material spellIndicatorCone;

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
        if (isSpellIndicatorEnabled)
        {
            switch (spellIndicatorType)
            {
                case IndicatorType.ARROW:

                    aimXAxis = guardianInput.aimXAxis;
                    aimZAxis = guardianInput.aimZAxis;

                    spellIndicator.transform.position = guardianInput.transform.position + new Vector3(0, 10, 0);

                    if (aimXAxis != 0 || aimZAxis != 0)
                    {
                        spellIndicator.SetActive(true);

                        Vector2 aimVector = new Vector2(aimXAxis, aimZAxis);
                        float angle = Mathf.Atan2(aimZAxis, aimXAxis) * Mathf.Rad2Deg;

                        angle += 180f;

                        spellIndicator.transform.localRotation = Quaternion.Euler(90, 0, angle);
                    }
                    else
                    {
                        spellIndicator.SetActive(false);
                    }

                    break;

                case IndicatorType.CIRCLE:
                    break;

                case IndicatorType.CONE:
                    break;

                case IndicatorType.POINT:
                    break;

                case IndicatorType.NONE:
                    break;
            }

        }
    }
}
