using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum OrbState
{
    EMPTY,
    IN_PROGRESS,
    CONTROLLED,
    CONTESTED
}

public class OrbController : MonoBehaviour 
{
    private GuardianPlayerController orbGuardianPlayerController;

    [Header("Orb Attributes")]
    public OrbState orbState;
    public PlayerTeam orbColor;

    private float captureSpeed;
    public float totalCaptureAmount;
    public float redTeamCaptureAmount;
    public float blueTeamCaptureAmount;

    public bool isBeingCaptured;

    [Header("Orb UI Attributes")]
    public Image orbCaptureIndicator;

    [Header("Orb Renderer Attributes")]
    public Renderer orbRenderer;
    public Material defaultMat;
    public Material redMat;
    public Material blueMat;

    private void Start()
    {
        InitializeOrb();
    }

    private void Update()
    {
        CaptureOrb();
        ManageOrbUI();
    }

    void InitializeOrb()
    {
        orbState = OrbState.EMPTY;
        orbColor = PlayerTeam.NONE;
    }

    void ManageOrbUI()
    {
        if (isBeingCaptured)
        {
           if (redTeamCaptureAmount > 0)
            {
                orbCaptureIndicator.fillAmount = redTeamCaptureAmount / totalCaptureAmount;
            }
            else if (blueTeamCaptureAmount > 0)
            {
                orbCaptureIndicator.fillAmount = blueTeamCaptureAmount / totalCaptureAmount;
            }
        }
    }

    public void StartOrbCapture(PlayerTeam teamColor, GameObject Guardian)
    {
        if (orbState == OrbState.EMPTY)
        {
            orbColor = teamColor;
            orbGuardianPlayerController = Guardian.GetComponent<GuardianPlayerController>();
            captureSpeed = orbGuardianPlayerController.captureSpeed;
            orbGuardianPlayerController.isCapturingOrb = true;
            isBeingCaptured = true;
            orbCaptureIndicator.enabled = true;
            orbState = OrbState.IN_PROGRESS;
        }
        else if (orbState == OrbState.IN_PROGRESS)
        {
            if (orbColor != teamColor)
            {
                orbGuardianPlayerController.isCapturingOrb = false;
                orbGuardianPlayerController = Guardian.GetComponent<GuardianPlayerController>();
                captureSpeed = orbGuardianPlayerController.captureSpeed;
                orbGuardianPlayerController.isCapturingOrb = true;
                orbColor = teamColor;
            }         
        }
    }

    public void CaptureOrb()
    {
        if (isBeingCaptured)
        {
            switch (orbColor)
            {
                case PlayerTeam.RED:
                    if (blueTeamCaptureAmount > 0)
                    {
                        orbCaptureIndicator.color = blueMat.color;
                        blueTeamCaptureAmount -= captureSpeed * Time.deltaTime;
                    }
                    else
                    {
                        orbCaptureIndicator.color = redMat.color;
                        redTeamCaptureAmount += captureSpeed * Time.deltaTime;

                        if (redTeamCaptureAmount >= totalCaptureAmount)
                        {
                            redTeamCaptureAmount = totalCaptureAmount;
                            CompleteCapture(PlayerTeam.RED);
                            isBeingCaptured = false;
                        }
                    }
                    break;

                case PlayerTeam.BLUE:
                    if (redTeamCaptureAmount > 0)
                    {
                        orbCaptureIndicator.color = redMat.color;
                        redTeamCaptureAmount -= captureSpeed * Time.deltaTime;
                    }
                    else
                    {
                        orbCaptureIndicator.color = blueMat.color;
                        blueTeamCaptureAmount += captureSpeed * Time.deltaTime;

                        if (blueTeamCaptureAmount >= totalCaptureAmount)
                        {
                            blueTeamCaptureAmount = totalCaptureAmount;
                            CompleteCapture(PlayerTeam.BLUE);
                            isBeingCaptured = false;
                        }
                    }
                    break;
            }
        }
    }

    void CompleteCapture(PlayerTeam teamColor)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                orbRenderer.material = redMat;
                break;
            case PlayerTeam.BLUE:
                orbRenderer.material = blueMat;
                break;
        }

        orbCaptureIndicator.enabled = false;
        captureSpeed = 0;
        orbGuardianPlayerController.isCapturingOrb = false;
        orbGuardianPlayerController.orbList.Add(gameObject);
        orbState = OrbState.CONTROLLED;
    }

    void ResetOrb()
    {
        redTeamCaptureAmount = 0;
        blueTeamCaptureAmount = 0;
        captureSpeed = 0;

        orbState = OrbState.EMPTY;
        isBeingCaptured = false;
    }
}
