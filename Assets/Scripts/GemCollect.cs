using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Oculta la gema
            GetComponent<MeshRenderer>().enabled = false;

            // ¡La podés destruir después de un pequeño delay!
            Destroy(gameObject, 0.5f);
        }
    }

}
