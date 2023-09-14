using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameManager gameManager;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward*0.5f*Time.deltaTime);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(Input.GetAxis("Mouse X")<0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), .3f);
            }
            if(Input.GetAxis("Mouse X")>0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z), .3f);
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
    }

}
