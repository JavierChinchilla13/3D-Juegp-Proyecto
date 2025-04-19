using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public GameObject blood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Lifes>().Lost();
            Instantiate(blood, other.transform.position, Quaternion.identity);
        }
    }
}
