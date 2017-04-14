using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConduitState
{
    EMPTY,
    IN_PROGRESS,
    CONTROLLED,
    DRAINING,
    CONTESTED,
    DISABLED,
    HOMEBASE
}

public class ConduitController : MonoBehaviour 
{
    private TimerClass waitTimer;

    private CommandManager commandManager;
    private CrystalManager crystalManager;

    private GuardianPlayerController guardianPlayerController;

    private Animator conduitAnimator;

    [Header("Conduit Attributes")]
    public ConduitState conduitState;
    public PlayerTeam conduitColor;

    public List <PlayerTeam> attachedGuardianColor;

    private float captureSpeed;
    [Space(10)]
    public float decaySpeed;
    public float totalCaptureAmount;
    public float redTeamCaptureAmount;
    public float blueTeamCaptureAmount;

    public bool isBeingAssistedByRedGolem;
    public bool isBeingAssistedByBlueGolem;

    public GameObject centerCrystal;

    [Header("Conduit Renderer Attributes")]
    public Renderer[] crystalRenderer;

    public Renderer[] innerGemRenderer;
    public Renderer[] outerGemRenderer;

    public Renderer outerRingRenderer;

    public Renderer crackRenderer;

    [Header("Player Colors")]
    public Color yellowCaptureColor;
    public Color blueCaptureColor;

    [Space(10)]
    public Color outerRingColor;

    public Color gemColor;

    public Color yellowSelectionColor;
    public Color blueSelectionColor;

    [Header("Conduit Particles Systems")]
    public ParticleSystem redCapturedParticles;
    public ParticleSystem blueCapturedParticles;

    public ParticleSystem[] ringParticlesRed;
    public ParticleSystem[] ringParticlesBlue;

    private void Start()
    {
        InitializeConduit();
    }

    private void Update()
    {
        CheckForConduitCapture();
        ManageConduitOutline();
        ManageConduitEffects();
    }

    void InitializeConduit()
    {
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        commandManager = GameObject.FindGameObjectWithTag("CommandManager").GetComponent<CommandManager>();

        waitTimer = new TimerClass();

        conduitAnimator = GetComponent<Animator>();

        if (conduitState == ConduitState.HOMEBASE)
        {
            switch (conduitColor)
            {
                case PlayerTeam.RED:
                    CompleteCapture(PlayerTeam.RED);
                    break;
                case PlayerTeam.BLUE:
                    CompleteCapture(PlayerTeam.BLUE);
                    break;
            }
        }
        else
        {
            conduitState = ConduitState.EMPTY;
            conduitColor = PlayerTeam.NONE;
        }
    }

    void ManageConduitEffects()
    {
        if (conduitState == ConduitState.IN_PROGRESS)
        {
           if (redTeamCaptureAmount > 0)
            {
                float capturePercentage = (redTeamCaptureAmount / totalCaptureAmount) * 100;

                if (capturePercentage > 25f && capturePercentage < 26f)
                {
                    ringParticlesRed[0].Play();

                    foreach (Renderer gemRenderer in outerGemRenderer)
                    {
                        gemRenderer.material.color = yellowCaptureColor;
                    }
                }
                else if (capturePercentage > 50f && capturePercentage < 51f)
                {
                    ringParticlesRed[1].Play();

                    foreach (Renderer gemRenderer in innerGemRenderer)
                    {
                        gemRenderer.material.color = yellowCaptureColor;
                    }
                }
                else if (capturePercentage > 90f && capturePercentage < 91f)
                {
                    ringParticlesRed[2].Play();
                }
            }
            else if (blueTeamCaptureAmount > 0)
            {
                float capturePercentage = blueTeamCaptureAmount / totalCaptureAmount;

                if (capturePercentage > 25f && capturePercentage < 26f)
                {
                    ringParticlesBlue[0].Play();

                    foreach (Renderer gemRenderer in innerGemRenderer)
                    {
                        gemRenderer.material.color = blueCaptureColor;
                    }
                }
                else if (capturePercentage > 50f && capturePercentage < 51f)
                {
                    ringParticlesBlue[1].Play();

                    foreach (Renderer gemRenderer in outerGemRenderer)
                    {
                        gemRenderer.material.color = blueCaptureColor;
                    }
                }
                else if (capturePercentage > 90f && capturePercentage < 91f)
                {
                    ringParticlesBlue[2].Play();
                }
            }
        }
        else if (conduitState == ConduitState.DRAINING)
        {
            if (redTeamCaptureAmount > 0)
            {
                float capturePercentage = (redTeamCaptureAmount / totalCaptureAmount) * 100;

                if (capturePercentage > 25f && capturePercentage < 26f)
                {
                    foreach (Renderer gemRenderer in outerGemRenderer)
                    {
                        gemRenderer.material.color = gemColor;
                    }
                }
                else if (capturePercentage > 50f && capturePercentage < 51f)
                {
                    foreach (Renderer gemRenderer in innerGemRenderer)
                    {
                        gemRenderer.material.color = gemColor;
                    }
                }           
            }
            else if (blueTeamCaptureAmount > 0)
            {
                float capturePercentage = blueTeamCaptureAmount / totalCaptureAmount;

                if (capturePercentage > 25f && capturePercentage < 26f)
                {
                    foreach (Renderer gemRenderer in innerGemRenderer)
                    {
                        gemRenderer.material.color = gemColor;
                    }
                }
                else if (capturePercentage > 50f && capturePercentage < 51f)
                {
                    foreach (Renderer gemRenderer in outerGemRenderer)
                    {
                        gemRenderer.material.color = gemColor;
                    }
                }       
            }
        }
    }

