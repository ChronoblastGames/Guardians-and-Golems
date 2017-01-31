using UnityEngine;
using System.Collections;
[System.Serializable]
public class WeightAssesment {

	//Here is a generic method. Notice the generic
	//type 'T'. This 'T' will be replaced at runtime
	//with an actual type. 


//	public T Generic<T> (T param1, T param2) where T :  typeof(float), int, double
//	{
//		if (param1 > param2) {
//			return param1;
//		} else {
//			return param2;
//		}
//	}
	public float GetGreatestWeightFloat(float firstValue, float secondValue){
		if (firstValue > secondValue) {
			return firstValue;
			}
		if (secondValue > firstValue) {
			return secondValue;
			}
		if (!(firstValue < secondValue) && !(secondValue < firstValue)) {
			Debug.Log (firstValue + " is equal in value to " + secondValue + " a random answer will be selected");
			int i = Random.Range (0, 1);
			if (i == 0) {
				return firstValue;
			} else {
				return secondValue;
			}
			
		} else {
				Debug.Log("Couldn't get the value");
				return 0f;
			}

	}

	public int GetGreatestWeightInt(int firstValue, int secondValue){
		if (firstValue > secondValue) {
			return firstValue;
		}
		if (secondValue > firstValue) {
			return secondValue;
		}
		if (!(firstValue < secondValue) && !(secondValue < firstValue)) {
			Debug.Log (firstValue + " is equal in value to " + secondValue + " a random answer will be selected");
			int i = Random.Range (0, 1);
			if (i == 0) {
				return firstValue;
			} else {
				return secondValue;
			}

		} else {
			Debug.Log("Couldn't get the value");
			return 0;
		}

	}

}
