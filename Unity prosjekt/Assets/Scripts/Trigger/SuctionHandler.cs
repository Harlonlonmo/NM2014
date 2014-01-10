using UnityEngine;

public class SuctionHandler : MonoBehaviour
{

    public Transform SuctionPoint;
    public GameObject Player; 
    public float SuctionPower;
    private bool _suck;
    private CharacterController _characterController; 

    void Start()
    {
        _characterController = Player.GetComponent<CharacterController>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        _suck = true;
    }

    void OnTriggerExit(Collider other)
    {
        _suck = false;
    }

    void Update()
    {
        if (!_suck) return;
        _characterController.Move((SuctionPoint.position - Player.transform.position).normalized * Time.deltaTime * SuctionPower);
    }

}
