using UnityEngine;
using System.Collections;
[System.Serializable]
public class BasicAttribute {

	public float baseAttributeMin = 0f; // this will be our attribute, set it to a value between 0 and 1;
	public float baseAttributeMax = 1f;
	public float baseAttributeCurrent;
	public float baseAttributeAdjustAmount; // how much you will adjust the attribute by;


	public BasicAttribute(){
		
	}

	public void AdjustAttribute(string command,float adjustAmount = 0){
		adjustAmount = Mathf.Clamp01 (adjustAmount);
		switch (command) {
		case "AdjustToNumber":
			baseAttributeCurrent = adjustAmount;
			break;
		case "AddAmount":
			baseAttributeCurrent = Mathf.Clamp01 (baseAttributeCurrent += adjustAmount);
			break;
		case "SubtractAmount":
			baseAttributeCurrent = Mathf.Clamp01 (baseAttributeCurrent -= adjustAmount);
			break;
		case "MultiplyByAmount":
			baseAttributeCurrent = Mathf.Clamp01 (baseAttributeCurrent * adjustAmount);
			break;
		case "DivideAmount":
			baseAttributeCurrent = Mathf.Clamp01 (baseAttributeCurrent / adjustAmount);
			break;
		case "SetToMax":
			baseAttributeCurrent = baseAttributeMax;
			break;
		case "SetToMin":
			baseAttributeCurrent = baseAttributeMin;
			break;
		default:
			break;

		}
		
	}

	//Check Value Functions
	public float GetRatio(){
		float ratio = Mathf.Clamp01 (baseAttributeCurrent / baseAttributeMax);
		return ratio;
	}

	//Shows value of the health as a percentage of 100
	public float GetPercentage(){
		float percentage = GetRatio () * 100f;
		return percentage;
	}
	//Shows value of the health as a relative value of X, this means that if you have 0.75 baseAttributeCurrent and set the float to 500 (meaing 500 is the max health), you will have 375 hp
	public float GetRatioAsRange(float maxValue){
		float ratioAsRange = GetRatio() * maxValue;
		return ratioAsRange;
	}
}
