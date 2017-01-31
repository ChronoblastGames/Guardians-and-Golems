using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    private Objects item;
    private string data;
    private GameObject tooltip;

    void Start()
    {
//        tooltip = GameObject.Find("Tooltip");
//        tooltip.SetActive(false);
    }

    void Update()
    {
//        if(tooltip.activeSelf)
//        {
//            tooltip.transform.position = Input.mousePosition;
//        }
    }

	public void Activate(Objects item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {

		//PutInterestingSTuffhere
        data = "<color=#000000><b>" + item.objectVal.itemName + "</color>";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
