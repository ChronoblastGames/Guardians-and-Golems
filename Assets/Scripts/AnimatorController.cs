using System.Collections;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator stateAnimator;

    void Start()
    {
        stateAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stateAnimator.SetBool("punchKeyDown", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKeyDown(KeyCode.Alpha1))   
        {
            stateAnimator.SetBool("canCombo", true);
        }
    }
}
