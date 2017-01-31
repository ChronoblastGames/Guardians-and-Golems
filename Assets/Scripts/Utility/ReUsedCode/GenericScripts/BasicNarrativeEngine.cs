using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class BasicNarrativeEngine{


    //we moved from basicText to GameObject text for convience with editor and time constraints, this means that access times are slower, so long term solution will require rework, but short term it will be effective.
	public NarrativeObject currentNarrativeText;
	public NarrativeObject nextNarrativeText;
    private NarrativeObject nullObject; //used to pass an object where others dare not tread.
    [SerializeField]
	public List <NarrativeObject> narrativeFullList;
	[SerializeField]
	public List<NarrativeObject> narrativeSelectionHistory;
	public int narrativeIterationCount = 0;
	private int narrativeLayerInUse; // will be

	//constructor
	public BasicNarrativeEngine(){
	}

	public void AddCurrentNarrativeTextToHistory(){
		currentNarrativeText.narrativeObject.MarkTime ();
		narrativeSelectionHistory.Add (currentNarrativeText);
		narrativeIterationCount++;

	}
    public void AddNarrativeTextToList(NarrativeObject narrativeTextToAdd)
    {
    
            if (narrativeFullList.Contains(narrativeTextToAdd))
            {
                Debug.Log("There is already an item of that type " + narrativeTextToAdd + " as a key");
            }
            else
            //if it isn't in the list add it
            {
                Debug.Log("Adding " + narrativeTextToAdd + " to the registry");
                narrativeFullList.Add(narrativeTextToAdd);
            }
            //Assign key to state;
	}

    //This gets the most salient narrative element in the list, powerful tool can check on layers, or just strongest message overall.
    public NarrativeObject GetHeaviestNarrativeKey(int narrativeLayer = -1, List<NarrativeObject> listToCheck = null) {
		
        NarrativeObject winningText = null;
        float highestWeight = 0;
        if (listToCheck == null) {
            listToCheck = narrativeFullList;
        }
        if (narrativeLayer == -1)
        {
           // Debug.Log("Default state selected, polling all layers.");
            foreach (NarrativeObject myText in listToCheck)
            {
                if (myText.narrativeObject.narrativeWeight < highestWeight)
                {
                    winningText = myText;
                    highestWeight = myText.narrativeObject.narrativeWeight;
                   // Debug.Log("New best heaviest text: " + myText + " with a weight of " + highestWeight);
                }
            }
        }
        else
        {
			//Debug.Log (" I am starting GetHeaviestNArrative Else condition " + narrativeLayer);
            foreach (NarrativeObject myText in listToCheck)
            {
				//Debug.Log (" I am doing the loop WHEEE Key " + myText);
				//Debug.Log (myText.narrativeObject.narrativeLayer + " vs " + narrativeLayer);
                if (myText.narrativeObject.narrativeLayer == narrativeLayer)
				{ //Debug.Log (" I am looking at all of the heavy objects at layer " + narrativeLayer);
					//Debug.Log (myText.narrativeObject.narrativeWeight + " and " + highestWeight);
                    if (myText.narrativeObject.narrativeWeight > highestWeight)
                    {
                        winningText = myText;
                        highestWeight = myText.narrativeObject.narrativeWeight;
                        //Debug.Log("New best heaviest text: " + myText + " with a weight of " + highestWeight);
                    }

                }
            }
        }
        Debug.Log(winningText + " was selected");
        return winningText;
    }
    public NarrativeObject GetNarrativeKeyByKeyName(string keyName)
    {
        foreach (NarrativeObject myText in narrativeFullList)
        {
           // Debug.Log("I am forEaching in the GetNarrativeKeybyname function");
            if (myText.narrativeObject.keyName == keyName)
            {
                return myText;
            }
            else
            {

            }
        }
        return nullObject;
    }

    public void ListAllNarrativeKeys()
    {
        foreach (NarrativeObject myText in narrativeFullList)
        {
           // Debug.Log(myText.narrativeObject.keyName);
        }

    }
    public void SetNarrativeLayerInUse(int layerToSet) {
        narrativeLayerInUse = layerToSet;
    }
    public void SetcurrentNarrativeText(NarrativeObject currentText) {
        currentNarrativeText = currentText;
    }
    public void SetNextNarrativeText(NarrativeObject nextText)
    {
        nextNarrativeText = nextText;
    }

}
