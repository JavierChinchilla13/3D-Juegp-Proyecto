using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    CharacterController player;
    
    Vector3 groundPosition;
    Vector3 lastGroundPosition;
    string groundName;
    string lastGroundName;

    Quaternion actualRot;
    Quaternion lastRot;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGrounded)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 
                player.height / 4.2f, -transform.up, out hit))
            {
                
                GameObject groundedIn = hit.collider.gameObject;
                //Toma información del suelo actual
                groundName = groundedIn.name;
                groundPosition = groundedIn.transform.position;
                actualRot = groundedIn.transform.rotation;

                if (groundPosition != lastGroundPosition &&
                    groundName == lastGroundName)
                {
                    //mueve al personaje
                    this.transform.position += groundPosition - lastGroundPosition;
                }
                //Realiza la rotación de la platfomr

                if (actualRot != lastRot && groundName == lastGroundName)
                {
                    //gira sobre el eje del jugador
                    var newRot = this.transform.rotation * (actualRot.eulerAngles
                        - lastRot.eulerAngles);
                    //Gira sobre le eje de la plataforma
                    this.transform.RotateAround(groundedIn.transform.position,
                        Vector3.up, newRot.y);
                }

                //almacena informacion utimo suelo
                lastGroundName = groundName;
                lastGroundPosition = groundPosition;

                lastRot = actualRot;
            }
        }
        else
        {
            //limpia las variables del suelo
            lastGroundName = null;
            lastGroundPosition = Vector3.zero;
            lastRot = Quaternion.Euler(0,0,0);
        }
    }

   /* private void OnDrawGizmos()
    {
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawSphere(transform.position, player.height / 4.2f);
    }*/

}
