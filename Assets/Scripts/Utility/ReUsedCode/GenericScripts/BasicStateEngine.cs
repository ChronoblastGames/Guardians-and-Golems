using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class BasicStateEngine {

	public State currentState;
	public State nextState;
	public List <State> stateFullList;
	public List <State> stateHistory;
	public int stateIterationCount = 0;

	public BasicStateEngine(){
	}

	public State GetState(){
		return currentState; 
	}

	public void SetState(State p_nextState = null){
		if (nextState == null && p_nextState == null) {
			Debug.Log ("SetState cannot complete the function as Next State and the argument parameter are null");
			return;
		}
		if (p_nextState == null) {
			p_nextState = nextState;
		}
		currentState = p_nextState;
		AddCurrentStateToHistory ();
	}
	public bool CheckState(State p_stateToCheck, State p_checkAgainstState){
		p_stateToCheck = currentState;
		if (p_stateToCheck == p_checkAgainstState) {
			return true;
		} else {
			return false;
		}
	}
	public void SetStateToNull(){
		currentState = null;
	}
	public void AddCurrentStateToHistory(){
		currentState.thisState.MarkTime ();
		stateHistory.Add (currentState);
		//Until I figure out how to log the time wiht a data pair a debug log
		Debug.Log("You have just stored " + currentState + " at index value" + stateIterationCount + " the current time is " + Time.time);
		stateIterationCount++;

	}
	public void AddStateToList(State stateToAdd){
		if (stateFullList.Contains(stateToAdd)){
			Debug.Log ("There is already an item with the stateID " + stateToAdd + " as a key");
            return;
		}else{
            stateFullList.Add (stateToAdd);
	    }
	}
		
}
