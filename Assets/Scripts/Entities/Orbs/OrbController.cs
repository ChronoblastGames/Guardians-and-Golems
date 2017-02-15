using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum OrbState
{
    EMPTY,
    IN_PROGRESS,
    CONTROLLED,
    CONTESTED,
    HOMEBASE
}

public class OrbController : MonoBehaviour 
{
    private GuardianPlayerController guardianPlayerController;

    [Header("Orb Attributes")]
    public OrbState orbState;
    public PlayerTeam orbColor;

    private float captureSpeed;
    public float totalCaptureAmount;
    public float redTeamCaptureAmount;
    public float blueTeamCaptureAmount;

    public bool isBeingCaptured;

    public bool isBeingAssistedByRedGolem;
    public bool isBeingAssistedByBlueGolem;

    [Header("Orb Collider")]
    public GameObject orbCollider;

    [Header("Orb UI Attributes")]
    public Image orbCaptureIndicator;

    [Header("Orb Renderer Attributes")]
    public Renderer orbRenderer;

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
        if (orbState == OrbState.EMPTY && !isBeingCaptured)
        {
            guardianPlayerController = Guardian.GetComponent<GuardianPlayerController>();
            guardianPlayerController.isCapturingOrb = true;

            captureSpeed = guardianPlayerController.captureSpeed;

            orbColor = teamColor;

            isBeingCaptured = true;
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
                        orbCaptureIndicator.color = Color.blue;
                        blueTeamCaptureAmount -= captureSpeed * Time.deltaTime;
                    }
                    else
                    {
                        orbCaptureIndicator.color = Color.red;
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
                        orbCaptureIndicator.color = Color.red;
                        redTeamCaptureAmount -= captureSpeed * Time.deltaTime;
                    }
                    else
                    {
                        orbCaptureIndicator.color = Color.blue;
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
                orbRenderer.material.SetColor("_BaseColor", Color.red);
                orbRenderer.material.SetFloat("_Outline", 30);
                break;
            case PlayerTeam.BLUE:
                orbRenderer.material.SetColor("_BaseColor", Color.blue);
                orbRenderer.material.SetFloat("_Outline", 30);
                break;
        }

        orbCaptureIndicator.enabled = false;
        captureSpeed = 0;
        guardianPlayerController.isCapturingOrb = false;
        guardianPlayerController.orbList.Add(gameObject);
        orbState = OrbState.CONTROLLED;
    }

    public void ResetOrb()
    {    
        orbRenderer.material.SetFloat("_Outline", 0);
        redTeamCaptureAmount = 0;
        blueTeamCaptureAmount = 0;
        captureSpeed = 0;

        orbState = OrbState.EMPTY;
        isBeingCaptured = false;
    }
}
