using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public int id;
    private InventorySystem inv;
    public Objects resetterObject;

    void Start()
    {
		Debug.Log (" Inventory slot initialized as a child of " + gameObject);
        inv = GameObject.Find("Inventory Master").GetComponent<InventorySystem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (inv.currentInventory[id].objectVal.identityNumber == -1)
        {
            inv.currentInventory[droppedItem.slotLocation] = resetterObject;
            inv.currentInventory[id] = droppedItem.item;
            droppedItem.slotLocation = id;
        }
        else if(droppedItem.slotLocation != id)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slotLocation = droppedItem.slotLocation;
            item.transform.SetParent(inv.slots[droppedItem.slotLocation].transform);
            item.transform.position = inv.slots[droppedItem.slotLocation].transform.position;

            droppedItem.slotLocation = id;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.currentInventory[droppedItem.slotLocation] = item.GetComponent<ItemData>().item;
            inv.currentInventory[id] = droppedItem.item;
        }
    }



}
