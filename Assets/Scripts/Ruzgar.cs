using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruzgar : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("AICharacters"))
        {
            Debug.Log("1");
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-10,0,0),ForceMode.Impulse);
        }
    }
}
