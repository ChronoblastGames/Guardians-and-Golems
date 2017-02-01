using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCooldown{
	public float cdTime;
	public BasicTimer cdTimer;
	public BasicStateEngine cdStateEngine;
	public BasicState[] possibleStates;


	public BasicCooldown(){
		InitialSetup ();

	}
	public void SetUpStateArray(BasicState[] stateArrayToFill){
	//Manual config
		BasicState NullState = new BasicState(0, "NullState");
	//Assign to PossibleState
		stateArrayToFill [0] = NullState;
		BasicState OnCDState = new BasicState(1,"OnCDState");
		stateArrayToFill [1] = OnCDState;
		BasicState Ready = new BasicState(2, "ReadyState");
		stateArrayToFill [2] = Ready;
		BasicState Disabled = new BasicState(3, "DisabledState");
		stateArrayToFill [3] = Disabled;
	}
	public void InitialSetup(){
		cdStateEngine = new BasicStateEngine ();
		possibleStates = new BasicState[4];
		SetUpStateArray (possibleStates);
		cdTimer = new BasicTimer (0);
		Debug.Log (possibleStates[2].stateName);
		Debug.Log(cdStateEngine.currentState);
		cdStateEngine.SetState(possibleStates[2]);
	}
	//lolrofldoofus fix to call coroutines that can stop remotely
	public void ResetCD(){

		//("RestartCoolDownCoroutine");
	}
	public IEnumerator RestartCoolDownCoroutine()
	{
//		cdTimer.ResetTimer (cdTime);
		cdStateEngine.SetState (possibleStates [1]);
		Debug.Log(cdTime + " is the time in seconds to wait: Waiting for TimerIsDoneToBeTrue... the time is " +Time.time );
		yield return new WaitForSeconds (cdTime);
//		yield return new WaitUntil(() => cdTimer.TimerIsDone());
		Debug.Log("TimerIsDone state set to ready " + Time.time);
		cdStateEngine.SetState (possibleStates [2]);
	}



}
