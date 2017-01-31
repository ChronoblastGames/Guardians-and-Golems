using UnityEngine;
using System.Collections;

public class HealthNodeSpawner : MonoBehaviour {
	public Spawner healthNodeSpawner;
	public Transform[] spawnLocations;
	public Transform objectSpawnSpace;
	public GameObject objectToSpawn;

	// Use this for initialization
	void Start () {
		healthNodeSpawner = new Spawner ();
		healthNodeSpawner.SpawnAtStart (spawnLocations,objectToSpawn);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
