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
            Vector3 newPos = new Vector3(transform.position.x, 0.23f,transform.position.z);
           GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectMakes(newPos);
            gameObject.SetActive(false);
        }
        if(other.CompareTag("Testere"))
        {
            Vector3 newPos = new Vector3(transform.position.x, 0.23f,transform.position.z);
           GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectMakes(newPos);
            gameObject.SetActive(false);
        } 
        if(other.CompareTag("PervaneDisli"))
        {
            Vector3 newPos = new Vector3(transform.position.x, 0.23f,transform.position.z);
           GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectMakes(newPos);
            gameObject.SetActive(false);
        } 
        if(other.CompareTag("Balyoz"))
        {
            Vector3 newPos = new Vector3(transform.position.x, 0.23f,transform.position.z);
           GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestroyEffectMakes(newPos,true);
            gameObject.SetActive(false);
        }
    }
}
