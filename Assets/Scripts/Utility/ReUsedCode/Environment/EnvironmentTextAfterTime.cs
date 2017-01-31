using UnityEngine;
using System.Collections;

public class EnvironmentTextAfterTime : MonoBehaviour {

	public NarrativeEngine narrativeEngine;
    public GameObject uiMaster;
    public bool onTriggerEnter;
	public bool onTriggerExit;
	public bool onTriggerStay;
	public int narrativeLayer;
	public string narrativeKey;
	public bool t4Layerf4Key;
    public float timeToWait;
    private BasicTimer timer;
    private bool isTriggered = false;

	void Start(){
        timer = new BasicTimer(0);
    }
    void Update() {
        if (isTriggered == true) {
            if ((timer.TimerIsDone()) && (timer.GetRatio() != 0)) {
                SayNarrative();
                this.enabled = false;
            }
        }
    }

	void OnTriggerEnter(Collider other){
		if (onTriggerEnter == true) {
            isTriggered = true;
            timer.ResetTimer(timeToWait);
            
        }
	}
	void OnTriggerStay(Collider other){
		if (onTriggerStay == true) {
            isTriggered = true;
            timer.ResetTimer(timeToWait);
        }
	}
	void OnTriggerExit(Collider other){
		if (onTriggerExit == true) {
            isTriggered = true;
            timer.ResetTimer(timeToWait);
        }
	}
    void SayNarrative() {
        NarrativeObject narrObj = narrativeEngine.narrativeManager.GetHeaviestNarrativeKey(narrativeLayer);
        narrativeEngine.narrativeManager.SetcurrentNarrativeText(narrObj);
        string narrText = narrativeEngine.narrativeManager.currentNarrativeText.narrativeObject.keyValue;
        uiMaster.GetComponent<UIEngine>().DisplayNarrativeText(narrText);
        Debug.Log("I worked");
    }
}
