using UnityEngine;
using System.Collections;


public class SetEnableOnTrigger : MonoBehaviour
{

    public Transform transform;

    public bool state;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.gameObject.SetActive(state);
        }
    }
}
