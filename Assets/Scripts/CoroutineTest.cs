using System.Collections;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    public int count;
    public int maxCount;
    public int addHowMuch = 5;

    void Start()
    {
        StartCoroutine("TestRoutine");
    }

    void Add()
    {
        count += addHowMuch;
        Debug.Log(count);
    }

    IEnumerator TestRoutine()
    {
        Add();
        yield return new WaitForSeconds(0.5f);
    }
}
