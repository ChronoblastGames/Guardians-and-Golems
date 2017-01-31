using UnityEngine;
using System.Collections;
[System.Serializable]
public class BasicItem{
	
	public BasicState currentState;
	public int identityNumber = -1;
	public string itemName;
	public float itemWeight;
	public bool isHeld;
	public bool isDropped;
	public Sprite itemIcon;
	public int objectiveLayer;
	//public 

	public BasicItem(){
		
	}
		
}
