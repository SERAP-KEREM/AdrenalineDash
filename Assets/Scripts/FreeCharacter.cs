using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class FreeCharacter : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public Material CharacterMaterial;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject Target;

    public GameManager gameManager;

    bool isContact;


    private void Start()
    {

    }
    private void LateUpdate()
    {
        if(isContact) 
        agent.SetDestination(Target.transform.position);
    }
    Vector3 PositionGive()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AICharacters")||other.CompareTag("Player"))
        {
            if(gameObject.CompareTag("FreeCharacter"))
            {

                isContact = true;
                MatChangeAndAnimPlay();
                GetComponent<AudioSource>().Play();

            }
        }
        if (other.CompareTag("Enemy"))
        {
          
            gameManager.DestroyEffectMakes(PositionGive(), false, true);//false
            gameObject.SetActive(false);
        }

        if (other.CompareTag("CubeObstacle"))
        {
            gameManager.DestroyEffectMakes(PositionGive());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Testere"))
        {
            gameManager.DestroyEffectMakes(PositionGive());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("PervaneDisli"))
        {
            gameManager.DestroyEffectMakes(PositionGive());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Balyoz"))
        {
            gameManager.DestroyEffectMakes(PositionGive(), true);
            gameObject.SetActive(false);
        }
    }
    void MatChangeAndAnimPlay()
    {

        Material[] mats = meshRenderer.materials;
        mats[0] = CharacterMaterial;
        meshRenderer.materials = mats;

        animator.SetBool("Attack", true);
        gameObject.tag = "AICharacters";
        GameManager.ActiveCharacterCount++;
       // Debug.Log(GameManager.ActiveCharacterCount);
    }
}
