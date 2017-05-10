using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConduitController : MonoBehaviour 
{
    private UIManager UI;

    private CommandManager commandManager;
    private CrystalManager crystalManager;

    private GuardianPlayerController redTeamGuardianPlayerController;
    private GuardianPlayerController blueTeamGuardianPlayerController;

    private GolemResources redTeamGolemResources;
    private GolemResources blueTeamGolemResources;

    private Animator conduitAnimator;

    [Header("Conduit Attributes")]
    public ConduitState conduitState;
    public PlayerTeam conduitColor;

    public List <PlayerTeam> attachedGuardians;
    public List<PlayerTeam> golemInRange;

    public GameObject centerCrystal;

    [Header("Conduit Capture Values")]
    public float currentCaptureSpeed;
    [Space(10)]
    public float baseCaptureSpeed;
    public float acceleratedCaptureSpeed;
    public float slowedCaptureSpeed;
    public float loseDrainSpeed;

    [Space(10)]
    public float currentCaptureAmount;
    public float totalCaptureAmount;
    [Space(10)]
    public float capturePercentage;
    [Space(10)]
    public float captureGracePeriod;

    private bool canBeRecaptured = true;

    [Header("Attached Conduits")]
    public GameObject[] neighbourConduits;

    public LineRenderer[] lineRendererArray;

    [Header("Conduit Renderer Attributes")]
    public Renderer[] crystalRenderer;

    public Renderer[] innerGemRenderer;
    public Renderer[] outerGemRenderer;

    public Renderer outerRingRenderer;

    [Space(10)]
    public Color outerRingColor;
    public Color gemColor;

    [Header("Conduit Particles Systems")]
    public ParticleSystem redCapturedParticles;
    public ParticleSystem blueCapturedParticles;

    public ParticleSystem[] ringParticlesRed;
    public ParticleSystem[] ringParticlesBlue;

    [Header("Conduit UI Attributes")]
    public Image conduitFillImage;

    private void Start()
    {
        InitializeConduit();
    }

    private void FixedUpdate()
    {
        ManageConduitProgressEffects();
        ManageConduitOutline();
        ManageGolems();
        ManageConduitState();
    }

    void InitializeConduit()
    {
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        commandManager = GameObject.FindGameObjectWithTag("CommandManager").GetComponent<CommandManager>();

        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        conduitAnimator = GetComponent<Animator>();

        redTeamGolemResources = GameObject.FindGameObjectWithTag("GolemRed").GetComponent<GolemResources>();
        blueTeamGolemResources = GameObject.FindGameObjectWithTag("GolemBlue").GetComponent<GolemResources>();

        redTeamGuardianPlayerController = GameObject.FindGameObjectWithTag("GuardianRed").GetComponent<GuardianPlayerController>();
        blueTeamGuardianPlayerController = GameObject.FindGameObjectWithTag("GuardianBlue").GetComponent<GuardianPlayerController>();

        if (conduitState == ConduitState.HOMEBASE)
        {
            switch (conduitColor)
            {
                case PlayerTeam.RED:
                    SetupHomeBase(PlayerTeam.RED);
                    break;

                case PlayerTeam.BLUE:
                    SetupHomeBase(PlayerTeam.BLUE);
                    break;
            }
        }
        else
        {
            currentCaptureAmount = 0;

            currentCaptureSpeed = baseCaptureSpeed;

            conduitState = ConduitState.EMPTY;
            conduitColor = PlayerTeam.NONE;
        }
    }

    public void GuardianAttachToConduit(PlayerTeam teamColor)
    {
        attachedGuardians.Add(teamColor);
    }

    public void GuardianDeattachToConduit(PlayerTeam teamColor)
    {
        attachedGuardians.Remove(teamColor);
    }

    public bool CanCaptureConduit(PlayerTeam teamColor)
    {
        if (conduitState == ConduitState.EMPTY)
        {
            foreach (GameObject conduit in neighbourConduits)
            {
                if (conduit.GetComponent<ConduitController>().conduitState == ConduitState.CAPTURED || conduit.GetComponent<ConduitController>().conduitState == ConduitState.HOMEBASE)
                {
                    if (conduit.GetComponent<ConduitController>().conduitColor == teamColor)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        else
        {
            return false;
        }
    }

    public bool CanIncreaseCapture(PlayerTeam teamColor)
    {
        if (conduitState == ConduitState.CAPTURING)
        {
            if (conduitColor == teamColor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (conduitState == ConduitState.DRAINING)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanCastAbility()
    {
        if (conduitState == ConduitState.DISABLED)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CanReverseCapture(PlayerTeam teamColor)
    {
        if (conduitState == ConduitState.CAPTURED)
        {
            if (conduitColor != teamColor)
            {
                if (canBeRecaptured)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void ConduitCapturingSetup(PlayerTeam teamColor)
    {
        if (conduitState == ConduitState.EMPTY)
        {
            conduitColor = teamColor;

            conduitState = ConduitState.CAPTURING;

            conduitFillImage.gameObject.SetActive(true);

            if (teamColor == PlayerTeam.RED)
            {
                conduitFillImage.color = Colors.YellowTeamColor;
            }
            else if (teamColor == PlayerTeam.BLUE)
            {
                conduitFillImage.color = Colors.BlueTeamColor;
            }
        }
    }

    public void ConduitDrainSetup(PlayerTeam teamColor)
    {
        if (conduitState == ConduitState.CAPTURED)
        {
            conduitFillImage.gameObject.SetActive(true);

            conduitState = ConduitState.DRAINING;
        }
    }

    public void CapturingConduit(PlayerTeam teamColor)
    {
        if (conduitState == ConduitState.CAPTURING)
        {
            if (teamColor == conduitColor)
            {
                if (currentCaptureAmount <= totalCaptureAmount)
                {
                    currentCaptureAmount += Time.deltaTime * currentCaptureSpeed;
                }
                else if (currentCaptureAmount >= totalCaptureAmount)
                {
                    CaptureConduit(teamColor);
                }
            }
        }
        else if (conduitState == ConduitState.DRAINING)
        {
            if (teamColor != conduitColor)
            {
                if (currentCaptureAmount >= 0)
                {
                    currentCaptureAmount -= Time.deltaTime * currentCaptureSpeed;
                }
                else if (currentCaptureAmount <= 0)
                {
                    ResetConduit();
                }
            }
            else if (teamColor == conduitColor)
            {
                if (currentCaptureAmount < totalCaptureAmount)
                {
                    currentCaptureAmount += Time.deltaTime * currentCaptureSpeed;
                }
                else if (currentCaptureAmount >= totalCaptureAmount)
                {
                    currentCaptureAmount = totalCaptureAmount;

                    conduitState = ConduitState.CAPTURED;

                    conduitFillImage.gameObject.SetActive(false);
                }
            }
        }
    }

    private void CaptureConduit(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redTeamGuardianPlayerController.CaptureConduit(gameObject);

                redTeamGuardianPlayerController.isCapturingConduit = false;

                UI.RequestGenericText("Captured Conduit", transform, Colors.YellowTeamColor);

                commandManager.ConduitCapture(PlayerTeam.RED);

                ConduitCaptureEffects(PlayerTeam.RED);
                break;

            case PlayerTeam.BLUE:
                blueTeamGuardianPlayerController.CaptureConduit(gameObject);

                blueTeamGuardianPlayerController.isCapturingConduit = false;

                UI.RequestGenericText("Captured Conduit", transform, Colors.BlueTeamColor);

                commandManager.ConduitCapture(PlayerTeam.BLUE);

                ConduitCaptureEffects(PlayerTeam.BLUE);
                break;
        }

        currentCaptureAmount = totalCaptureAmount;

        conduitFillImage.gameObject.SetActive(false);

        conduitState = ConduitState.CAPTURED;

        DrawLine();
    }

    public void ResetConduit()
    {     
        if (conduitColor != PlayerTeam.NONE)
        {
            if (conduitColor == PlayerTeam.RED)
            {
                commandManager.LoseConduit(PlayerTeam.RED);

                crystalManager.RemoveCrystal(PlayerTeam.RED);

                redTeamGuardianPlayerController.LoseConduit(gameObject);
            }
            else if (conduitColor == PlayerTeam.BLUE)
            {
                commandManager.LoseConduit(PlayerTeam.BLUE);

                crystalManager.RemoveCrystal(PlayerTeam.BLUE);

                blueTeamGuardianPlayerController.LoseConduit(gameObject);
            }
        }

        currentCaptureAmount = 0;
        conduitState = ConduitState.EMPTY;

        foreach (Renderer gemRenderer in outerGemRenderer)
        {
            gemRenderer.material.color = gemColor;
        }

        foreach (Renderer gemRenderer in innerGemRenderer)
        {
            gemRenderer.material.color = gemColor;
        }

        foreach (Renderer gemRenderer in crystalRenderer)
        {
            gemRenderer.material.color = gemColor;
        }

        outerRingRenderer.material.color = outerRingColor;

        conduitFillImage.gameObject.SetActive(false);

        conduitFillImage.color = Colors.White;

        conduitColor = PlayerTeam.NONE;

        DrawLine();
    }

    private void ManageConduitState()
    {
        if (conduitState == ConduitState.LOSING)
        {
            if (conduitColor != PlayerTeam.NONE)
            {
                if (currentCaptureAmount > 0)
                {
                    currentCaptureAmount -= Time.fixedDeltaTime * loseDrainSpeed;
                }
                else if (currentCaptureAmount <= 0)
                {
                    ResetConduit();
                }
            }
        }
    }

    private void SetupHomeBase(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:

                foreach (Renderer gemRenderer in outerGemRenderer)
                {
                    gemRenderer.material.color = Colors.YellowTeamColor;
                }

                foreach (Renderer gemRenderer in innerGemRenderer)
                {
                    gemRenderer.material.color = Colors.YellowTeamColor;
                }

                foreach (Renderer gemRenderer in crystalRenderer)
                {
                    gemRenderer.material.color = Colors.YellowTeamColor;
                }

                redTeamGuardianPlayerController.conduitCapturedList.Add(gameObject);
                break;

            case PlayerTeam.BLUE:

                foreach (Renderer gemRenderer in outerGemRenderer)
                {
                    gemRenderer.material.color = Colors.BlueTeamColor;
                }

                foreach (Renderer gemRenderer in innerGemRenderer)
                {
                    gemRenderer.material.color = Colors.BlueTeamColor;
                }

                foreach (Renderer gemRenderer in crystalRenderer)
                {
                    gemRenderer.material.color = Colors.BlueTeamColor;
                }

                blueTeamGuardianPlayerController.conduitCapturedList.Add(gameObject);
                break;
        }
    }

    public void DisableConduit (float length, PlayerTeam teamColor)
    {
        if (conduitColor != teamColor)
        {
            if (conduitState != ConduitState.HOMEBASE)
            {
                StartCoroutine(ConduitDisable(length));
            }
        }
    }

    private IEnumerator ConduitDisable (float disableTime)
    {
        ConduitState previousState = conduitState;

        foreach (Renderer gemRenderer in crystalRenderer)
        {
            gemRenderer.material.color = Colors.DarkSlateGray;
        }

        centerCrystal.GetComponent<Renderer>().material.color = Colors.DarkSlateGray;

        conduitState = ConduitState.DISABLED;

        yield return new WaitForSeconds(disableTime);

        conduitState = previousState;

        if (conduitColor == PlayerTeam.RED)
        {
            foreach (Renderer gemRenderer in crystalRenderer)
            {
                gemRenderer.material.color = Colors.YellowTeamColor;
            }

            centerCrystal.GetComponent<Renderer>().material.color = Colors.YellowTeamColor;
        }
        else if (conduitColor == PlayerTeam.BLUE)
        {
            foreach (Renderer gemRenderer in crystalRenderer)
            {
                gemRenderer.material.color = Colors.BlueTeamColor;
            }

            centerCrystal.GetComponent<Renderer>().material.color = Colors.BlueTeamColor;
        }
        else if (conduitColor == PlayerTeam.NONE)
        {
            foreach (Renderer gemRenderer in crystalRenderer)
            {
                gemRenderer.material.color = gemColor;
            }

            centerCrystal.GetComponent<Renderer>().material.color = gemColor;
        }

        yield return null;
    }

    private void ManageConduitProgressEffects()
    {
        capturePercentage = (currentCaptureAmount / totalCaptureAmount) * 100;

        conduitFillImage.fillAmount = currentCaptureAmount / totalCaptureAmount;

        switch (conduitState)
        {
            case ConduitState.CAPTURING:
                if (capturePercentage >= 30f && capturePercentage <= 31f)
                {
                    FirstStateConduitCapturingEffect(conduitColor);
                }
                else if (capturePercentage >= 60f && capturePercentage <= 61f)
                {
                    SecondStateConduitCapturingEffect(conduitColor);
                }
                else if (capturePercentage >= 85f && capturePercentage <= 86f)
                {
                    ThirdStateConduitCapturingEffect(conduitColor);
                }
                break;

            case ConduitState.DRAINING:
                if (capturePercentage >= 84f && capturePercentage <= 85f)
                {
                    FirstStateConduitDrainEffect();
                }
                else if (capturePercentage >= 59f && capturePercentage <= 60f)
                {
                    SecondStateConduitDrainEffect();
                }
                else if (capturePercentage >= 31f && capturePercentage <= 30f)
                {
                    ThirdStateConduitDrainEffect();
                }
                break;

            case ConduitState.LOSING:
                if (capturePercentage >= 84f && capturePercentage <= 85f)
                {
                    FirstStateConduitDrainEffect();
                }
                else if (capturePercentage >= 59f && capturePercentage <= 60f)
                {
                    SecondStateConduitDrainEffect();
                }
                else if (capturePercentage >= 31f && capturePercentage <= 30f)
                {
                    ThirdStateConduitDrainEffect();
                }
                break;
        }
    }

    private void FirstStateConduitCapturingEffect(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                ringParticlesRed[0].Play();

                foreach (Renderer currentRenderer in outerGemRenderer)
                {
                    currentRenderer.material.color = Colors.YellowTeamColor;
                }
                break;

            case PlayerTeam.BLUE:
                ringParticlesBlue[0].Play();

                foreach (Renderer currentRenderer in outerGemRenderer)
                {
                    currentRenderer.material.color = Colors.BlueTeamColor;
                }
                break;
        }
    }

    private void SecondStateConduitCapturingEffect(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                ringParticlesRed[1].Play();

                foreach (Renderer currentRenderer in innerGemRenderer)
                {
                    currentRenderer.material.color = Colors.YellowTeamColor;
                }
                break;

            case PlayerTeam.BLUE:
                ringParticlesBlue[1].Play();

                foreach (Renderer currentRenderer in innerGemRenderer)
                {
                    currentRenderer.material.color = Colors.BlueTeamColor;
                }
                break;
        }
    }

    private void ThirdStateConduitCapturingEffect(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                ringParticlesRed[2].Play();

                foreach(Renderer currentRenderer in crystalRenderer)
                {
                    currentRenderer.material.color = Colors.YellowTeamColor;
                }
                break;

            case PlayerTeam.BLUE:
                ringParticlesBlue[2].Play();

                foreach (Renderer currentRenderer in crystalRenderer)
                {
                    currentRenderer.material.color = Colors.BlueTeamColor;
                }
                break;
        }
    }

    private void FirstStateConduitDrainEffect()
    {
        centerCrystal.GetComponent<Renderer>().material.color = gemColor;

        foreach(Renderer currentRenderer in crystalRenderer)
        {
            currentRenderer.material.color = gemColor;
        }
    }

    private void SecondStateConduitDrainEffect()
    {
        foreach (Renderer currentRenderer in innerGemRenderer)
        {
            currentRenderer.material.color = gemColor;
        }
    }

    private void ThirdStateConduitDrainEffect()
    {
        foreach (Renderer currentRenderer in outerGemRenderer)
        {
            currentRenderer.material.color = gemColor;
        }
    }

    private void ConduitCaptureEffects(PlayerTeam teamColor) //Plays Captured Conduit Effects
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                redCapturedParticles.Play();

                centerCrystal.GetComponent<Renderer>().material.color = Colors.YellowTeamColor;
                break;

            case PlayerTeam.BLUE:
                blueCapturedParticles.Play();

                centerCrystal.GetComponent<Renderer>().material.color = Colors.BlueTeamColor;
                break;
        }
    }

    private void ManageConduitOutline() //Manages Outlines for Guardians attached to Conduit
    {
        if (attachedGuardians.Count > 0)
        {
            if (attachedGuardians.Contains(PlayerTeam.RED) && attachedGuardians.Contains(PlayerTeam.BLUE))
            {
                outerRingRenderer.material.color = Colors.Green;
            }
            else if (attachedGuardians.Contains(PlayerTeam.RED))
            {
                outerRingRenderer.material.color = Colors.YellowTeamColor;
            }
            else if (attachedGuardians.Contains(PlayerTeam.BLUE))
            {
                outerRingRenderer.material.color = Colors.BlueTeamColor;
            }
        }
        else
        {
            outerRingRenderer.material.color = outerRingColor;
        }     
    }

    private void ManageGolems() //Manages Golem Participation in Capturing
    {
        if (conduitState == ConduitState.CAPTURING)
        {
            if (golemInRange.Contains(PlayerTeam.RED) && golemInRange.Contains(PlayerTeam.BLUE))
            {
                currentCaptureSpeed = baseCaptureSpeed;
            }
            else if (golemInRange.Contains(PlayerTeam.RED) && !redTeamGolemResources.isDead)
            {
                if (conduitColor == PlayerTeam.RED)
                {
                    currentCaptureSpeed = acceleratedCaptureSpeed;
                }
                else if (conduitColor == PlayerTeam.BLUE)
                {
                    currentCaptureSpeed = slowedCaptureSpeed;
                }
            }
            else if (golemInRange.Contains(PlayerTeam.BLUE) && !blueTeamGolemResources.isDead)
            {
                if (conduitColor == PlayerTeam.RED)
                {
                    currentCaptureSpeed = slowedCaptureSpeed;
                }
                else if (conduitColor == PlayerTeam.BLUE)
                {
                    currentCaptureSpeed = acceleratedCaptureSpeed;
                }
            }
            else
            {
                currentCaptureSpeed = baseCaptureSpeed;
            }
        }
        else if (conduitState == ConduitState.DRAINING)
        {
            if (golemInRange.Contains(PlayerTeam.RED) && golemInRange.Contains(PlayerTeam.BLUE))
            {
                currentCaptureSpeed = baseCaptureSpeed;
            }
            else if (golemInRange.Contains(PlayerTeam.RED) && !redTeamGolemResources.isDead)
            {
                if (conduitColor == PlayerTeam.RED)
                {
                    currentCaptureSpeed = slowedCaptureSpeed;
                }
                else if (conduitColor == PlayerTeam.BLUE)
                {
                    currentCaptureSpeed = acceleratedCaptureSpeed;
                }
            }
            else if (golemInRange.Contains(PlayerTeam.BLUE) && !blueTeamGolemResources.isDead)
            {
                if (conduitColor == PlayerTeam.RED)
                {
                    currentCaptureSpeed = acceleratedCaptureSpeed;
                }
                else if (conduitColor == PlayerTeam.BLUE)
                {
                    currentCaptureSpeed = slowedCaptureSpeed;
                }
            }
            else
            {
                currentCaptureSpeed = baseCaptureSpeed;
            }
        }
        else
        {
            currentCaptureSpeed = baseCaptureSpeed;
        }
    }

    void DrawLine() //Draws Lines Between Connect Conduits
    {
        if (conduitState == ConduitState.CAPTURED)
        {
            for (int i = 0; i < neighbourConduits.Length; i++)
            {
                if (neighbourConduits[i].GetComponent<ConduitController>().conduitState == ConduitState.CAPTURED || neighbourConduits[i].GetComponent<ConduitController>().conduitState == ConduitState.HOMEBASE)
                {
                    if (neighbourConduits[i].GetComponent<ConduitController>().conduitColor == conduitColor)
                    {
                        lineRendererArray[i].SetPosition(0, transform.position);
                        lineRendererArray[i].SetPosition(1, neighbourConduits[i].transform.position);

                        if (conduitColor == PlayerTeam.RED)
                        {
                            lineRendererArray[i].material.color = Colors.YellowTeamColor;
                        }
                        else if (conduitColor == PlayerTeam.BLUE)
                        {
                            lineRendererArray[i].material.color = Colors.BlueTeamColor;
                        }
                    }          
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

    private IEnumerator CaptureGracePeriod (float captureGracePeriod)
    {
        canBeRecaptured = false;

        yield return new WaitForSeconds(captureGracePeriod);

        canBeRecaptured = true;
    }
}
