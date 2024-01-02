using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAnimator : MonoBehaviour
{
    public Animator animator;

    public void PacifyYourself()
    {
        animator.SetBool("ok", false);
    }
}
