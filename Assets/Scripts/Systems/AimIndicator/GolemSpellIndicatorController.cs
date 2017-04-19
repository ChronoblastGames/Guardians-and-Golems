using System.Collections;
using UnityEngine;

public enum IndicatorType
{
    NONE,
    ARROW,
    CONE,
    CIRCLE,
    POINT
}

public class GolemSpellIndicatorController : MonoBehaviour 
{
    private GolemInputController golemInput;

    private float aimXAxis;
    private float aimZAxis;

    [Header("Golem Spell Indicator")]
    public GameObject holderObject;

    private IndicatorType currentSpellIndicatorType;

    private GameObject currentIndicator;

    private bool indicatorActive = false;
    private bool canRotate = false;

    [Header("Golem Spell Materials")]
    public GameObject[] aimIndicatorArray;

    private void Start()
    {
        Initialize();
    }

    void Update () 
    {
        ManageRotation();
	}

    void Initialize()
    {
        golemInput = GetComponent<GolemInputController>();

        SetNewIndicator(IndicatorType.ARROW, 0);
    }

    public void SetNewIndicator(IndicatorType desiredIndicatorType, float indicatorSize)
    {
        if (currentSpellIndicatorType != desiredIndicatorType)
        {
            switch (desiredIndicatorType)
            {
                case IndicatorType.ARROW:
                    if (currentIndicator != null)
                    {
                        currentIndicator.SetActive(false);
                    }

                    currentIndicator = aimIndicatorArray[0];

                    currentSpellIndicatorType = IndicatorType.ARROW;

                    canRotate = true;
                    indicatorActive = true;
                    break;

                case IndicatorType.CIRCLE:
                    if (currentIndicator != null)
                    {
                        currentIndicator.SetActive(false);
                    }

                    currentIndicator = aimIndicatorArray[1];

                    currentIndicator.GetComponent<Projector>().orthographicSize = indicatorSize;

                    currentSpellIndicatorType = IndicatorType.CIRCLE;

                    canRotate = false;
                    indicatorActive = true;
                    break;

                case IndicatorType.CONE:
                    if (currentIndicator != null)
                    {
                        currentIndicator.SetActive(false);
                    }

                    currentIndicator = aimIndicatorArray[2];

                    currentSpellIndicatorType = IndicatorType.CONE;

                    canRotate = true;
                    indicatorActive = true;
                    break;
            }
        }       
    }

    void ManageRotation()
    {
        if (indicatorActive)
        {
            aimXAxis = golemInput.aimXAxis;
            aimZAxis = golemInput.aimZAxis;

            holderObject.transform.position = golemInput.transform.position + new Vector3(0, 10, 0);

            if (aimXAxis != 0 || aimZAxis != 0 && canRotate)
            {
                currentIndicator.SetActive(true);

                Vector2 aimVector = new Vector2(aimXAxis, aimZAxis);
                float angle = Mathf.Atan2(aimZAxis, aimXAxis) * Mathf.Rad2Deg;

                holderObject.transform.localRotation = Quaternion.Euler(90, 0, angle);
            }
            else if (currentSpellIndicatorType == IndicatorType.CIRCLE)
            {
                currentIndicator.SetActive(true);
            }
            else
            {
                currentIndicator.SetActive(false);
            }
        }     
    }
}