    public void ManageConduitOutline()
    {
        if (conduitState != ConduitState.CONTROLLED)
        {
            if (attachedGuardianColor.Contains(PlayerTeam.RED) && attachedGuardianColor.Contains(PlayerTeam.BLUE))
            {
                outerRingRenderer.material.color = Color.green;
            }
            else if (attachedGuardianColor.Contains(PlayerTeam.RED))
            {
                outerRingRenderer.material.color = yellowSelectionColor;
            }
            else if (attachedGuardianColor.Contains(PlayerTeam.BLUE))
            {
                outerRingRenderer.material.color = blueSelectionColor;
            }        
        }
        else if (conduitState == ConduitState.CONTROLLED)
        {
            if (attachedGuardianColor.Contains(PlayerTeam.RED))
            {
                outerRingRenderer.material.color = yellowSelectionColor;
            }
            else if (attachedGuardianColor.Contains(PlayerTeam.BLUE))
            {
                outerRingRenderer.material.color = blueSelectionColor;
            }
        }
        else if (conduitState != ConduitState.DISABLED)
        {
            outerRingRenderer.material.color = Color.black;
        }    
    }

    public void StartConduitCapture(PlayerTeam teamColor, GameObject Guardian)
    {
        if (conduitState == ConduitState.EMPTY)
        {
            guardianPlayerController = Guardian.GetComponent<GuardianPlayerController>();
            guardianPlayerController.isCapturingOrb = true;
            captureSpeed = guardianPlayerController.captureSpeed;

            conduitState = ConduitState.IN_PROGRESS;
            conduitColor = teamColor;
        }
        else if (conduitState == ConduitState.IN_PROGRESS)
        {
            if (teamColor != conduitColor)
            {
                if (!attachedGuardianColor.Contains(conduitColor))
                {
                    guardianPlayerController.isCapturingOrb = false;

                    guardianPlayerController = Guardian.GetComponent<GuardianPlayerController>();
                    guardianPlayerController.isCapturingOrb = true;
                    captureSpeed = guardianPlayerController.captureSpeed;

                    conduitColor = teamColor;
                }
            }
        }
    }

