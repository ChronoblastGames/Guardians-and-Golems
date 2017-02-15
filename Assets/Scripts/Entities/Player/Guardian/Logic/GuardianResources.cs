using System.Collections;
using UnityEngine;

public class GuardianResources : MonoBehaviour
{
    private GuardianPlayerController guardianPlayerController;

    [Header("Player Mana Attributes")]
    public float currentMana;
    public float maxMana;

    [Header("Player Mana Regeneration Attributes")]
    public float manaRegenerationSpeed;

    void Awake()
    {
        InitializeValues();
    }

    private void FixedUpdate()
    {      
        RegenerateMana();
    }

    void InitializeValues()
    {
        guardianPlayerController = GetComponent<GuardianPlayerController>();

        currentMana = maxMana;
    }

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenerationSpeed * Time.deltaTime;

            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            else if (currentMana < 0)
            {
                currentMana = 0;
            }
        }
    }

    public bool CanCast(float spellManaCost)
    {
        if (currentMana > spellManaCost)
        {
            currentMana -= spellManaCost;
            return true;
        }
        else
        {
            Debug.Log(spellManaCost);
            return false;
        }
    }
}
