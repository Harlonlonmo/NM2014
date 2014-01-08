using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterMotor))]
public class Ladder : MonoBehaviour
{

    private CharacterController controller;
    private CharacterMotor motor;

    private float defaultSlopeLimit;
    private bool defaultSlidingEnabled;

    public float ClimbSlopeLimit = 85;
    public bool ClimbSlidingEnabled = false;

    public bool active = false;
    
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        defaultSlopeLimit = controller.slopeLimit;
        motor = transform.GetComponent<CharacterMotor>();
        defaultSlidingEnabled = motor.sliding.enabled;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ladder")
        {
            controller.slopeLimit = ClimbSlopeLimit;
            motor.sliding.enabled = ClimbSlidingEnabled;
            active = true;
        }
    } 
 
    void OnTriggerExit (Collider other) 
    { 
        if(other.gameObject.tag == "ladder")
        {
            controller.slopeLimit = defaultSlopeLimit;
            motor.sliding.enabled = defaultSlidingEnabled;
            active = false;
        }
    
    }
}

