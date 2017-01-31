using UnityEngine;
using System.Collections;

public class NarrativeMaster : MonoBehaviour {

	public BasicNarrativeObjectEngine narrativeManager;
	BasicTimer timerToUse;
	public float timerTime;
	public bool goTimer;
	public bool startIntroText = true;
	public bool starOldManText1 = false;
	public bool starOldManText2 = false;
	public bool starOldManText3 = false;
	public bool startObjectiveText1 = false;
	public bool deadBool = false;


	// Use this for initialization
	void Start () {
		Debug.Log (narrativeManager.narrativeFullList.Count + " elements in the list");
		timerToUse = new BasicTimer (0);
		goTimer = true;
		//narrativeManager.ListAllNarrativeKeys ();
	}

	// Update is called once per frame
	// Update is called once per frame
	void Update () {
		
		if (startIntroText == true && goTimer == true) {
			
			NarrativeObject temp;
			temp = narrativeManager.GetNarrativeKeyByKeyName ("INT1");
			Debug.Log (temp.narrativeObject.keyValue);
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			timerToUse.ResetTimer (5f);
			goTimer = false;

//			DoNextThing (startIntroText, starOldManText1);
//			Debug.Log ("This just happened" + starOldManText1);
		}
		if (timerToUse.TimerIsDone () && startIntroText == true) {
			startIntroText = false;
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisableNarrativeText ();
			goTimer = true;
		}
		if (starOldManText1 == true && timerToUse.TimerIsDone()) {
			NarrativeObject temp;
			temp =narrativeManager.GetNarrativeKeyByKeyName ("OMT1");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			starOldManText1 = false;
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisableNarrativeText ();
//			DoNextThing (starOldManText1, startObjectiveText1);
		}
		if (starOldManText2 == true) {
			NarrativeObject temp;
			temp =narrativeManager.GetNarrativeKeyByKeyName ("OMT2");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			starOldManText2 = false;
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisableNarrativeText ();
//			DoNextThing (starOldManText2, starOldManText3);
		}
		if (starOldManText3 == true) {
			NarrativeObject temp;
			temp =narrativeManager.GetNarrativeKeyByKeyName ("OMT3");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			starOldManText3 = false;
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisableNarrativeText ();
//			DoNextThing (starOldManText3, deadBool);
		}
		if (startObjectiveText1 == true) {
			NarrativeObject temp;
			temp =narrativeManager.GetNarrativeKeyByKeyName ("OBJ1");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			startObjectiveText1 = false;
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisableNarrativeText ();
		}


	}
	public void DoNextThing(bool boolToRetire, bool boolToSet){
		boolToRetire = false;
		Debug.Log (boolToRetire + "this should be flase");
		boolToSet = true;
		Debug.Log (boolToSet);
		timerToUse.ResetTimer(timerTime);
	}
}
