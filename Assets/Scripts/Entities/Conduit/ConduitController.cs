using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConduitState
{
    EMPTY,
    IN_PROGRESS,
    CONTROLLED,
    CONTESTED,
    DISABLED,
    HOMEBASE
}

public class ConduitController : MonoBehaviour 
{
    private GuardianPlayerController guardianPlayerController;

    private Animator conduitAnimator;

    [Header("Conduit Attributes")]
    public ConduitState conduitState;
    public PlayerTeam orbColor;

    public List<PlayerTeam> attachedGuardianColor;

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
    private Material outerRingMat;

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
        conduitAnimator = GetComponent<Animator>();

        outerRingMat = outerRingRenderer.material;

        if (conduitState == ConduitState.HOMEBASE)
        {
            switch (orbColor)
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
            orbColor = PlayerTeam.NONE;
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
    }

    public void ManageConduitOutline()
    {
        if (conduitState != ConduitState.CONTROLLED)
        {
            if (conduitState == ConduitState.DISABLED)
            {
                outerRingRenderer.material.color = Color.black;
            }
            else if (attachedGuardianColor.Contains(PlayerTeam.RED) && attachedGuardianColor.Contains(PlayerTeam.BLUE))
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
        else if (attachedGuardianColor.Count == 0)
        {
            if (orbColor == PlayerTeam.RED)
            {
                outerRingRenderer.material.color = yellowCaptureColor;
            }
            else if (orbColor == PlayerTeam.BLUE)
            {
                outerRingRenderer.material.color = blueCaptureColor;
            }
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
            orbColor = teamColor;
        }
        else if (conduitState == ConduitState.IN_PROGRESS)
        {
            if (teamColor != orbColor)
            {
                if (!attachedGuardianColor.Contains(orbColor))
                {
                    guardianPlayerController.isCapturingOrb = false;

                    guardianPlayerController = Guardian.GetComponent<GuardianPlayerController>();
                    guardianPlayerController.isCapturingOrb = true;
                    captureSpeed = guardianPlayerController.captureSpeed;

                    orbColor = teamColor;
                }
            }
        }
    }

    public void CheckForConduitCapture()
    {
        if (conduitState == ConduitState.IN_PROGRESS)
        {
            switch(orbColor)
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
                    Debug.Log("Something wrong passed through CheckForOrbCapture, was " + orbColor);
                    break;
            }
        }
    }

    void CompleteCapture(PlayerTeam teamColor)
    {
        switch(teamColor)
        {
            case PlayerTeam.RED:
                outerRingRenderer.material.color = yellowCaptureColor;

                PlayRedCaptureParticles();
                break;
            case PlayerTeam.BLUE:
                outerRingRenderer.material.color = blueCaptureColor;

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
        guardianPlayerController.isCapturingOrb = false;
        redTeamCaptureAmount = 0;
        blueTeamCaptureAmount = 0;

        conduitState = ConduitState.EMPTY;
        orbColor = PlayerTeam.NONE;
    }

    public void DeselectConduit(PlayerTeam guardianColor)
    {
        attachedGuardianColor.Remove(guardianColor);      
        
        if (attachedGuardianColor.Count == 0)
        {
            outerRingRenderer.material.color = outerRingMat.color;
        }
    }

    public void DisableConduit (float length, PlayerTeam teamColor)
    {
        if (orbColor != teamColor)
        {
            ConduitDisable(length);
        }
    }

    private IEnumerator ConduitDisable (float disableTime)
    {
        conduitState = ConduitState.DISABLED;

        Debug.Log("Off");

        yield return new WaitForSeconds(disableTime);

        if (orbColor == PlayerTeam.RED || orbColor == PlayerTeam.BLUE)
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
