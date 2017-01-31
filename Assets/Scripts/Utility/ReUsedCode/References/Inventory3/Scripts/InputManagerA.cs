using UnityEngine;
using System.Collections;

public class InputManagerA: ScriptableObject
{
    public bool UFPS;
    public KeyCode reloadWeapon = KeyCode.R;
    public KeyCode throwGrenade = KeyCode.G;

    public KeyCode SplitItem;
    public KeyCode InventoryKeyCode;
    public KeyCode StorageKeyCode;
    public KeyCode CharacterSystemKeyCode;
    public KeyCode CraftSystemKeyCode;
/*    private GameObject gameObject;


    void update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }*/
  
}
