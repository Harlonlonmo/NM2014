using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Sprint : MonoBehaviour
{

    private CharacterMotor motor;

    public float SprintSpeed;
    private float DefaultMoveSpeed;
    private bool _superSpeed; 

	// Use this for initialization
	void Start ()
	{
	    motor = GetComponent<CharacterMotor>();
	    DefaultMoveSpeed = motor.movement.maxForwardSpeed;
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(2)) _superSpeed = !_superSpeed; 
        if (Input.GetButton("Sprint"))
        {
            if (_superSpeed) motor.movement.maxForwardSpeed = SprintSpeed * 2;
            else motor.movement.maxForwardSpeed = SprintSpeed;
        }
        else
        {
            motor.movement.maxForwardSpeed = DefaultMoveSpeed;
        }
    }
}
