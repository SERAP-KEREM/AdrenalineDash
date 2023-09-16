using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator animator;
    public float IdleTime;
    public BoxCollider boxCollider;

    public void AnimasyonEvent(string animEvent)
    {
        if(animEvent=="true")
        {

            animator.SetBool("Play", true);
            boxCollider.enabled = true;
        }

        else
        {
            animator.SetBool("Play", false);
            StartCoroutine(AnimPlay());
            boxCollider.enabled = false;
        }
    }
    IEnumerator AnimPlay()
    {
        yield return new WaitForSeconds(IdleTime);
        AnimasyonEvent("true");
    }
}
