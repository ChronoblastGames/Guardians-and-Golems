using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellOrbs : MonoBehaviour {

	[SerializeField]
	private Transform spellSpawn;
	public int user;
	[SerializeField]
	private float cdGlobal;
	private float cdLong;
	public bool canAbility;
	private BasicCooldown cd;

	// Use this for initialization
	void Awake () {
		cd = new BasicCooldown ();
		spellSpawn = this.transform;
        cdGlobal = GameObject.FindObjectOfType<GeneralVariables>().spellOrbCoolDown;
		//IEnumerator cdOrbReset = cd.RestartCoolDownCoroutine();
		cd.cdTime = cdGlobal;
		//TriggerCoolDown ("globalCD");
	}
	
	// Update is called once per frame
	void Update () {
		if (cd.cdStateEngine.GetState () == cd.possibleStates[2]) {
			Debug.Log (cd.cdStateEngine.currentState.stateName + " " );
			canAbility = true;
		}
		if (Input.GetKey(KeyCode.Alpha3)){
			TriggerCoolDown("globalCD");
		}
	}
	void TriggerCoolDown(string whichCoolDown){
		Debug.Log ("Running Trigger CoolDown");
		switch (whichCoolDown) {

		case "globalCD":
			StartCoroutine (cd.RestartCoolDownCoroutine());
			Debug.Log ("Running the GCD " + cd.cdTimer.GetRatio ());
			//orbStateEngine.currentState = currentOrbState;
		break;
		case "largeCD":
			
			break;
		default :
			break;

		}
	}
}
