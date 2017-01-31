using UnityEngine;
using System.Collections;

public class PrototypeEngine : MonoBehaviour {

	BasicTimer timerToUse;
	public float timerTime;
	public bool startIntroText;
	public bool starOldManText1 = false;
	public bool starOldManText2 = false;
	public bool starOldManText3 = false;
	public bool startObjectiveText1 = false;
	public bool deadBool = false;
	public NarrativeMaster narrativeMast;
	//public bool startObjectiveText2 = false;

	// Use this for initialization
	void Start () {
		timerToUse = new BasicTimer (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (startIntroText == true) {
			NarrativeObject temp;
			temp = (narrativeMast.narrativeManager.GetNarrativeKeyByKeyName ("INT1"));
			Debug.Log (temp.narrativeObject.keyValue);
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			DoNextThing (startIntroText, starOldManText1);
		}
		if (starOldManText1 == true) {
			NarrativeObject temp;
			temp =GameObject.Find ("NarrativeMaster").GetComponent<NarrativeMaster> ().narrativeManager.GetNarrativeKeyByKeyName ("OMT1");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			DoNextThing (starOldManText1, startObjectiveText1);
		}
		if (starOldManText2 == true) {
			NarrativeObject temp;
			temp =GameObject.Find ("NarrativeMaster").GetComponent<NarrativeMaster> ().narrativeManager.GetNarrativeKeyByKeyName ("OMT2");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			DoNextThing (starOldManText2, starOldManText3);
		}
		if (starOldManText3 == true) {
			NarrativeObject temp;
			temp =GameObject.Find ("NarrativeMaster").GetComponent<NarrativeMaster> ().narrativeManager.GetNarrativeKeyByKeyName ("OMT3");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
			DoNextThing (starOldManText3, deadBool);
		}
		if (startObjectiveText1 == true) {
			NarrativeObject temp;
			temp =GameObject.Find ("NarrativeMaster").GetComponent<NarrativeMaster> ().narrativeManager.GetNarrativeKeyByKeyName ("OBJ1");
			GameObject.Find ("UIMaster").GetComponent<UIEngine> ().DisplayNarrativeText (temp.narrativeObject.keyValue);
		}

	
	}
	public void DoNextThing(bool boolToRetire, bool boolToSet){
		boolToRetire = false;
		boolToSet = true;
		timerToUse.ResetTimer(timerTime);
	}

}
