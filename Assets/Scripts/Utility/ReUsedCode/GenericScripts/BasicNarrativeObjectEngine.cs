using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class BasicNarrativeObjectEngine{

	public NarrativeObject currentNarrativeText;
	public NarrativeObject nextNarrativeText;
	private NarrativeObject nullObject;
//	public NarrativeObject firstText;
//	public NarrativeObject secondText;
//	public NarrativeObject thirdText;
	[SerializeField]
	public List <NarrativeObject> narrativeFullList;
	[SerializeField]
	public List <NarrativeObject> narrativeSelectionHistory;
	public int narrativeIterationCount = 0;
	private int narrativeLayerInUse;

	//constructor
	public BasicNarrativeObjectEngine(){
//		narrativeFullList  = new List<NarrativeObject>(){
//			
//			{firstText},{secondText},{thirdText}
//		};
		
	}
//	public void AddCurrentNarrativeTextToHistory(){
//		currentNarrativeText.MarkTime ();
//		narrativeSelectionHistory.Add (narrativeIterationCount, currentNarrativeText);
//	
//		narrativeIterationCount++;
//
//	}
//	public void AddNarrativeTextToList(string narrativeKeyName,BasicNarrativeText narrativeTextToAdd){
//		BasicNarrativeText temp = null;
//		if (narrativeFullList.TryGetValue(narrativeKeyName, out temp)){
//			Debug.Log ("There is already an item with the stateID " + narrativeKeyName + " as a key");
//		}else{
//		}
//		//Assign key to state;
//		narrativeTextToAdd.keyName = narrativeKeyName;
//		narrativeFullList.Add (narrativeKeyName, narrativeTextToAdd);
//	}
	public NarrativeObject GetHeaviestNarrativeKey(int narrativeLayer, List<NarrativeObject> listToCheck) {
		NarrativeObject winningText = null;
        float highestWeight = 0;
		foreach (NarrativeObject myText in listToCheck) {
            if (myText.narrativeObject.narrativeLayer == narrativeLayer) {
				if (myText.narrativeObject.narrativeWeight < highestWeight)
                {
                    winningText = myText;
					highestWeight = myText.narrativeObject.narrativeWeight;
             
                }

            }
        }
        return winningText;
    }
	public NarrativeObject GetNarrativeKeyByKeyName(string keyName){
		foreach (NarrativeObject myText in narrativeFullList) {
			Debug.Log("I am forEaching in the GetNarrativeKeybyname function");
			if (myText.narrativeObject.keyName == keyName) {
				return myText;
			} else {
				
			}
		}
		return nullObject;
	}

	public void ListAllNarrativeKeys(){
		foreach (NarrativeObject myText in narrativeFullList) {
			Debug.Log (myText.narrativeObject.keyName);
		}
		
	}

}
