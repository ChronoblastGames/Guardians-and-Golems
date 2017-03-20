using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour 
{
    public List<GameObject> damageTextPool = new List<GameObject>();

    public GameObject textPrefab;

    public void CreateDamageText(float damageValue, Transform textLocation)
    {
        GameObject newText = GetNextObject();
        string damageText = damageValue.ToString();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(textLocation.position);

        newText.transform.position = screenPos;
        newText.transform.GetChild(0).GetComponent<Text>().text = damageText;
        newText.transform.SetParent(transform, false);

        newText.GetComponent<FloatingTextController>().Initialize();
    }

    private GameObject GetNextObject()
    {
        if (damageTextPool.Count > 0)
        {
            GameObject newText = damageTextPool[0];
            damageTextPool.Remove(newText);
            return newText;
        }
        else
        {
            GameObject newText = Instantiate(textPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            newText.name = "DamageText" + damageTextPool.Count;

            return newText;
        }
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        damageTextPool.Add(objectToReturn);
    }
}
