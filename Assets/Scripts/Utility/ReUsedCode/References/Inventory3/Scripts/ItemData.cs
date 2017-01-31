using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler, IPointerExitHandler {

    public Objects item;
    public int amount;
    public int slotLocation;

    
    private InventorySystem inv;
    private Tooltip tooltip;
    private Vector2 offset;


    void Start ()
    {
        inv = GameObject.Find("Inventory Master").GetComponent<InventorySystem>();
        tooltip = inv.GetComponent<Tooltip>();
    }

     public void OnBeginDrag(PointerEventData eventData) // Dragging the item from the inventory
    {
        if (item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slotLocation].transform);
        this.transform.position = inv.slots[slotLocation].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }	

    public void OnPointerExit(PointerEventData eventData)
    {
       tooltip.Deactivate();
    }

    //afterwards under prefab of the item >>layout element [X] check - ignore layout
    // And item prefab Canvas Group >> [X] interactable [X] block raycast

    // https://www.youtube.com/watch?v=1gveNfidKPA  Time: 14:25
}
