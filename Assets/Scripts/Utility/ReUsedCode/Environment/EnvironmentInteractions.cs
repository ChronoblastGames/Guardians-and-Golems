using UnityEngine;
using System.Collections;

public class EnvironmentInteractions : MonoBehaviour {

	public GameObject objectToInteractWith;
    //public bool valueToSet;
    public bool deactivateOnUse;
	public GameObject[] mastersList;
	public GameObject masterToUse;
	public GameObject uiMaster;
	public State stateToSet;
	public Objective objectiveToSet;
	public int narrativeLayer;
	public string narrativeKey;
	public string commandToUse;
	public bool onTriggerEnter;
	public bool onTriggerExit;
	public bool onTriggerStay;
	public bool t4Layerf4Key;
	//SetObjective
	//SetState
	//SetBool
	//SetNarrativeKey

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (onTriggerEnter == true) {
			if (other.gameObject == objectToInteractWith) {
				Debug.Log("I'm executing the OnTriggerEnterScript, you are trying to pass " + commandToUse + " as a command");
				switch (commandToUse) {
				case "SetObjective":
					SetObjective ();
					break;
				case "SetState":
					SetState ();
					break;
				case "SetBool":
					SetBool ();
					break;
				case "SetNarrativeKey":
					DoNarrative ();
					break;
				default:
					break;
				}
			}
		}
        if (deactivateOnUse == true) {
            this.enabled = false;
        }
	}
	void OnTriggerExit(Collider other){
		if (onTriggerExit == true) {
			if (other.gameObject == objectToInteractWith) {
				Debug.Log("I'm executing the OnTriggerExitScript, you are trying to pass " + commandToUse + " as a command");
				switch (commandToUse) {
				case "SetObjective":
					SetObjective ();
					break;
				case "SetState":
					SetState ();
					break;
				case "SetBool":
					SetBool ();
					break;
				case "SetNarrativeKey":
					DoNarrative ();
					break;
				default:
					break;
				}
			}
		}
        if (deactivateOnUse == true)
        {
            this.enabled = false;
        }
    }
	void OnTriggerStay(Collider other){
		if (onTriggerStay == true) {
			if (other.gameObject == objectToInteractWith) {
				Debug.Log("I'm executing the OnTriggerStayScript, you are trying to pass " + commandToUse + " as a command");
				switch (commandToUse) {
				case "SetObjective":
					SetObjective ();
					break;
				case "SetState":
					SetState ();
					break;
				case "SetBool":
					SetBool ();
					break;
				case "SetNarrativeKey":
					DoNarrative ();
					break;
				default:
					break;
				}
			}
		}
        if (deactivateOnUse == true)
        {
            this.enabled = false;
        }
    }

	public void SetObjective()
	{
		masterToUse.GetComponent<GameEngine> ().gameObjectiveEngine.SetObjective (objectiveToSet);
	}
	public void SetState(){
		if (masterToUse.name == "NarrativeMaster") {
			masterToUse.GetComponent<NarrativeEngine> ().narrativeStateEngine.SetState (stateToSet);
		}
		if (masterToUse.name == "UIMaster") {
			masterToUse.GetComponent<UIEngine> ().uiStateEngine.SetState (stateToSet);
		}
		if (masterToUse.name == "GameMaster") {
			masterToUse.GetComponent<GameEngine> ().gameStateEngine.SetState (stateToSet);
		}
		else{
			Debug.Log("There was not a master set to inherit a state");
		}
		
	}
	public void SetBool(){
		Debug.Log("This script doesn't work, thanks for trying!");
		//valueToSet = true;
	}
	public void DoNarrative(){
		Debug.Log ("Running do Narrative, true = layer, false = key " + t4Layerf4Key);
		if (t4Layerf4Key == true) {
			GameObject nm = GameObject.Find ("NarrativeMaster");
			NarrativeEngine ne = nm.GetComponent<NarrativeEngine> ();
			BasicNarrativeEngine bse = ne.narrativeManager;
			NarrativeObject bst = bse.GetHeaviestNarrativeKey (narrativeLayer);
			Debug.Log (bst);
			string tts = bst.narrativeObject.keyValue;
			//string textToShow= GameObject.Find("NarrativeMaster").GetComponent<NarrativeEngine> ().narrativeManager.GetHeaviestNarrativeKey (narrativeLayer).narrativeObject.keyValue;
			uiMaster.GetComponent<UIEngine> ().DisplayNarrativeText (tts);
		} else {
		
		}
	}
}
