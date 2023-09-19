using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject AttackTarget;
   public NavMeshAgent agent;
    public Animator animator;
    public GameManager gameManager;

    bool isAttackStart;
    public float moveSpeed = 2f;


    private void FixedUpdate()
    {
        if (isAttackStart)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
   

    public void AnimPlay()
    {

        animator.SetBool("Attack", true);
        isAttackStart = true;


    }

    private void LateUpdate()
    {
        if (isAttackStart)
        {
            agent.SetDestination(AttackTarget.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AICharacters"))
        {
            Vector3 newPos = new Vector3(transform.position.x, 0.23f, transform.position.z);
            gameManager.DestroyEffectMakes(newPos);
            gameObject.SetActive(false);
        }
    }
}
