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
    private Vector3 lastMousePosition;
    public float sensitivity = 0.01f; // Fare hareket hassasiyeti

    private void FixedUpdate()
    {
        if (!isFinal)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void Start()
    {
        float fark = Vector3.Distance(transform.position, TransitPoint.transform.position);
        slider.maxValue = fark;
    }

    private void Update()
    {
        if (isFinal)
        {
            // Final hedefine do?ru yava?ça ilerleme
            transform.position = Vector3.Lerp(transform.position, FinalTarget.transform.position, 0.001f);

            // Slider de?eri s?f?ra do?ru azal?r
            if (slider.value > 0f)
            {
                slider.value -= 0.01f;
            }
        }
        else
        {
            float fark = Vector3.Distance(transform.position, TransitPoint.transform.position);
            slider.value = fark;

            if (Time.timeScale != 0 && Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;

                // Mouse hareketlerini kontrol ederek pozisyonu güncelle
                if (delta.x < 0)
                {
                    transform.position += Vector3.left * Mathf.Abs(delta.x) * sensitivity;
                }
                else if (delta.x > 0)
                {
                    transform.position += Vector3.right * delta.x * sensitivity;
                }

                // X ekseninde hareketi -1 ile 1 aras?nda s?n?rland?r
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, -1f, 1f),
                    transform.position.y,
                    transform.position.z
                );

                lastMousePosition = Input.mousePosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Toplama") || other.CompareTag("Cikarma") || other.CompareTag("Carpma") || other.CompareTag("Bolme"))
        {
            int sayi = int.Parse(other.name);
            gameManager.ManManager(other.tag, sayi, other.transform);
        }
        else if (other.CompareTag("FinalTrigger"))
        {
            camera.isFinal = true;
            gameManager.EnemiesAttack();
            isFinal = true;
        }
        else if (other.CompareTag("FreeCharacter"))
        {
            gameManager.Characters.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pole") || collision.gameObject.CompareTag("CubeObstacle") || collision.gameObject.CompareTag("PervaneDisli") || collision.gameObject.CompareTag("Balyoz"))
        {
            float adjustment = 0.3f;
            if (transform.position.x > 0f)
            {
                transform.position = new Vector3(transform.position.x - adjustment, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + adjustment, transform.position.y, transform.position.z);
            }
        }
    }
}
