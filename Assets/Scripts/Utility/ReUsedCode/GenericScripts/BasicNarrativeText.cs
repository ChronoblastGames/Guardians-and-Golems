using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class BasicNarrativeText{
	//Dictionary key and values 
	public string keyName; 
	public string keyValue;
	//List of tags associated with this system
	public List<BasicTag> associatedTags;
	[System.NonSerialized]
	public float timeMarker;
	[System.NonSerialized]
	public float timeOfOnset;
	//Weight of this narrative compontent that can be used to pick text
	public float narrativeWeight;

	//narrative layers are used to be able to differentiate the different types of texts we want to store, we will have a "database" for the overall compilation.
	public int narrativeLayer;
	// layer 0 = flavour
	// layer 1 = Environmental Barks
	// layer 2 = Generic Hello
	// layer 3 = Generic Goodbye
	// layer 4 = Generic signs
	// layer 5 = Win Messages
	// layer 6 = Lose Messages
	// layer 7 = System Alerts
	// layer 8 = Menu texts
	// layer 9 = DO NOT GO HERE
	// layer 10-100 objective-specific step layers


	public BasicNarrativeText(){
//		timeOfOnset = MarkTime();
	}
	public void SetNarrativeValue(string valueToSet){
		keyValue = valueToSet;
	}
	public void SetKeyName(string keyToSet){
		keyName = keyToSet;
	}
	public void SetNarrativeWeight(float weightToAssign){
		narrativeWeight = weightToAssign;
	}
	public float GetNarrativeWeight(){
		return narrativeWeight;
	}
	public void AddTagToList(BasicTag tagToSet){
		associatedTags.Add (tagToSet);
	}
	public List<BasicTag> GetAssociatedTags(){
		return associatedTags;
	}
	public bool CheckIfTagIsInList (BasicTag tagToCheck){
		return associatedTags.Contains (tagToCheck);
	}
	public int GetNarrativeLayer(){
		return narrativeLayer;
	}
	public void SetNarrativeLayer(int layerToSet){
		narrativeLayer = layerToSet;
	}
	public float MarkTime(){
		
		timeMarker = Time.time;
		return timeMarker;
	}

}
