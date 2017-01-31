using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class BasicObjectiveEngine{

	public Objective currentObjective;
	public Objective nextObjective;
	public List <Objective> objectiveFullList;
	public List <Objective> objectiveHistory;
	public int objectiveIterationCount = 0;

	public BasicObjectiveEngine(){
	
	}
	public Objective GetCurrentObjective(){
		return currentObjective; 
	}

	public void SetObjective(Objective p_nextObjective = null){
		if (nextObjective == null && p_nextObjective == null) {
			Debug.Log ("SetState cannot complete the function as Next State and the argument parameter are null");
			return;
		}
		if (p_nextObjective == null) {
			p_nextObjective = nextObjective;
		}
		currentObjective = p_nextObjective;
		AddCurrentObjectiveToHistory ();

	}
	public bool CheckCurrentObjective(Objective p_objectiveToCheck, Objective p_checkAgainstObjective){
		p_objectiveToCheck = currentObjective;
		if (p_objectiveToCheck == p_checkAgainstObjective) {
			return true;
		} else {
			return false;
		}
	}
	public void SetObjectiveToNull(){
		currentObjective = null;
	}
	public void AddCurrentObjectiveToHistory(){
		currentObjective.thisObjective.MarkTime ();
		objectiveHistory.Add (currentObjective);
		//Until I figure out how to log the time wiht a data pair a debug log
		Debug.Log("You have just stored " + currentObjective + " at index value" + objectiveIterationCount + " the current time is " + Time.time);
		objectiveIterationCount++;

	}
	public void AddObjectiveToList(Objective objectiveToAdd){
		if (objectiveFullList.Contains(objectiveToAdd)){
			Debug.Log ("There is already an item with the stateID " + objectiveToAdd + " as a key");
            return;
		}else{
            objectiveFullList.Add(objectiveToAdd);
        }

		//Assign key to state;
		
	}

}
