using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    private UIManager UI;

    [Header("Crystal Attributes")]
    public int totalMapCrystalCount = 0;

    [Header("Team Crystal Attributes")]
    public List<GameObject> redTeamCapturedCrystals;
    public List<GameObject> blueTeamCapturedCrystals;

    public int startingCrystalCount = 2;
    [Space(10)]
    public float redTeamGuardianRefill = 0f;
    public float blueTeamGuardianRefill = 0f;
    [Space(10)]
    public float redTeamGolemRefill = 0f;
    public float blueTeamGolemRefill = 0f;

    [Space(20)]
    public int redTeamGuardianCurrentCrystalCount = 0;
    public int blueTeamGuardianCurrentCrystalCount = 0;
    [Space(10)]
    public int redTeamGolemCurrentCrystalCount = 0;
    public int blueTeamGolemCurrentCrystalCount = 0;
    [Space(10)]
    public int redTeamTotalCrystalCount = 0;
    public int blueTeamTotalCrystalCount = 0;

    [Space(10)]
    public float redTeamGuardianCrystalRegenerationRate = 0f;
    public float blueTeamGuardianCrystalRegenerationRate = 0f;
    [Space(10)]
    public float redTeamGolemCrystalRegenerationRate = 0;
    public float blueTeamGolemCrystalRegenerationRate = 0;
    [Space(10)]
    public bool canRedTeamGuardianRegenerate = true;
    public bool canBlueTeamGuardianRegenerate = true;
    [Space(10)]
    public bool canRedTeamGolemRegenerate = true;
    public bool canBlueTeamGolemRegenerate = true;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        RegenerateCrystals();
    }

    void Initialize()
    {
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        SetupStartingCrystals();
    }

    void SetupStartingCrystals()
    {
        for (int i = -1; i < startingCrystalCount; i++)
        {
            CaptureCrystal(PlayerTeam.RED);
            CaptureCrystal(PlayerTeam.BLUE);
        }
    }

    void RegenerateCrystals()
    {
        if (redTeamGuardianCurrentCrystalCount != redTeamTotalCrystalCount)
        {
            if (canRedTeamGuardianRegenerate)
            {
                redTeamGuardianRefill += redTeamGuardianCrystalRegenerationRate * Time.fixedDeltaTime;

                if (redTeamGuardianRefill >= 1)
                {
                    RefillCrystal(PlayerTeam.RED, PlayerType.GUARDIAN);
                    redTeamGuardianRefill = 0;
                }
            }
        }

        if (redTeamGolemCurrentCrystalCount != redTeamTotalCrystalCount)
        {
            if (canRedTeamGolemRegenerate)
            {
                redTeamGolemRefill += redTeamGolemCrystalRegenerationRate * Time.fixedDeltaTime;

                if (redTeamGolemRefill >= 1)
                {
                    RefillCrystal(PlayerTeam.RED, PlayerType.GOLEM);
                    redTeamGolemRefill = 0;
                }
            }
        }

        if (blueTeamGuardianCurrentCrystalCount != blueTeamTotalCrystalCount)
        {
            if (canBlueTeamGuardianRegenerate)
            {
                blueTeamGuardianRefill += blueTeamGuardianCrystalRegenerationRate * Time.fixedDeltaTime;

                if (blueTeamGuardianRefill >= 1)
                {
                    RefillCrystal(PlayerTeam.BLUE, PlayerType.GUARDIAN);
                    blueTeamGuardianRefill = 0;
                }
            }
        }   
        
        if (blueTeamGolemCurrentCrystalCount != blueTeamTotalCrystalCount)
        {
            if (canBlueTeamGolemRegenerate)
            {
                blueTeamGolemRefill += blueTeamGolemCrystalRegenerationRate * Time.fixedDeltaTime;

                if (blueTeamGolemRefill >= 1)
                {
                    RefillCrystal(PlayerTeam.BLUE, PlayerType.GOLEM);
                    blueTeamGolemRefill = 0;
                }
            }
        }
    }

    public bool TryCast(int crystalAmount, PlayerTeam teamColor, PlayerType playerType)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                if (playerType == PlayerType.GOLEM)
                {
                    if (redTeamGolemCurrentCrystalCount >= crystalAmount)
                    {
                        return true;
                    }
                    else
                    {
                        UI.GolemCantCastCrystalUI(PlayerTeam.RED, crystalAmount);
                    }
                }
                else if (playerType == PlayerType.GUARDIAN)
                {
                    if (redTeamGuardianCurrentCrystalCount >= crystalAmount)
                    {
                        return true;
                    }
                    else
                    {
                        UI.GuardianCantCastCrystalUI(PlayerTeam.RED, crystalAmount);
                    }
                }
                
                break;

            case PlayerTeam.BLUE:
                if (playerType == PlayerType.GOLEM)
                {
                    if (blueTeamGolemCurrentCrystalCount >= crystalAmount)
                    {
                        return true;
                    }
                    else
                    {
                        UI.GolemCantCastCrystalUI(PlayerTeam.BLUE, crystalAmount);
                    }
                }
                else if (playerType == PlayerType.GUARDIAN)
                {
                    if (blueTeamGuardianCurrentCrystalCount >= crystalAmount)
                    {
                        return true;
                    }
                    else
                    {
                        UI.GuardianCantCastCrystalUI(PlayerTeam.BLUE, crystalAmount);
                    }
                }       
                break;
        }

        return false;
    }

    public bool UseCrystals(int crystalAmount, PlayerTeam teamColor, PlayerType playerType)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                if (playerType == PlayerType.GUARDIAN)
                {
                    if (redTeamGuardianCurrentCrystalCount >= crystalAmount)
                    {
                        int count = redTeamGuardianCurrentCrystalCount - crystalAmount;

                        for (int i = redTeamGuardianCurrentCrystalCount; i >= count; i--)
                        {
                            UI.ResetCrystalUI(i, PlayerTeam.RED, PlayerType.GUARDIAN);
                        }

                        redTeamGuardianCurrentCrystalCount -= crystalAmount;

                        return true;
                    }
                }
                else if (playerType == PlayerType.GOLEM)
                {
                    if (redTeamGolemCurrentCrystalCount >= crystalAmount)
                    {
                        int count = redTeamGolemCurrentCrystalCount - crystalAmount;

                        for (int i = redTeamGolemCurrentCrystalCount; i >= count; i--)
                        {
                            UI.ResetCrystalUI(i, PlayerTeam.RED, PlayerType.GOLEM);
                        }

                        redTeamGolemCurrentCrystalCount -= crystalAmount;

                        return true;
                    }
                }             
                break;

            case PlayerTeam.BLUE:
                if (playerType == PlayerType.GUARDIAN)
                {
                    if (blueTeamGuardianCurrentCrystalCount >= crystalAmount)
                    {
                        int count = blueTeamGuardianCurrentCrystalCount - crystalAmount;

                        for (int i = blueTeamGuardianCurrentCrystalCount; i >= count; i--)
                        {
                            UI.ResetCrystalUI(i, PlayerTeam.BLUE, PlayerType.GUARDIAN);
                        }

                        blueTeamGuardianCurrentCrystalCount -= crystalAmount;

                        return true;
                    }
                }
                else if (playerType == PlayerType.GOLEM)
                {
                    if (blueTeamGolemCurrentCrystalCount >= crystalAmount)
                    {
                        int count = blueTeamGolemCurrentCrystalCount - crystalAmount;

                        for (int i = blueTeamGolemCurrentCrystalCount; i >= count; i--)
                        {
                            UI.ResetCrystalUI(i, PlayerTeam.BLUE, PlayerType.GOLEM);
                        }

                        blueTeamGolemCurrentCrystalCount -= crystalAmount;

                        return true;
                    }
                }              
                break;
        }

        return false;
    }

    void RefillCrystal(PlayerTeam teamColor, PlayerType playerType)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                if (playerType == PlayerType.GOLEM)
                {
                    redTeamGolemCurrentCrystalCount++;
                }
                else if (playerType == PlayerType.GUARDIAN)
                {
                    redTeamGuardianCurrentCrystalCount++;
                }
                break;

            case PlayerTeam.BLUE:
                if (playerType == PlayerType.GOLEM)
                {
                    blueTeamGolemCurrentCrystalCount++;
                }
                else if (playerType == PlayerType.GUARDIAN)
                {
                    blueTeamGuardianCurrentCrystalCount++;
                }
                break;
        }
    }

    public void CaptureCrystal(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamTotalCrystalCount++;
                UI.SetCrystalColor(redTeamTotalCrystalCount, PlayerTeam.RED);
                break;

            case PlayerTeam.BLUE:
                blueTeamTotalCrystalCount++;
                UI.SetCrystalColor(blueTeamTotalCrystalCount, PlayerTeam.RED);
                break;
        }
    }

    public void RemoveCrystal(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamTotalCrystalCount--;
                break;

            case PlayerTeam.BLUE:
                blueTeamTotalCrystalCount--;
                break;
        }
    }
}
