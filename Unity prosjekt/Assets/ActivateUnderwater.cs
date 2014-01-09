using UnityEngine;
using System.Collections;

public class ActivateUnderwater : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //if (other.gameObject.tag != "Player") return; 
        other.gameObject.GetComponent<SubmergeHandler>().Submerge();
    }

    void OnTriggerExit(Collider other)
    {   
        //if (other.gameObject.tag != "Player") return;
        other.gameObject.GetComponent<SubmergeHandler>().Emerge();
    }
}
