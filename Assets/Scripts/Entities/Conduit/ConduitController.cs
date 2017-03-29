using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject orbObjectBase;

    [Header("Conduit UI Attributes")]
    public Image orbCaptureIndicator;

    [Header("Conduit Renderer Attributes")]
    public Renderer conduitCrackRenderer;
    public Renderer outerRingRenderer;
    public Renderer[] outerGemRenderer;
    public Renderer[] innerGemRenderer;
    public Renderer[] gemRenderer;

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
        orbObjectBase = gameObject;

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

                orbCaptureIndicator.fillAmount = redTeamCaptureAmount / totalCaptureAmount;

                if (capturePercentage > 25f && capturePercentage < 50f)
                {
                    //25%
                }
                else if (capturePercentage > 50f && capturePercentage < 75f)
                {
                    //50%
                }
                else if (capturePercentage > 75f && capturePercentage < 100f)
                {
                    //75%
                }
                else
                {
                    //100%
                }

                conduitCrackRenderer.material.color = Color.Lerp(conduitCrackRenderer.material.color, Color.yellow, redTeamCaptureAmount / totalCaptureAmount);

                foreach (Renderer gemRender in gemRenderer)
                {
                    gemRender.material.color = Color.Lerp(gemRender.material.color, Color.yellow, redTeamCaptureAmount / totalCaptureAmount);
                }
            }
            else if (blueTeamCaptureAmount > 0)
            {
                float capturePercentage = blueTeamCaptureAmount / totalCaptureAmount;

                if (capturePercentage > 0.25f && capturePercentage < 0.50f)
                {

                }
                else if (capturePercentage > 0.50f && capturePercentage < 0.75f)
                {

                }
                else if (capturePercentage > 0.75f && capturePercentage < 1f)
                {

                }
          
                orbCaptureIndicator.fillAmount = blueTeamCaptureAmount / totalCaptureAmount;

                conduitCrackRenderer.material.color = Color.Lerp(conduitCrackRenderer.material.color, Color.blue, blueTeamCaptureAmount / totalCaptureAmount);

                foreach (Renderer gemRender in gemRenderer)
                {
                    gemRender.material.color = Color.Lerp(gemRender.material.color, Color.blue, blueTeamCaptureAmount / totalCaptureAmount);
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

            orbCaptureIndicator.enabled = true;

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

                    orbCaptureIndicator.enabled = true;

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
                            orbCaptureIndicator.color = Color.red;

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
                            orbCaptureIndicator.color = Color.blue;

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
                conduitCrackRenderer.material.color = Color.yellow;

                foreach (Renderer gemRender in gemRenderer)
                {
                    gemRender.material.color = Color.yellow;
                }

                PlayRedCaptureParticles();
                break;
            case PlayerTeam.BLUE:
                conduitCrackRenderer.material.color = Color.blue;

                foreach (Renderer gemRender in gemRenderer)
                {
                    gemRender.material.color = Color.blue;
                }

                PlayBlueCaptureParticles();
                break;
        }

        orbCaptureIndicator.enabled = false;
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

        foreach (Renderer gemRender in gemRenderer)
        {
            gemRender.material.color = Color.black;
        }

        conduitCrackRenderer.material.color = Color.black;
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
