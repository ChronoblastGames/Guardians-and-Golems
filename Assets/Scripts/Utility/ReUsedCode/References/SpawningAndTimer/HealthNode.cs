using UnityEngine;
using System.Collections;

public class HealthNode : MonoBehaviour {


	//Ask Rene how I would integrate this who class of an object that respawns after it


	public Transform respawnLocation;
	public float healAmount =75f;
	public BasicTimer respawnTimer;
	public float respawnDelay = 30;
	public bool canSpawn = false;

	void Start(){
		respawnTimer = new BasicTimer(5);
	}
	void Update(){
		if (respawnTimer.TimerIsDone ()) {
			canSpawn = true;
		}
		if (canSpawn == true && gameObject.GetComponent<BoxCollider> ().enabled == false) {
			respawnNode ();
		}
	}
	void OnTriggerEnter(Collider other){

		//if the enemy picks it up, in case I want a specific rule later
		if (other.gameObject.layer == 10) {
			despawnNode (respawnDelay);
		}
		//if the player picks it up
		if (other.gameObject.tag == "Player" || other.gameObject.layer == 13){
			despawnNode (respawnDelay);
		}
	}

	void respawnNode(){
		Debug.Log("The Node Respawned");
		gameObject.GetComponent<MeshRenderer> ().enabled = true;
		gameObject.GetComponent<BoxCollider> ().enabled = true;
	}
	void despawnNode(float respawnOffset){
		Debug.Log("The Node Despawned");
		gameObject.GetComponent<MeshRenderer> ().enabled = false;
		gameObject.GetComponent<BoxCollider> ().enabled = false;
		respawnTimer.ResetTimer (respawnOffset);
		canSpawn = false;
	}
}
