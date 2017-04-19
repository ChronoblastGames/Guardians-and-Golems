using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConduitController : MonoBehaviour 
{
    private CommandManager commandManager;
    private CrystalManager crystalManager;

    private GuardianPlayerController guardianPlayerController;

    private Animator conduitAnimator;

    [Header("Conduit Attributes")]
    public ConduitState conduitState;
    public PlayerTeam conduitColor;

    public List <PlayerTeam> attachedGuardianColor;

    private float captureSpeed;
    private float assistedCaptureSpeed;
    [Space(10)]
    public float decaySpeed;
    public float totalCaptureAmount;
    public float drainWaitTime;
    private float initialDrainWaitTime;
    [Space(10)]
    public float redTeamCaptureAmount;
    public float blueTeamCaptureAmount;

    public bool isBeingAssistedByRedGolem;
    public bool isBeingAssistedByBlueGolem;

    public GameObject centerCrystal;

    [Header("Attached Conduits")]
    public GameObject[] neighbourConduits;

    public LineRenderer[] lineRendererArray;

    public Material redLineMaterial;
    public Material blueLineMaterial;

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

        conduitAnimator = GetComponent<Animator>();

        initialDrainWaitTime = drainWaitTime;

        if (conduitState == ConduitState.HOMEBASE)
        {
            switch (conduitColor)
            {
                case PlayerTeam.RED:
                    GameObject redTeamGuardian = GameObject.FindGameObjectWithTag("RedGuardian");
                    redTeamGuardian.GetComponent<GuardianPlayerController>().conduitCapturedList.Add(gameObject);
                    break;
                case PlayerTeam.BLUE:
                    GameObject blueTeamGuardian = GameObject.FindGameObjectWithTag("BlueGuardian");
                    blueTeamGuardian.GetComponent<GuardianPlayerController>().conduitCapturedList.Add(gameObject);
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
            switch (conduitColor)
            {
                case PlayerTeam.RED:

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
                    break;

                case PlayerTeam.BLUE:
                    if (blueTeamCaptureAmount > 0)
                    {
                        float capturePercentage = (blueTeamCaptureAmount / totalCaptureAmount) * 100;

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
                    break;
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
                else if (capturePercentage > 90f && capturePercentage < 91f)
                {
                    foreach (Renderer gem in crystalRenderer)
                    {
                        gem.material.color = gemColor;
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
                else if (capturePercentage > 90f && capturePercentage < 91f)
                {
                    foreach (Renderer gem in crystalRenderer)
                    {
                        gem.material.color = gemColor;
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
                    assistedCaptureSpeed = guardianPlayerController.assistedCaptureSpeed;

                    conduitColor = teamColor;
                }
            }
        }
    }

    public void CheckForConduitCapture()
    {
        if (conduitState == ConduitState.IN_PROGRESS)
        {
            switch (conduitColor)
            {
                case PlayerTeam.RED:
                    if (attachedGuardianColor.Contains(PlayerTeam.RED) && !attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {
                        if (isBeingAssistedByRedGolem)
                        {
                            if (blueTeamCaptureAmount > 0)
                            {
                                blueTeamCaptureAmount -= assistedCaptureSpeed * Time.deltaTime;
                            }
                            else
                            {
                                redTeamCaptureAmount += assistedCaptureSpeed * Time.deltaTime;
                                blueTeamCaptureAmount = 0;

                                if (redTeamCaptureAmount > totalCaptureAmount)
                                {
                                    redTeamCaptureAmount = totalCaptureAmount;

                                    CompleteCapture(PlayerTeam.RED);
                                }
                            }
                        }
                        else
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
                    }
                    else if (attachedGuardianColor.Contains(PlayerTeam.RED) && attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {

                    }
                    else
                    {
                        conduitState = ConduitState.DRAINING;
                    }
                    break;

                case PlayerTeam.BLUE:
                    if (attachedGuardianColor.Contains(PlayerTeam.BLUE) && !attachedGuardianColor.Contains(PlayerTeam.RED))
                    {
                        if (isBeingAssistedByBlueGolem)
                        {
                            if (redTeamCaptureAmount > 0)
                            {
                                redTeamCaptureAmount -= assistedCaptureSpeed * Time.deltaTime;
                            }
                            else
                            {
                                blueTeamCaptureAmount += assistedCaptureSpeed * Time.deltaTime;
                                redTeamCaptureAmount = 0;

                                if (blueTeamCaptureAmount > totalCaptureAmount)
                                {
                                    blueTeamCaptureAmount = totalCaptureAmount;

                                    CompleteCapture(PlayerTeam.BLUE);
                                }
                            }
                        }
                        else
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
                    }
                    else if (attachedGuardianColor.Contains(PlayerTeam.RED) && attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {

                    }
                    else
                    {
                        conduitState = ConduitState.DRAINING;                    
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
                    if (!attachedGuardianColor.Contains(PlayerTeam.RED))
                    {
                        if (drainWaitTime <= 0)
                        {
                            conduitState = ConduitState.DRAINING;
                        }
                        else
                        {
                            drainWaitTime -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        if (drainWaitTime > 0)
                        {
                            drainWaitTime = initialDrainWaitTime;
                        }
                    }
                    break;

                case PlayerTeam.BLUE:
                    if (!attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {
                        if (drainWaitTime <= 0)
                        {
                            conduitState = ConduitState.DRAINING;
                        }
                        else
                        {
                            drainWaitTime -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        if (drainWaitTime > 0)
                        {
                            drainWaitTime = initialDrainWaitTime;
                        }
                    }
                    break;
            }
        }
        else if (conduitState == ConduitState.DRAINING)
        {
            switch (conduitColor)
            {
                case PlayerTeam.RED:
                    if (!attachedGuardianColor.Contains(PlayerTeam.RED))
                    {
                        if (redTeamCaptureAmount > 0)
                        {
                            redTeamCaptureAmount -= decaySpeed * Time.deltaTime;
                        }
                        else
                        {
                            ResetConduit();
                        }
                    }
                    else if (attachedGuardianColor.Contains(PlayerTeam.RED))
                    {
                        if (redTeamCaptureAmount < totalCaptureAmount)
                        {
                            if (isBeingAssistedByRedGolem)
                            {
                                redTeamCaptureAmount += assistedCaptureSpeed * Time.deltaTime;

                                if (redTeamCaptureAmount >= totalCaptureAmount)
                                {
                                    conduitState = ConduitState.CONTROLLED;
                                }
                            }
                            else
                            {
                                redTeamCaptureAmount += captureSpeed * Time.deltaTime;

                                if (redTeamCaptureAmount >= totalCaptureAmount)
                                {
                                    conduitState = ConduitState.CONTROLLED;
                                }
                            }
                        }
                    }       
                    break;

                case PlayerTeam.BLUE:
                    if (!attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {
                        if (blueTeamCaptureAmount > 0)
                        {
                            blueTeamCaptureAmount -= decaySpeed * Time.deltaTime;
                        }
                        else
                        {
                            ResetConduit();
                        }
                    }
                    else if (attachedGuardianColor.Contains(PlayerTeam.BLUE))
                    {
                        if (blueTeamCaptureAmount < totalCaptureAmount)
                        {
                            if (isBeingAssistedByBlueGolem)
                            {
                                blueTeamCaptureAmount += assistedCaptureSpeed * Time.deltaTime;

                                if (blueTeamCaptureAmount >= totalCaptureAmount)
                                {
                                    conduitState = ConduitState.CONTROLLED;
                                }
                            }
                            else
                            {
                                blueTeamCaptureAmount += captureSpeed * Time.deltaTime;

                                if (blueTeamCaptureAmount >= totalCaptureAmount)
                                {
                                    conduitState = ConduitState.CONTROLLED;
                                }
                            }
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
                crystalManager.CaptureCrystal(PlayerTeam.RED);

                outerRingRenderer.material.color = yellowCaptureColor;

                foreach(Renderer gem in crystalRenderer)
                {
                    gem.material.color = yellowCaptureColor;
                }

                commandManager.ConduitCapture(PlayerTeam.RED);

                PlayRedCaptureParticles();
                break;
            case PlayerTeam.BLUE:
                crystalManager.CaptureCrystal(PlayerTeam.BLUE);

                outerRingRenderer.material.color = blueCaptureColor;

                foreach (Renderer gem in crystalRenderer)
                {
                    gem.material.color = blueCaptureColor;
                }

                commandManager.ConduitCapture(PlayerTeam.BLUE);

                PlayBlueCaptureParticles();
                break;
        }

        captureSpeed = 0;
        guardianPlayerController.isCapturingOrb = false;
        guardianPlayerController.conduitCapturedList.Add(gameObject);
        guardianPlayerController.FinishCapture();
        conduitState = ConduitState.CONTROLLED;

        DrawLine();
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

        DrawLine();
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

    void DrawLine()
    {
        if (conduitState == ConduitState.CONTROLLED)
        {
            for (int i = 0; i < neighbourConduits.Length; i++)
            {
                if (neighbourConduits[i].GetComponent<ConduitController>().conduitState == ConduitState.CONTROLLED || neighbourConduits[i].GetComponent<ConduitController>().conduitState == ConduitState.HOMEBASE && neighbourConduits[i].GetComponent<ConduitController>().conduitColor == conduitColor)
                {
                    lineRendererArray[i].SetPosition(0, transform.position);
                    lineRendererArray[i].SetPosition(1, neighbourConduits[i].transform.position);
                }
            }
        }
        else if (conduitState == ConduitState.EMPTY)
        {
            for (int i = 0; i < neighbourConduits.Length; i++)
            {
                lineRendererArray[i].SetPosition(0, transform.position);
                lineRendererArray[i].SetPosition(1, transform.position);
            }
        }
    }
}
