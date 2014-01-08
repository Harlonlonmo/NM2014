using UnityEngine;
using System.Collections;

public class SimpleDoorScript : MonoBehaviour {

    public Transform leftDoor;
    public Transform rightDoor;

    public float openSpeed = 1f;
    public float openLength = 1f;

    public bool isForwardBased = true;

    void OnTriggerEnter(Collider other)
    {
        if (isForwardBased)
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftDoor.position + (-Vector3.forward * openLength), Time.time * openSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightDoor.position + (Vector3.forward * openLength), Time.time * openSpeed);
        }
        else
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftDoor.position + (Vector3.right * openLength), Time.time * openSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightDoor.position + (-Vector3.right * openLength), Time.time * openSpeed);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (isForwardBased)
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftDoor.position + (Vector3.forward * openLength), Time.time * openSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightDoor.position + (-Vector3.forward * openLength), Time.time * openSpeed);
        }
        else
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftDoor.position + (-Vector3.right * openLength), Time.time * openSpeed);
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightDoor.position + (Vector3.right * openLength), Time.time * openSpeed);
        }
    }
}
