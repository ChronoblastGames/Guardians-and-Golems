using UnityEngine;
using System.Collections;

public class ObjectsInteractions
{
    public GameObject objectInstance;
    public Transform objectPosition;
    public bool isHighlighted;

    public Color orginalColor;
    public Renderer interactableObjRender;
    public GameObject interactableObj;


    public BasicItem gameItem = new BasicItem();
    public Spawner spawnobject = new Spawner();

    public void HighlightOnHover(GameObject p_thisGameObject, Renderer p_interactableObjRender, Color p_objOriginalColor, bool p_higlighted)
    {
       orginalColor = p_objOriginalColor;
       interactableObjRender = p_interactableObjRender;
       interactableObj = p_thisGameObject;

       interactableObjRender = interactableObj.GetComponent<Renderer>();
       orginalColor = interactableObjRender.material.color;

        if (p_higlighted == true)
        {
            interactableObjRender.material.color = orginalColor + new Color32(200, 200, 200, 1);
            //interactableObjRender.material.color = Color.blue;
        }

        if (p_higlighted == false)
        {
            interactableObjRender.material.color = orginalColor - new Color32(200, 200, 200, 1);
            //interactableObjRender.material.color = Color.blue;
            Debug.Log("asdf");
        }
    }


    public void AddObjectToInventory(string p_name , int p_value, float p_weight)
    {
        gameItem.itemName = p_name;
        gameItem.identityNumber = p_value;
        gameItem.itemWeight = p_weight;

        // Place object into your inventory
    }

    public void HoldObject(GameObject p_instance, Transform p_position)
    {
        // Instantiate the object in this position
        objectInstance = p_instance;
        objectPosition = p_position;
        spawnobject.SpawnObjectAtSpot(objectPosition, objectInstance);
    }
    /*
    public void LeaveObject(GameObject p_droppedInstance, Transform p_doppedPosition)
    {
        p_droppedInstance = GameObject.Instantiate(p_droppedInstance, p_doppedPosition) as GameObject;
        p_droppedInstance.transform.position = p_droppedInstance.transform.position;

        gameItem.isDropped = true;
    }
    */
}
