using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject camera;
    public bool isFinal;
    public float moveSpeed = 2f;
    public GameObject FinalTarget;


    private void FixedUpdate()
    {
        if(!isFinal)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if(isFinal)
        {
            transform.position = Vector3.Lerp(transform.position, FinalTarget.transform.position, 0.001f);

        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), .3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z), .3f);
                }

            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
          if(other.CompareTag("Toplama")||other.CompareTag("Cikarma") ||other.CompareTag("Carpma") || other.CompareTag("Bolme"))
        {
           
            int sayi = int.Parse(other.name);
            gameManager.ManManager(other.tag,sayi,other.transform);
        }
          else if(other.CompareTag("FinalTrigger"))
        {
            Debug.Log("1");
            camera.GetComponent<CameraController>().isFinal=true;
            gameManager.EnemiesAttack();
            isFinal = true;
        }
    }

}
