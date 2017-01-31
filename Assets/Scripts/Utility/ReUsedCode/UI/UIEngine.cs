using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIEngine : MonoBehaviour {

    // public NarrativeUI enterRoomUI;
	//Redundant public State ui_EngineState;
    public BasicStateEngine uiStateEngine = new BasicStateEngine();
    public string textToShow;
	public Text uiTextBoxToUse;
	public bool isNarrativeUIShowing;
	public bool isMenuShowing;

	//Show and hide narrative. Set narrative in Display with textToShow
	public void DisableNarrativeText() //Text narrativeBox
	{
		uiTextBoxToUse.gameObject.SetActive(false);
	}
	public void DisplayNarrativeText(string textToShow) //Text narrativeBox
	{
		uiTextBoxToUse.gameObject.SetActive(true);
		uiTextBoxToUse.text = textToShow;
	}
	public void DisplayMenu(){
		
	}
	public void HideMenu(){
		
	}
	public void SelectMenuItem(){
		
	}
	public void ShowCursor(){

	}
    public void HideCursor() {
    }
	public void LockCursor(){
	}
}
