using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour 
{
    public List<GameObject> damageTextPool = new List<GameObject>();

    public List<GameObject> statusTextPool = new List<GameObject>();

    public GameObject damageTextPrefab;
    public GameObject statusTextPrefab;

    public void CreateDamageText(float damageValue, Transform textLocation, FloatingDamageSubTextType effectType)
    {
        Color textColor = Color.black;
        GameObject newText = GetNextDamageText();
        string damageText = damageValue.ToString();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(textLocation.position);

        newText.transform.position = screenPos;
        newText.transform.GetChild(0).GetComponent<Text>().text = damageText;

        switch(effectType)
        {
            case FloatingDamageSubTextType.DAMAGE:
                textColor = Color.red;
                break;

            case FloatingDamageSubTextType.HEAL:
                textColor = Color.green;
                break;

            case FloatingDamageSubTextType.CRITICAL:
                textColor = Color.yellow;
                break;

            case FloatingDamageSubTextType.SHIELD:
                textColor = Color.blue;
                break;

            default:
                Debug.Log("Something Incorrect Passed through CreateDamageText on " + this);
                break;
        }

        newText.transform.GetChild(0).GetComponent<Text>().color = textColor;
        newText.transform.SetParent(transform, false);

        newText.GetComponent<FloatingTextController>().Initialize();
    }

    public void CreateStatusText(float effectStrength, Transform textLocation, StatusEffect statusEffect)
    {
        Color textColor = Color.black;
        GameObject newText = GetNextStatusText();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(textLocation.position);

        newText.transform.position = screenPos;

        switch (statusEffect)
        {
            case StatusEffect.BLEED:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Bleeding";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                break;

            case StatusEffect.HEALOVERTIME:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Heal Over Time";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.green;
                break;

            case StatusEffect.KNOCKBACK:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Knocked Back";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
                break;

            case StatusEffect.MANA_DRAIN:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Mana Drained";
                break;

            case StatusEffect.SHIELD:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Shielded";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.blue;
                break;

            case StatusEffect.SILENCE:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Silenced";
                break;

            case StatusEffect.SLOW:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Slowed";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
                break;

            case StatusEffect.STUN:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Stunned";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
                break;

            case StatusEffect.STAGGER:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Stagger";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
                break;

            case StatusEffect.PULL:
                newText.transform.GetChild(0).GetComponent<Text>().text = "Pulled";
                newText.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
                break;
        }

        newText.transform.SetParent(transform, false);

        newText.GetComponent<FloatingTextController>().Initialize();
    }

    private GameObject GetNextDamageText()
    {
        if (damageTextPool.Count > 0)
        {
            GameObject newText = damageTextPool[0];
            damageTextPool.Remove(newText);
            return newText;
        }
        else
        {
            GameObject newText = Instantiate(damageTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            newText.name = "DamageText" + damageTextPool.Count;

            return newText;
        }
    }

    private GameObject GetNextStatusText()
    {
        if (statusTextPool.Count > 0)
        {
            GameObject newText = statusTextPool[0];
            statusTextPool.Remove(newText);
            return newText;
        }
        else
        {
            GameObject newText = Instantiate(statusTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            newText.name = "StatusText" + statusTextPool.Count;

            return newText;
        }
    }

    public void ReturnToDamagePool(GameObject objectToReturn, FloatingTextType textType)
    {
        switch (textType)
        {
            case FloatingTextType.DAMAGE:
                damageTextPool.Add(objectToReturn);
                break;

            case FloatingTextType.STATUS:
                statusTextPool.Add(objectToReturn);
                break;
        }
    }
}
