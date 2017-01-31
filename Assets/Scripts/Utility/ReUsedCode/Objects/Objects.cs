using UnityEngine;
using System.Collections;
[System.Serializable]
public class Objects : MonoBehaviour
{
    //made these public so that the inventorySystem can use them
    public BasicItem objectVal = new BasicItem();
    public State objectState = new State();
    private Spawner spawn = new Spawner();

    public void DestroyGameObject(GameObject p_GameObjectInstance)
    {
        Destroy(p_GameObjectInstance);
    }

    public void InstantiateAtLocation(GameObject p_GameObjectInstance, Transform p_SpawnPoint)
    {
        spawn.SpawnObjectAtSpot(p_SpawnPoint, p_GameObjectInstance);
    }
}

//PRototype START
//public PrototypeEngine prototyper;

//void Awake(){
//	prototyper = GameObject.Find("GameMaster").GetComponent<PrototypeEngine>();
//}


//void OnTriggerEnter(Collider other){
//	if (other.tag == "Player") {
//		prototyper.DoNextThing(prototyper.startObjectiveText1,prototyper.starOldManText2);
//	}

//}

//Prototype END