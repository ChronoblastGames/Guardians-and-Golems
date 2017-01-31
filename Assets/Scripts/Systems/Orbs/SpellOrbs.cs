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
	private BasicCooldown cd;



	// Use this for initialization
	void Awake () {
		spellSpawn = this.transform;
		cdGlobal = GameObject.FindGameObjectWithTag ("Variable").GetComponent<GeneralVariables> ().abilityCoolDown;

		TriggerCoolDown ("globalCD");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void TriggerCoolDown(string whichCoolDown){
		switch (whichCoolDown) {

		case "globalCD":
			StartCoroutine (cd.RestartCoolDownCoroutine());

			//orbStateEngine.currentState = currentOrbState;
		break;
		case "largeCD":
			
			break;
		default :
			break;

		}
	}
}
