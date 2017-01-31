using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[System.Serializable]
public class BasicObjective{

	public int identityNumber;
	public string objectiveName;
	public string objectiveMessage;
	public float timeMarker;
	public float timeOfOnset;
	public int objectiveLayer;
    // use numbers 10-100 for objective steps to match them with corresponding narrative text sets.


        //This is for editor Use,
    public BasicObjective() {

    }

       //This is for script use (later)
	//public BasicObjective(int p_identityNumber , string p_objectiveName, int p_objectiveLayer =1000, float timeStart = 0){
	//	identityNumber = p_identityNumber;
	//	objectiveName = p_objectiveName;
	//	timeOfOnset = MarkTime ();
 //       objectiveLayer = p_objectiveLayer;

 //   }
	public float MarkTime(){
			timeMarker = Time.time;
			return timeMarker;
	}
	public void SetObjectiveMessage(string objectiveMessageToSet){
		objectiveMessage = objectiveMessageToSet;
	}
	public string GetObjectiveMessage(){
		return objectiveMessage;
	}
	public bool CheckObjectiveMessage(string objectiveMessageToCheck){
		if (objectiveMessageToCheck == objectiveMessage) {
			return true;
		} else {
			return false;
		}
	}
    public bool CheckObjectiveStatus(bool thingToTest) {
        if (thingToTest == true)
        {
            return true;
        }
        else {
            return false;
        }

  
    }

}
