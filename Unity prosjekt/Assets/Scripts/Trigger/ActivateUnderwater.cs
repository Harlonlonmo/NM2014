using UnityEngine;

public class ActivateUnderwater : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<SubmergeHandler>().Submerge();
    }

    void OnTriggerExit(Collider other)
    {   
        other.gameObject.GetComponent<SubmergeHandler>().Emerge();
    }
}
