using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    private UIManager UI;

    [Header("Crystal Attributes")]
    public int totalMapCrystalCount = 0;

    [Header("Team Crystal Attributes")]
    public int startingCrystalCount = 2;

    public float redTeamRefill = 0f;
    public float blueTeamRefill = 0f;

    [Space(20)]
    public int redTeamCurrentCrystalCount = 0;
    public int blueTeamCurrentCrystalCount = 0;

    public int redTeamTotalCrystalCount = 0;
    public int blueTeamTotalCrystalCount = 0;

    [Space(10)]
    public List<GameObject> redTeamCapturedCrystals;
    public List<GameObject> blueTeamCapturedCrystals;
    [Space(10)]
    public float redTeamCrystalRegenerationRate = 0f;
    public float blueTeamCrystalRegenerationRate = 0f;

    public bool canRedTeamRegenerate = true;
    public bool canBlueTeamRegenerate = true;

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

        redTeamTotalCrystalCount = startingCrystalCount;
        blueTeamTotalCrystalCount = startingCrystalCount;
    }

    void RegenerateCrystals()
    {
        if (redTeamCurrentCrystalCount != redTeamTotalCrystalCount)
        {
            if (canRedTeamRegenerate)
            {
                redTeamRefill += redTeamCrystalRegenerationRate * Time.fixedDeltaTime;

                if (redTeamRefill >= 1)
                {
                    RefillCrystal(PlayerTeam.RED);
                    redTeamRefill = 0;
                }
            }
        }

        if (blueTeamCurrentCrystalCount != blueTeamTotalCrystalCount)
        {
            if (canBlueTeamRegenerate)
            {
                blueTeamRefill += blueTeamCrystalRegenerationRate * Time.fixedDeltaTime;

                if (blueTeamRefill >= 1)
                {
                    RefillCrystal(PlayerTeam.BLUE);
                    blueTeamRefill = 0;
                }
            }
        }     
    }

    public bool TryCast(int crystalAmount, PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                if (redTeamCurrentCrystalCount >= crystalAmount)
                {
                    return true;
                }
                break;

            case PlayerTeam.BLUE:
                if (blueTeamCurrentCrystalCount >= crystalAmount)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public bool UseCrystals(int crystalAmount, PlayerTeam teamColor)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                if (redTeamCurrentCrystalCount >= crystalAmount)
                {
                    int count = redTeamCurrentCrystalCount - crystalAmount;

                    for (int i = redTeamCurrentCrystalCount - 1; i >= count; i--)
                    {
                        UI.ResetCrystalUI(i, PlayerTeam.RED);
                    }

                    redTeamCurrentCrystalCount -= crystalAmount;

                    return true;
                } 
                break;

            case PlayerTeam.BLUE:
                if (blueTeamCurrentCrystalCount >= crystalAmount)
                {
                    int count = blueTeamCurrentCrystalCount - crystalAmount;

                    for (int i = blueTeamCurrentCrystalCount - 1; i >= count; i--)
                    {
                        UI.ResetCrystalUI(i, PlayerTeam.BLUE);
                    }

                    blueTeamCurrentCrystalCount -= crystalAmount;

                    return true;
                }
                break;
        }

        return false;
    }

    void RefillCrystal(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamCurrentCrystalCount++;
                break;

            case PlayerTeam.BLUE:
                blueTeamCurrentCrystalCount++;
                break;
        }
    }

    public void CaptureCrystal(PlayerTeam teamColor, GameObject conduit)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamTotalCrystalCount++;
                redTeamCapturedCrystals.Add(conduit);
                break;

            case PlayerTeam.BLUE:
                blueTeamTotalCrystalCount++;
                blueTeamCapturedCrystals.Add(conduit);
                break;
        }
    }

    public void RemoveCrystal(PlayerTeam teamColor, GameObject conduit)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamTotalCrystalCount--;
                redTeamCapturedCrystals.Remove(conduit);
                break;

            case PlayerTeam.BLUE:
                blueTeamTotalCrystalCount--;
                blueTeamCapturedCrystals.Remove(conduit);
                break;
        }
    }
}
