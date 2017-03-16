using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private FloatingTextManager floatingTextManager;

    private GolemResources redGolemResources;
    private GolemResources blueGolemResources;

    [Header("Golem HealthBar")]
    public Image redGolemHealthBar;
    public Image blueGolemHealthBar;

    [Header("Ingame Time")]
    public Text timeText;
    public bool isCountingDown = true;
    public bool isCountingUp = false;

    void Start()
    {
        UISetup();
    }

    void UISetup()
    {
        redGolemResources = GameObject.FindGameObjectWithTag("GolemRed").GetComponent<GolemResources>();
        blueGolemResources = GameObject.FindGameObjectWithTag("GolemBlue").GetComponent<GolemResources>();

        floatingTextManager = GetComponent<FloatingTextManager>();
    }

    void Update()
    {
        ManageHealthBars();
        ManageClock();
    }

    void ManageHealthBars()
    {
        redGolemHealthBar.fillAmount = redGolemResources.currentHealth / redGolemResources.maxHealth;
        blueGolemHealthBar.fillAmount = blueGolemResources.currentHealth / blueGolemResources.maxHealth;
    }

    void ManageClock()
    {
        if (isCountingDown)
        {

        }
        else if (isCountingUp)
        {

        }
    }

    public void RequestDamageText(float damageValue, Transform textPosition)
    {
        int damageAmount = (int)damageValue;

        floatingTextManager.CreateDamageText(damageAmount, textPosition);
    }
}
