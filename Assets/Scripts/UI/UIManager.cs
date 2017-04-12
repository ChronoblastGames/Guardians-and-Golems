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
    private CommandManager commandManager;

    private GolemResources redGolemResources;
    private GolemResources blueGolemResources;

    [Header("Golem HealthBar")]
    public Image redGolemHealthBar;
    public Image blueGolemHealthBar;

    [Header("Command UI")]
    public Image redTeamCommandImage;
    public Image blueTeamCommandImage;

    [Header("Crystal UI")]
    public List<Image> guardianCrystalLeft;
    public List<Image> guardianCrystalRight;
    [Space(10)]
    public List<Image> golemCrystalRed;
    public List<Image> golemCrystalBlue;
    

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

        commandManager = GameObject.FindGameObjectWithTag("CommandManager").GetComponent<CommandManager>();

        redGolemResources = GameObject.FindGameObjectWithTag("GolemRed").GetComponent<GolemResources>();
        blueGolemResources = GameObject.FindGameObjectWithTag("GolemBlue").GetComponent<GolemResources>();

        floatingTextManager = GetComponent<FloatingTextManager>();
    }

    void Update()
    {
        ManageHealthBars();
        ManageClock();
        ManageCrystals();
        ManageCommand();
    }

    void ManageHealthBars()
    {
        redGolemHealthBar.fillAmount = redGolemResources.currentHealth / redGolemResources.maxHealth;
        blueGolemHealthBar.fillAmount = blueGolemResources.currentHealth / blueGolemResources.maxHealth;
    }

    void ManageCrystals()
    {
        int redTeamGuardianCrystal = crystalManager.redTeamGuardianCurrentCrystalCount;
        int blueTeamGuardianCrystal = crystalManager.blueTeamGuardianCurrentCrystalCount;

        guardianCrystalLeft[redTeamGuardianCrystal].fillAmount = crystalManager.redTeamGuardianRefill / 1;
        guardianCrystalRight[blueTeamGuardianCrystal].fillAmount = crystalManager.blueTeamGuardianRefill / 1;

        int redTeamGolemCrystal = crystalManager.redTeamGolemCurrentCrystalCount;
        int blueTeamGolemCrystal = crystalManager.blueTeamGolemCurrentCrystalCount;

        golemCrystalRed[redTeamGolemCrystal].fillAmount = crystalManager.redTeamGolemRefill / 1;
        golemCrystalBlue[blueTeamGolemCrystal].fillAmount = crystalManager.blueTeamGolemRefill / 1;
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

    void ManageCommand()
    {
        redTeamCommandImage.fillAmount = commandManager.redTeamCurrentCommand / commandManager.maxCommand;

        blueTeamCommandImage.fillAmount = commandManager.blueTeamCurrentCommand / commandManager.maxCommand;
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

    public void SetCrystalColor(int crystalNumber, PlayerTeam playerTeam)
    {
        if (playerTeam == PlayerTeam.RED)
        {
            guardianCrystalLeft[crystalNumber].color = Color.yellow;
        }
        else if (playerTeam == PlayerTeam.BLUE)
        {
            guardianCrystalRight[crystalNumber].color = Color.blue;
        }
    }

    public void ResetCrystalUI(int crystalNumber, PlayerTeam teamColor, PlayerType playerType)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                if (playerType == PlayerType.GOLEM)
                {
                    golemCrystalRed[crystalNumber].fillAmount = 0;
                }
                else if (playerType == PlayerType.GUARDIAN)
                {
                    guardianCrystalLeft[crystalNumber].fillAmount = 0;
                }
                break;

            case PlayerTeam.BLUE:
                if (playerType == PlayerType.GOLEM)
                {
                    golemCrystalBlue[crystalNumber].fillAmount = 0;
                }
                else if (playerType == PlayerType.GUARDIAN)
                {
                    guardianCrystalRight[crystalNumber].fillAmount = 0;
                }
                break;
        }
    }
}
