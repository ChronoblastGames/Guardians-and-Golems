using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameEngine : MonoBehaviour {

   //Redundant public State g_EngineState;
	public BasicObjectiveEngine gameObjectiveEngine = new BasicObjectiveEngine();
	public BasicStateEngine gameStateEngine = new BasicStateEngine();
	public UIEngine uiEngine;
	public NarrativeEngine narrativeEngine;
	public GameObject[] listOfTriggers;
    private BasicTimer objectiveTimer;
    private BasicTimer transitionTimer;
    private bool timerStarted = false;
    public float timeForObjective;
    private bool transitionTimerStarted = false;

	//For the switching
	public bool switchNarrative;
	private Objective objectiveSwitchChecker;
    private bool setBadEnding = false;

	//public 

	// Use this for initialization
	void Start () {
		uiEngine = GameObject.Find ("UIMaster").GetComponent<UIEngine> ();
		narrativeEngine = GameObject.Find ("NarrativeMaster").GetComponent<NarrativeEngine> ();
        objectiveTimer = new BasicTimer(0);
        transitionTimer = new BasicTimer(0);
    }
	void WinGame(){

        //string winMessage = narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (3).narrativeObject.keyValue;

        //uiEngine.DisplayNarrativeText (winMessage);
        //Time.timeScale = 0;
        SceneManager.LoadScene(2);
    }

	void LoseGame(){
        SceneManager.LoadScene(3);
        //string loseMessage = narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (4).narrativeObject.keyValue;

        //uiEngine.DisplayNarrativeText (loseMessage);
        //Time.timeScale = 0;

    }

	void NarrativeSwitchOff(){
		objectiveSwitchChecker = gameObjectiveEngine.currentObjective;
		switchNarrative = false;
	}


	// Update is called once per frame
	void Update () {

		//Start Win/Lose

        //End Win/Lose


        //Start Objective Progresses

        //Condition for Objective WalkAround
            if ((objectiveTimer.TimerIsDone() && timerStarted) || ((gameObjectiveEngine.currentObjective == gameObjectiveEngine.objectiveFullList[7]) && (GameObject.Find("TZOMLVL8Flowers2").GetComponent<EnvironmentTextAfterTime>().enabled == false) && (GameObject.Find("TZOMLVL8Military2").GetComponent<EnvironmentTextAfterTime>().enabled == false))){

                if (transitionTimerStarted == false)
                {
                    transitionTimer.ResetTimer(15f);
                    transitionTimerStarted = true;
                }
            
                if (transitionTimerStarted == true && transitionTimer.TimerIsDone()) {
                    gameObjectiveEngine.SetObjective(gameObjectiveEngine.objectiveFullList[8]);
                    timerStarted = false;
                
                }
            
            }

        //Condition for Objective CollectDebtOrLeave
        if (setBadEnding == false)
        {
            if (GameObject.Find("TZOMLVL9OldMan2").GetComponent<EnvironmentInteractions>().enabled == false)
            {
            }
        }

        //Objective Switcher
        if (objectiveSwitchChecker != gameObjectiveEngine.currentObjective){
			switchNarrative = true;
		}
		if (switchNarrative == true) {
			NarrativeSwitchOff ();
            //Objective 1 Enter house
            if (gameObjectiveEngine.objectiveFullList [2] == gameObjectiveEngine.currentObjective) {
				uiEngine.DisplayNarrativeText (narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (13).narrativeObject.keyValue);


			}
			//Objective 2 See Old Man
			if (gameObjectiveEngine.objectiveFullList [3] == gameObjectiveEngine.currentObjective) {
				uiEngine.DisplayNarrativeText (narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (14).narrativeObject.keyValue);
                //Cleanup unnecessary triggerzones
                GameObject.Find ("TZOM11").GetComponent<BoxCollider> ().enabled = false;

                //Setup Level
                GameObject.Find ("TZOM13").GetComponent<BoxCollider> ().enabled = true;

			}
			//Objective 3 Get ScrapBook
			if (gameObjectiveEngine.objectiveFullList [4] == gameObjectiveEngine.currentObjective) {
				uiEngine.DisplayNarrativeText (narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (15).narrativeObject.keyValue);
                //Setup Level

                    //Enable all triggers in TZOMLVL4
                    BoxCollider[] myArray = GameObject.Find("TZOMLVL4").GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < myArray.Length; i++) {
                        myArray[i].enabled = true;
                    }
                    
                    GameObject.Find ("TZOM14").GetComponent<BoxCollider> ().enabled = true;

			}
			//Objective 4 Feel the scrap book
			if (gameObjectiveEngine.objectiveFullList [5] == gameObjectiveEngine.currentObjective) {
				uiEngine.DisplayNarrativeText (narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (20).narrativeObject.keyValue);
                //Cleanup unnecessary triggerzones
                GameObject.Find("TZOM13").GetComponent<BoxCollider>().enabled = false;
                GameObject.Find ("TZOM14").GetComponent<Renderer> ().enabled = false;
				GameObject.Find ("TZOMScrapBook").GetComponent<Renderer> ().enabled = false;
                
                //Setup Level
                GameObject.Find ("TZOM15").GetComponent<BoxCollider> ().enabled = true;

			}
			//Objective 5 Give it to Old Man
			if (gameObjectiveEngine.objectiveFullList [6] == gameObjectiveEngine.currentObjective) {
				uiEngine.DisplayNarrativeText (narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (21).narrativeObject.keyValue);
				//Cleanup unnecessary triggerzones
				GameObject.Find ("TZOM14").GetComponent<BoxCollider> ().enabled = false;
				GameObject.Find ("TZOM14").GetComponent<Renderer> ().enabled = false;
                GameObject.Find("TZOMLVL4").SetActive(false);
                

                //Setup Level
                


			}
            //Walk Around
			if (gameObjectiveEngine.objectiveFullList [7] == gameObjectiveEngine.currentObjective) {
				uiEngine.DisplayNarrativeText (narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (26).narrativeObject.keyValue);
                //Cleanup unnecessary triggerzones
                GameObject.Find("TZOM14").GetComponent<EnvironmentTextAfterTime>().enabled = false;
                
                GameObject.Find("TZOMScrapBook").SetActive(false);
                GameObject.Find("TZOM15").SetActive(false);


                //SetupLevel
                BoxCollider[] myArray = GameObject.Find("TZOMLVL8").GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < myArray.Length; i++)
                {
                    myArray[i].enabled = true;
                }
                objectiveTimer.ResetTimer(timeForObjective);
                timerStarted = true;


            }
            //CollectDebt

			if (gameObjectiveEngine.objectiveFullList [8] == gameObjectiveEngine.currentObjective) {
				uiEngine.DisplayNarrativeText (narrativeEngine.narrativeManager.GetHeaviestNarrativeKey (36).narrativeObject.keyValue);
                //Cleanup
                GameObject.Find("TZOMLVL8").SetActive(false);

                //Setup
                BoxCollider[] myArray = GameObject.Find("TZOMLVL9").GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < myArray.Length; i++)
                {
                    myArray[i].enabled = true;
                }


            }

		}
		//End Objective Progresses
		//Start State Conditions
			if (gameStateEngine.stateFullList[0] == gameStateEngine.currentState){
			}
			if (gameStateEngine.stateFullList[1] == gameStateEngine.currentState){
			}
			
		//EndStateConditions

	}

}