    public void CheckForConduitCapture()
    {
        if (conduitState == ConduitState.IN_PROGRESS)
        {
            switch(conduitColor)
            {
                case PlayerTeam.RED:
                    if (attachedGuardianColor.Contains(PlayerTeam.RED) && !attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {
                        if (blueTeamCaptureAmount > 0)
                        {
                            blueTeamCaptureAmount -= captureSpeed * Time.deltaTime;
                        }
                        else
                        {
                            redTeamCaptureAmount += captureSpeed * Time.deltaTime;
                            blueTeamCaptureAmount = 0;

                            if (redTeamCaptureAmount > totalCaptureAmount)
                            {
                                redTeamCaptureAmount = totalCaptureAmount;

                                CompleteCapture(PlayerTeam.RED);
                            }
                        }
                    }
                    else if (attachedGuardianColor.Contains(PlayerTeam.RED) && attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {
                        
                    }
                    else
                    {
                        redTeamCaptureAmount -= decaySpeed * Time.deltaTime;

                        if (redTeamCaptureAmount < 0)
                        {
                            ResetConduit();
                        }
                    }         
                    break;

                case PlayerTeam.BLUE:
                    if (attachedGuardianColor.Contains(PlayerTeam.BLUE) && !attachedGuardianColor.Contains(PlayerTeam.RED))
                    {
                        if (redTeamCaptureAmount > 0)
                        {
                            redTeamCaptureAmount -= captureSpeed * Time.deltaTime;
                        }
                        else
                        {
                            blueTeamCaptureAmount += captureSpeed * Time.deltaTime;
                            redTeamCaptureAmount = 0;

                            if (blueTeamCaptureAmount > totalCaptureAmount)
                            {
                                blueTeamCaptureAmount = totalCaptureAmount;

                                CompleteCapture(PlayerTeam.BLUE);
                            }
                        }
                    }
                    else if (attachedGuardianColor.Contains(PlayerTeam.RED) && attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {
                       
                    }
                    else
                    {
                        blueTeamCaptureAmount -= decaySpeed * Time.deltaTime;

                        if (blueTeamCaptureAmount < 0)
                        {
                            ResetConduit();
                        }
                    }                 
                    break;
                default:
                    Debug.Log("Something wrong passed through CheckForOrbCapture, was " + conduitColor);
                    break;
            }
        }
        else if (conduitState == ConduitState.CONTROLLED)
        {
            switch (conduitColor)
            {
                case PlayerTeam.RED:
                    break;

                case PlayerTeam.BLUE:
                    break;
            }
        }
    }

    void CompleteCapture(PlayerTeam teamColor)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                crystalManager.CaptureCrystal(PlayerTeam.RED);

                outerRingRenderer.material.color = yellowCaptureColor;

                commandManager.ConduitCapture(PlayerTeam.RED);

                PlayRedCaptureParticles();
                break;
            case PlayerTeam.BLUE:
                crystalManager.CaptureCrystal(PlayerTeam.BLUE);

                outerRingRenderer.material.color = blueCaptureColor;

                commandManager.ConduitCapture(PlayerTeam.BLUE);

                PlayBlueCaptureParticles();
                break;
        }

        captureSpeed = 0;
        guardianPlayerController.isCapturingOrb = false;
        guardianPlayerController.orbList.Add(gameObject);
        guardianPlayerController.FinishCapture();
        conduitState = ConduitState.CONTROLLED;
    }

    void PlayRedCaptureParticles()
    {
        redCapturedParticles.Play();
    }

    void PlayBlueCaptureParticles()
    {
        blueCapturedParticles.Play(); 
    }

    public void ResetConduit()
    {
        if (guardianPlayerController != null)
        {
            guardianPlayerController.isCapturingOrb = false;
        }

        redTeamCaptureAmount = 0;
        blueTeamCaptureAmount = 0;

        conduitState = ConduitState.EMPTY;
        conduitColor = PlayerTeam.NONE;
    }

    public void DeselectConduit(PlayerTeam guardianColor)
    {
        attachedGuardianColor.Remove(guardianColor);      
        
        if (attachedGuardianColor.Count == 0)
        {
            outerRingRenderer.material.color = outerRingColor;
        }
    }

    public void DisableConduit (float length, PlayerTeam teamColor)
    {
        if (conduitColor != teamColor)
        {
            StartCoroutine(ConduitDisable(length));
        }
    }

    private IEnumerator ConduitDisable (float disableTime)
    {
        conduitState = ConduitState.DISABLED;

        yield return new WaitForSeconds(disableTime);

        if (conduitColor == PlayerTeam.RED || conduitColor == PlayerTeam.BLUE)
        {
            conduitState = ConduitState.CONTROLLED;
        }
        else
        {
            conduitState = ConduitState.EMPTY;
        }

        yield return null;
    }

    public bool CanCaptureConduit()
    {
        if (conduitState == ConduitState.EMPTY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
