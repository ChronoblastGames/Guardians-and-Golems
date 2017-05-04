using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Golem UI")]
    public GameObject redGolemUI;
    public GameObject blueGolemUI;

    private GameObject redGolem;
    private GameObject blueGolem;

    [Header("Command UI")]
    public Image redTeamCommandImage;
    public Image blueTeamCommandImage;

    [Header("Crystal UI")]
    public List<Image> guardianCrystalLeft;
    public List<Image> guardianCrystalRight;
    [Space(10)]
    public List<Image> golemCrystalRed;
    public List<Image> golemCrystalBlue;
    
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

        redGolem = redGolemResources.gameObject;
        blueGolem = blueGolemResources.gameObject;

        floatingTextManager = GetComponent<FloatingTextManager>();
    }

    void Update()
    {
        ManageHealthBars();
        ManageCrystals();
        ManageCommand();
        ManageGolemUI();
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

    void ManageCommand()
    {
        redTeamCommandImage.fillAmount = commandManager.redTeamCurrentCommand / commandManager.maxCommand;

        blueTeamCommandImage.fillAmount = commandManager.blueTeamCurrentCommand / commandManager.maxCommand;
    }

    void ManageGolemUI()
    {
        Vector3 redGolemPos = redGolem.transform.position;
        redGolemPos.y = 1;

        redGolemUI.transform.position = redGolemPos;

        Vector3 blueGolemPos = blueGolem.transform.position;
        blueGolemPos.y = 1;

        blueGolemUI.transform.position = blueGolemPos;
    }

    public void RequestDamageText(float textValue, Transform textPosition, FloatingDamageSubTextType effectType)
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

    public void RequestGenericText(string textString, Transform textPos, Color textColor)
    {
        floatingTextManager.CreateGenericText(textString, textPos, textColor);
    }

    public void SetCrystalColor(int crystalNumber, PlayerTeam playerTeam)
    {
        if (playerTeam == PlayerTeam.RED)
        {
            guardianCrystalLeft[crystalNumber].color = Colors.YellowTeamColor;

            Debug.Log("Setting " + guardianCrystalLeft[crystalNumber].name + " to Yellow!");
        }
        else if (playerTeam == PlayerTeam.BLUE)
        {
            guardianCrystalRight[crystalNumber].color = Colors.BlueTeamColor;
        }
    }

    public void GolemCantCastCrystalUI(PlayerTeam teamColor, int crystalAmount)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                for (int i = 0; i < crystalAmount; i++)
                {
                    golemCrystalRed[i].GetComponent<Animator>().SetTrigger("canFlash");
                }
                break;

            case PlayerTeam.BLUE:
                for (int i = 0; i < crystalAmount; i++)
                {
                    golemCrystalBlue[i].GetComponent<Animator>().SetTrigger("canFlash");
                }
                break;
        }
    }

    public void GuardianCantCastCrystalUI(PlayerTeam teamColor, int crystalAmount)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                for (int i = 0; i < crystalAmount; i++)
                {
                    guardianCrystalLeft[i].GetComponent<Animator>().SetTrigger("canFlash");
                }
                break;

            case PlayerTeam.BLUE:
                for (int i = 0; i < crystalAmount; i++)
                {
                    guardianCrystalRight[i].GetComponent<Animator>().SetTrigger("canFlash");
                }
                break;
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
