using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCooldown{
	public float cdTime;
	public BasicTimer cdTimer;
	public BasicStateEngine cdStateEngine;
	public State[] possibleStates;


//	public BasicCooldown(){
//		InitialSetup ();
//
//	}
	public void SetUpStateArray(State[] stateArrayToFill){
	//Manual config
		State NullState = new State();
	//Assign to PossibleState
		stateArrayToFill [0] = NullState;
		State OnCDState = new State();
		stateArrayToFill [1] = OnCDState;
		State Ready = new State();
		stateArrayToFill [2] = Ready;
		State Disabled = new State();
		stateArrayToFill [3] = Disabled;
	}
	public void InitialSetup(){
		SetUpStateArray (possibleStates);
		cdTimer = new BasicTimer (0);
		cdStateEngine.SetState(possibleStates [2]);
	}
	//lolrofldoofus fix to call coroutines that can stop remotely
	public void ResetCD(){

		//("RestartCoolDownCoroutine");
	}
	public IEnumerator RestartCoolDownCoroutine()
	{
		cdTimer.ResetTimer (cdTime);
		cdStateEngine.SetState (possibleStates [1]);
		Debug.Log("Waiting for TimerIsDoneToBeTrue...");
		yield return new WaitUntil(() => cdTimer.TimerIsDone());
		Debug.Log("TimerIsDone, Resetting state to ready");
		cdStateEngine.SetState (possibleStates [2]);
	}



}
