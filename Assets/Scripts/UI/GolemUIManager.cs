using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GolemUIManager : MonoBehaviour
{
    private GolemInputManager golemInput;

    [Header("Golem Aim Indicator")]
    public GameObject aimIndicatorSprite;

    private float aimXAxis;
    private float aimZAxis;

    [Header("Golem HealthBar")]
    public Image healthBarImage;

    [Header("Golem ManaBar")]
    public Image manaBarImage;

    void Start()
    {
        UISetup();
    }

    void UISetup()
    {
        golemInput = transform.parent.GetComponent<GolemInputManager>();
    }

    void Update()
    {
        ManageAimIndicator();
    }

    void ManageAimIndicator()
    {
        aimXAxis = golemInput.aimXAxis;
        aimZAxis = golemInput.aimZAxis;

        aimIndicatorSprite.transform.position = golemInput.transform.position;

        if (aimXAxis != 0 || aimZAxis != 0)
        {
            aimIndicatorSprite.SetActive(true);

            Vector2 aimVector = new Vector2(aimXAxis, aimZAxis);
            float angle = Mathf.Atan2(aimZAxis, aimXAxis) * Mathf.Rad2Deg;

            angle -= 90f;

            aimIndicatorSprite.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            aimIndicatorSprite.SetActive(false);
        }
    }
}
