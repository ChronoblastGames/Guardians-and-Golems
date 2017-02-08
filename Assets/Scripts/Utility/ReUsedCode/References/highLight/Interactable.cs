using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    private bool isHighlighted;
    private Renderer rend;
    private Color startingColor;


    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();    //Access Renderer
        startingColor = rend.material.color; //Gets its current Color
    }

    // Update is called once per frame
    void Update()
    {

        HandleColors(); 

        isHighlighted = false;  //Sets highlighted to false each frame

    }

    public void Highlight()
    {

        isHighlighted = true;   //When hovered over; highlight selected object.

    }

    private void HandleColors()
    {

        if (isHighlighted)
        {

            rend.material.color = startingColor + new Color32(200, 200, 200, 1); //Highlight

        }
        else
        {

            rend.material.color = startingColor; //Put back to normal;

        }

    }
}
