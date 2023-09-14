using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    GameObject Target;
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestinationPoint;
    }

    private void LateUpdate()
    {
        navMeshAgent.SetDestination(Target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CubeObstacle"))
        {
           GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectMakes(transform);
            gameObject.SetActive(false);
        }
    }
}
