using System.Collections;
using UnityEngine;

public class FloatingTextController : MonoBehaviour 
{
    private TimerClass textTimer;
    private FloatingTextManager textPool;

    private Animator textAnimator;
    public AnimationClip textAnim;

    private bool isActive = false;

    private void Start()
    {
        textTimer = new TimerClass();

        textPool = GameObject.FindGameObjectWithTag("UIManager").GetComponent<FloatingTextManager>();

        textAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);

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
        isActive = false;

        gameObject.SetActive(false);

        textPool.ReturnToPool(gameObject);      
    }
}
