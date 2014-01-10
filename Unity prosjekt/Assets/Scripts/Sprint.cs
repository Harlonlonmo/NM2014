using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Sprint : MonoBehaviour
{

    private CharacterMotor motor;

    public float SprintSpeed;
    private float DefaultMoveSpeed;

	// Use this for initialization
	void Start ()
	{
	    motor = GetComponent<CharacterMotor>();
	    DefaultMoveSpeed = motor.movement.maxForwardSpeed;
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Sprint"))
        {
            motor.movement.maxForwardSpeed = SprintSpeed;
        }
        else
        {
            motor.movement.maxForwardSpeed = DefaultMoveSpeed;
        }
    }
}
