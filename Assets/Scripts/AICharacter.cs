using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class AICharacter : MonoBehaviour
{
    public GameObject Target;
    NavMeshAgent navMeshAgent;
    public GameManager gameManager;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    Vector3 PositionGive()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);

    }
    private void LateUpdate()
    {
        navMeshAgent.SetDestination(Target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CubeObstacle"))
        {
          gameManager.DestroyEffectMakes(PositionGive());
            gameObject.SetActive(false);
        }
        if(other.CompareTag("Testere"))
        {
            gameManager.DestroyEffectMakes(PositionGive());
            gameObject.SetActive(false);
        } 
        if(other.CompareTag("PervaneDisli"))
        {
           gameManager.DestroyEffectMakes(PositionGive());
            gameObject.SetActive(false);
        } 
        if(other.CompareTag("Balyoz"))
        {
           gameManager.DestroyEffectMakes(PositionGive(), true);
            gameObject.SetActive(false);
        } 
        if(other.CompareTag("Enemy"))
        {
           gameManager.DestroyEffectMakes(PositionGive(),false,true);//false
            gameObject.SetActive(false);
        }
        if (other.CompareTag("FreeCharacter"))
        {
            gameManager.Characters.Add(other.gameObject);
         
        }
    }
}
