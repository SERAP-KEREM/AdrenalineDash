using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public GameManager gameManager;
    public CameraController camera;
    public bool isFinal;
    public float moveSpeed = 2f;
    public GameObject FinalTarget;

    public Slider slider;
    public GameObject TransitPoint;
    private void FixedUpdate()
    {
        if(!isFinal)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }


    private void Start()
    {
            float fark = Vector3.Distance(transform.position, TransitPoint.transform.position);
        slider.maxValue = fark;
    }
    private void Update()
    {


        if(isFinal)
        {
            transform.position = Vector3.Lerp(transform.position, FinalTarget.transform.position, 0.001f);
            if(slider.value != 0f)
            {
                slider.value -= .01f;

            }
        }
        else
        {
            float fark = Vector3.Distance(transform.position, TransitPoint.transform.position);
            slider.value = fark;    

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
          if(other.CompareTag("FinalTrigger"))
        {
            camera.GetComponent<CameraController>().isFinal=true;
            gameManager.EnemiesAttack();
            isFinal = true;
        } 
        if(other.CompareTag("FreeCharacter"))
        {
            gameManager.Characters.Add(other.gameObject);
           
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pole")|| collision.gameObject.CompareTag("CubeObstacle")||collision.gameObject.CompareTag("PervaneDisli")|| collision.gameObject.CompareTag("Balyoz"))
        {
            if (transform.position.x > 0f)
            {

                Debug.Log("1");
                transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z);
            }
            else
            {

                Debug.Log("2");

                transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
            }

        }
    }
}
