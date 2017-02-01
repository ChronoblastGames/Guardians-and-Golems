using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class BasicStateEngine {

	public BasicState currentState;
	public BasicState nextState;
	public List <BasicState> stateFullList = new List<BasicState>() ;
	public List <BasicState> stateHistory = new List<BasicState>() ;
	public int stateIterationCount = 0;

	public BasicStateEngine(){
		
	}

	public BasicState GetState(){
		return currentState; 
	}

	public void SetState(BasicState p_nextState = null){
		//Debug.Log ("Setstate started");
		if (nextState == null && p_nextState == null) {
			//Debug.Log ("SetState cannot complete the function as Next State and the argument parameter are null");
			return;
		}
		if (p_nextState == null) {
			p_nextState = nextState;
		}
		//Debug.Log("About to assign " + p_nextState.stateName + " as the current state");
		currentState = p_nextState;
		AddCurrentStateToHistory ();
	}
	public bool CheckState(BasicState p_stateToCheck, BasicState p_checkAgainstState){
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
		currentState.MarkTime ();
		//Debug.Log (currentState.stateName + " is being added to history");
		stateHistory.Add (currentState);
		//Until I figure out how to log the time wiht a data pair a debug log
		//Debug.Log("You have just stored " + currentState.stateName + " at index value" + stateIterationCount + " the current time is " + Time.time);
		stateIterationCount++;

	}
	public void AddStateToList(BasicState stateToAdd){
		if (stateFullList.Contains(stateToAdd)){
			//Debug.Log ("There is already an item with the stateID " + stateToAdd + " as a key");
            return;
		}else{
            stateFullList.Add (stateToAdd);
	    }
	}
		
}
