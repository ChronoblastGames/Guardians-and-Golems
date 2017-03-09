using System.Collections;
using UnityEngine;

public class FloatingTextController : MonoBehaviour 
{
    private FloatingTextManager textPool;
    private TimerClass textTimer;

    private Animator textAnimator;
    public AnimationClip textAnim;

    private bool isActive = false;

    private void Start()
    {
        textTimer = new TimerClass();

        textPool = transform.parent.GetComponent<FloatingTextManager>();

        textAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Initialize()
    {
        textTimer.ResetTimer(textAnim.length);

        textAnimator.SetTrigger("isActive");

        isActive = true;
    }

    private void Update()
    {
        ManageTimer();
    }

    void ManageTimer()
    {
        if (isActive)
        {
            if (textTimer.TimerIsDone())
            {
                SendBackToPool();
            }
        }
    }

    void SendBackToPool()
    {
        textPool.ReturnToPool(gameObject);

        isActive = false;

        gameObject.SetActive(false);
    }
}
