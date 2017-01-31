using UnityEngine;
using System.Collections;
[System.Serializable]
//you are declaring a class, not a monobehaviour, this class can be used in any function without reference
public class BasicTimer {

	//The initial time gets the time of the game at the moment of the consctructor being called;
	float initialTime;
	//The delay time, is the time in seconds that you want to push the top of the timer forward.
	float delayTime;


	//This calls the ResetTimer method declared below to set a new delay.
	public BasicTimer(float p_delayTime = 0){
		ResetTimer (p_delayTime);
	}

	//Ratios are useful computations, here we resolve time to a ratio. Actual time is the time at moment of GetRatio();
	//initial time is the time at the start of the consctructor.
	//It calculates time elapsed to a value between 0 and 1, 0 is start 1 is complete
	//This awesome;
	public float GetRatio(){
		
		float actualTime = Time.time;
		if (actualTime == initialTime)
			return 0;
		
		float ratio = Mathf.Clamp ((actualTime - initialTime) / delayTime, 0f, 1f);
		return ratio;
	}

	public bool TimerIsDone(){
		return (GetRatio () == 1);
	}

	public void ResetTimer(float p_delayTime){
		delayTime = p_delayTime;
		initialTime = Time.time;
	}
}
