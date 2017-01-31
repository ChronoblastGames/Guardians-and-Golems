using UnityEngine;
using System.Collections;

public class Memories : MonoBehaviour
{
    // Add this script to the Memories Manager in the scene

    public GameObject[] memoriesThatDerivesFrom;                            // Key items that trigger the memories
    public GameObject[] memoryInstance;                                     // The scene that appears when you trigger the memories
    public Transform [] memoryScene;
    public bool[] playMemory;

    private Spawner memory = new Spawner();

    public void GetMemories()
    {
        // if the player interacts with a story related object that presents a memory, redirect to this function.
        // set playmemory[x] to true -> play memories 
    }

    public void PlayMemories()
    {
        if(playMemory[1] == true)
        {
            memory.SpawnObjectAtSpot(memoryScene[1], memoryInstance[1]);
        }
        //Added more when needed
    }
}