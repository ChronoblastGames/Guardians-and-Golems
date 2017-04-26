using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConduitController : MonoBehaviour 
{
    private CommandManager commandManager;
    private CrystalManager crystalManager;

    private Animator conduitAnimator;

    private GuardianPlayerController redTeamGuardianPlayerController;
    private GuardianPlayerController blueTeamGuardianPlayerController;

    [Header("Conduit Attributes")]
    public ConduitState conduitState;
    public PlayerTeam conduitColor;

    public List <PlayerTeam> attachedGuardians;

    public float captureSpeed;
    [Space(10)]
    public float currentCaptureAmount;
    public float totalCaptureAmount;
    [Space(10)]

    public GameObject centerCrystal;

    public bool isRedGolemInRange = false;
    public bool isBlueGolemInRange = false;

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

    private void Start()
    {
        InitializeConduit();
    }

    private void FixedUpdate()
    {
  
    }

    void InitializeConduit()
    {
        crystalManager = GameObject.FindGameObjectWithTag("CrystalManager").GetComponent<CrystalManager>();

        commandManager = GameObject.FindGameObjectWithTag("CommandManager").GetComponent<CommandManager>();

        conduitAnimator = GetComponent<Animator>();

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
                    currentCaptureAmount += Time.fixedDeltaTime * captureSpeed;
                }
                else if (currentCaptureAmount >= totalCaptureAmount)
                {
                    CaptureConduit(teamColor);
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

                PlayConduitCaptureEffects(PlayerTeam.RED);
                break;

            case PlayerTeam.BLUE:
                blueTeamGuardianPlayerController.CaptureConduit(gameObject);

                PlayConduitCaptureEffects(PlayerTeam.BLUE);
                break;
        }

        currentCaptureAmount = totalCaptureAmount;

        conduitState = ConduitState.CAPTURED;
    }

    public void ResetConduit()
    {
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
            StartCoroutine(ConduitDisable(length));
        }
    }

    private IEnumerator ConduitDisable (float disableTime)
    {
        conduitState = ConduitState.DISABLED;

        yield return new WaitForSeconds(disableTime);

        if (conduitColor == PlayerTeam.RED || conduitColor == PlayerTeam.BLUE)
        {
            conduitState = ConduitState.CAPTURED;
        }
        else
        {
            conduitState = ConduitState.EMPTY;
        }

        yield return null;
    }

    public void PlayConduitCaptureEffects(PlayerTeam teamColor)
    {
        switch (teamColor)
        {
            case PlayerTeam.RED:
                break;

            case PlayerTeam.BLUE:
                break;
        }
    }

    void DrawLine() //Rewrite
    {
        if (conduitState == ConduitState.CAPTURED)
        {
            for (int i = 0; i < neighbourConduits.Length; i++)
            {
                if (neighbourConduits[i].GetComponent<ConduitController>().conduitState == ConduitState.CAPTURED || neighbourConduits[i].GetComponent<ConduitController>().conduitState == ConduitState.HOMEBASE && neighbourConduits[i].GetComponent<ConduitController>().conduitColor == conduitColor)
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
