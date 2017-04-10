using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FloatingDamageTextType
{
    DAMAGE,
    HEAL,
    CRITICAL,
    SHIELD,
    NONE
}

public class UIManager : MonoBehaviour
{
    private FloatingTextManager floatingTextManager;

    private CrystalManager crystalManager;

    private GolemResources redGolemResources;
    private GolemResources blueGolemResources;

    [Header("Golem HealthBar")]
    public Image redGolemHealthBar;
    public Image blueGolemHealthBar;

    [Header("Crystal UI")]
    public List<Image> crystalLeftTeam;
    public List<Image> crystalRightTeam;

    [Header("UI Clock")]
    public Text timeText;
    public bool isCountingDown = true;
    public bool isCountingUp = false;

    void Start()
    {
        UISetup();
    }

    void UISetup()
    {
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        redGolemResources = GameObject.FindGameObjectWithTag("GolemRed").GetComponent<GolemResources>();
        blueGolemResources = GameObject.FindGameObjectWithTag("GolemBlue").GetComponent<GolemResources>();

        floatingTextManager = GetComponent<FloatingTextManager>();
    }

    void Update()
    {
        ManageHealthBars();
        ManageClock();
        ManageCrystals();
    }

    void ManageHealthBars()
    {
        redGolemHealthBar.fillAmount = redGolemResources.currentHealth / redGolemResources.maxHealth;
        blueGolemHealthBar.fillAmount = blueGolemResources.currentHealth / blueGolemResources.maxHealth;
    }

    void ManageCrystals()
    {
        int redTeamCurrentCrystal = crystalManager.redTeamCurrentCrystalCount;
        int blueTeamCurrentCrystal = crystalManager.blueTeamCurrentCrystalCount;

        crystalLeftTeam[redTeamCurrentCrystal].fillAmount = crystalManager.redTeamRefill / 1;
        crystalRightTeam[blueTeamCurrentCrystal].fillAmount = crystalManager.blueTeamRefill / 1;
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

    public void RequestDamageText(float textValue, Transform textPosition, FloatingDamageTextType effectType)
    {
        if (textValue > 0)
        {
            int displayValue = (int)textValue;

            floatingTextManager.CreateDamageText(displayValue, textPosition, effectType);
        }     
    }

    public void RequestStatusText(float effectStrength, Transform textPos, StatusEffect statusEffect)
    {
        floatingTextManager.CreateStatusText(effectStrength, textPos, statusEffect);
    }

    public void ResetCrystalUI(int crystalNumber, PlayerTeam teamColor)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                crystalLeftTeam[crystalNumber].fillAmount = 0;
                break;

            case PlayerTeam.BLUE:
                crystalRightTeam[crystalNumber].fillAmount = 0;
                break;
        }
    }
}
