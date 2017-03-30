using System.Collections;
using UnityEngine;

public class PlayAnimationAfterSeconds : MonoBehaviour
{
    public Animator targetAnimator;

    public float waitTime;

    void Start()
    {

    }

    private IEnumerator PlayAnimation()
    {
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);

            targetAnimator.SetTrigger("Trigger");
        }
    }
}
