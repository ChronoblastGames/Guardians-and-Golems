using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbState
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
    public OrbState orbState;
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

    [HideInInspector]
    public GameObject centerCrystal;

    [Header("Conduit Renderer Attributes")]
    public Renderer[] crystalRenderer;

    public Renderer[] innerGemRenderer;
    public Renderer[] outerGemRenderer;

    public Renderer innerRingRenderer;
    public Renderer middleRingRenderer;
    public Renderer outerRingRenderer;

    [Header("Player Colors")]
    public Color yellowColor;
    public Color blueColor;

    [Header("Conduit Particles Systems")]
    public ParticleSystem redCaptureParticles;
    public ParticleSystem blueCaptureParticles;

    public ParticleSystem[] redConstantParticles;
    public ParticleSystem[] blueConstantParticles;

    private void Start()
    {
        InitializeOrb();
    }

    private void Update()
    {
        CheckForOrbCapture();
        ManageOrbOutline();
        ManageOrbUI();
    }

    void InitializeOrb()
    {
        conduitAnimator = GetComponent<Animator>();

        if (orbState == OrbState.HOMEBASE)
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
            orbState = OrbState.EMPTY;
            orbColor = PlayerTeam.NONE;
        }
    }

    void ManageOrbUI()
    {
        if (orbState == OrbState.IN_PROGRESS)
        {
           if (redTeamCaptureAmount > 0)
            {
                float capturePercentage = (redTeamCaptureAmount / totalCaptureAmount) * 100;

                if (capturePercentage > 25f && capturePercentage < 50f)
                {
                    middleRingRenderer.material.color = Color.Lerp(innerRingRenderer.material.color, yellowColor, capturePercentage);
                }
                else if (capturePercentage > 50f && capturePercentage < 75f)
                {
                    innerRingRenderer.material.color = Color.Lerp(middleRingRenderer.material.color, yellowColor, capturePercentage);
                }
                else if (capturePercentage > 75f && capturePercentage < 100f)
                {
                    foreach (Renderer gemRenderer in crystalRenderer)
                    {
                        gemRenderer.material.color = Color.Lerp(gemRenderer.material.color, yellowColor, capturePercentage);
                    }

                    foreach (Renderer gemRenderer in outerGemRenderer)
                    {
                        gemRenderer.material.color = Color.Lerp(gemRenderer.material.color, yellowColor, capturePercentage);
                    }

                    foreach (Renderer gemRenderer in innerGemRenderer)
                    {
                        gemRenderer.material.color = Color.Lerp(gemRenderer.material.color, yellowColor, capturePercentage);
                    }
                }
            }
            else if (blueTeamCaptureAmount > 0)
            {
                float capturePercentage = blueTeamCaptureAmount / totalCaptureAmount;

                if (capturePercentage > 0.25f && capturePercentage < 0.50f)
                {
                    middleRingRenderer.material.color = Color.Lerp(innerRingRenderer.material.color, blueColor, capturePercentage);
                }
                else if (capturePercentage > 0.50f && capturePercentage < 0.75f)
                {
                    innerRingRenderer.material.color = Color.Lerp(middleRingRenderer.material.color, blueColor, capturePercentage);
                }
                else if (capturePercentage > 0.75f && capturePercentage < 1f)
                {
                    foreach (Renderer gemRenderer in crystalRenderer)
                    {
                        gemRenderer.material.color = Color.Lerp(gemRenderer.material.color, blueColor, capturePercentage);
                    }

                    foreach (Renderer gemRenderer in outerGemRenderer)
                    {
                        gemRenderer.material.color = Color.Lerp(gemRenderer.material.color, blueColor, capturePercentage);
                    }

                    foreach (Renderer gemRenderer in innerGemRenderer)
                    {
                        gemRenderer.material.color = Color.Lerp(gemRenderer.material.color, blueColor, capturePercentage);
                    }
                }
            }
        }
    }

    public void ManageOrbOutline()
    {
        if (orbState == OrbState.DISABLED)
        {
            outerRingRenderer.material.color = Color.gray;
        }
       else if (attachedGuardianColor.Contains(PlayerTeam.RED) && attachedGuardianColor.Contains(PlayerTeam.BLUE))
        {
            outerRingRenderer.material.color = Color.green;
        }
        else if (attachedGuardianColor.Contains(PlayerTeam.RED))
        {
            outerRingRenderer.material.color = Color.yellow;
        }
       else if (attachedGuardianColor.Contains(PlayerTeam.BLUE))
        {
            outerRingRenderer.material.color = Color.blue;
        }
    }

    public void StartOrbCapture(PlayerTeam teamColor, GameObject Guardian)
    {
        if (orbState == OrbState.EMPTY)
        {
            guardianPlayerController = Guardian.GetComponent<GuardianPlayerController>();
            guardianPlayerController.isCapturingOrb = true;
            captureSpeed = guardianPlayerController.captureSpeed;

            orbState = OrbState.IN_PROGRESS;
            orbColor = teamColor;
        }
        else if (orbState == OrbState.IN_PROGRESS)
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

    public void CheckForOrbCapture()
    {
        if (orbState == OrbState.IN_PROGRESS)
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
                            ResetOrb();
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
                            ResetOrb();
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
        

                PlayRedCaptureParticles();
                break;
            case PlayerTeam.BLUE:
            

                PlayBlueCaptureParticles();
                break;
        }

        captureSpeed = 0;
        guardianPlayerController.isCapturingOrb = false;
        guardianPlayerController.orbList.Add(gameObject);
        orbState = OrbState.CONTROLLED;
    }

    void PlayRedCaptureParticles()
    {
        redCaptureParticles.Play();

        foreach (ParticleSystem particles in redConstantParticles)
        {
            
        }
    }

    void PlayBlueCaptureParticles()
    {
        blueCaptureParticles.Play();

        foreach (ParticleSystem particles in blueConstantParticles)
        {

        }
    }

    public void ResetOrb()
    {
        guardianPlayerController.isCapturingOrb = false;
        redTeamCaptureAmount = 0;
        blueTeamCaptureAmount = 0;

        orbState = OrbState.EMPTY;
        orbColor = PlayerTeam.NONE;
    }

    public void DeselectOrb(PlayerTeam guardianColor)
    {
        attachedGuardianColor.Remove(guardianColor);      
        
        if (attachedGuardianColor.Count == 0)
        {
            outerRingRenderer.material.color = Color.black;
        }
    }

    public void DisableOrb (float length, PlayerTeam teamColor)
    {
        if (orbColor != teamColor)
        {
            OrbDisable(length);
        }
    }

    private IEnumerator OrbDisable (float disableTime)
    {
        orbState = OrbState.DISABLED;

        Debug.Log("Off");

        yield return new WaitForSeconds(disableTime);

        if (orbColor == PlayerTeam.RED || orbColor == PlayerTeam.BLUE)
        {
            orbState = OrbState.CONTROLLED;
        }
        else
        {
            orbState = OrbState.EMPTY;
        }

        yield return null;
    }
}
