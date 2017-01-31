using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

    public BasicObjective thisObjective;
    public bool testGoal = false; //what is the goal? interact with, get state, go somehwere? whatever it is, have it resolve into a bool
    //When is resolves 
    //public bool useDistance = false;
    //public bool useState = false;



    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void Goal() {
        thisObjective.CheckObjectiveStatus(testGoal);
    }
    //public bool ResolveTestGoal(float minimumDistance = 0) {  //BasicState stateToCheck
    //    bool myBool = false;
    //    if (useDistance == true) {

    //    }
    //    //if (stateToCheck)
    //    //{

    //    //}
    //    return myBool;
    //}
}
