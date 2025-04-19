using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    public GameObject objectToPickUp;
    public GameObject pickedObject;
    public Transform InteractionZone;

    private void Update()
    {
        if (objectToPickUp != null &&
            objectToPickUp.GetComponent<PickableObject>().isPickable == true &&
            pickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                pickedObject = objectToPickUp;
                pickedObject.GetComponent<PickableObject>().isPickable = false;
                pickedObject.transform.SetParent(InteractionZone);
                pickedObject.transform.position = InteractionZone.position;
                pickedObject.GetComponent<Rigidbody>().useGravity = false;
                pickedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else if (pickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                pickedObject.GetComponent<PickableObject>().isPickable = true;
                pickedObject.transform.SetParent(null);
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject = null;
            }
        }
    }
}
