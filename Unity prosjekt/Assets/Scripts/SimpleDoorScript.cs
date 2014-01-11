using UnityEngine;
using System.Collections;

public class SimpleDoorScript : MonoBehaviour
{

    public Transform leftDoor;
    public Transform rightDoor;

    public float openSpeed = 1f;
    public float openLength = 1f;

    private bool _open, _close;
    private Vector3 originalPosLeft, originalPosRight;

    float keyFrame = 0;  

    void Start()
    {
        originalPosRight = rightDoor.transform.position;
        originalPosLeft = leftDoor.transform.position; 

    }

    void OnTriggerEnter(Collider other)
    {
        _open = true;
        _close = false;
        keyFrame = 0;
    }

    void Update()
    {
        if (_open)
        {
            leftDoor.position = Vector3.Lerp(originalPosLeft, originalPosLeft + (transform.right * openLength), (keyFrame += Time.deltaTime * openSpeed));
            rightDoor.position = Vector3.Lerp(originalPosRight, originalPosRight + (-transform.right * openLength), (keyFrame += Time.deltaTime * openSpeed));
            if (leftDoor.transform.position == originalPosLeft + (transform.right * openLength))
            {
                keyFrame = 0f;
                _open = false; 
            } 
        }
       else if (_close)
        {
            leftDoor.position = Vector3.Lerp(originalPosLeft + (transform.right * openLength), originalPosLeft, (keyFrame += Time.deltaTime * openSpeed));
            rightDoor.position = Vector3.Lerp(originalPosRight + (-transform.right * openLength), originalPosRight, (keyFrame += Time.deltaTime * openSpeed));
            if (leftDoor.transform.position == originalPosLeft)
            {
                keyFrame = 0f;
                _close = false; 
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        _open = false;
        _close = true;
        keyFrame = 0; 
    }
}
