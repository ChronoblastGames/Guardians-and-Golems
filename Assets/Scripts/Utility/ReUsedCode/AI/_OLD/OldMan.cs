using UnityEngine;
using System.Collections;

public class OldMan : MonoBehaviour {

	public NarrativeMaster narrativeTracker;

	// Use this for initialization
	void Start () {
		narrativeTracker = GameObject.Find ("NarrativeMastero").GetComponent<NarrativeMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			
				narrativeTracker.starOldManText1 = true;

		}
	
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {

				narrativeTracker.starOldManText2 = true;
		}
	}
}
