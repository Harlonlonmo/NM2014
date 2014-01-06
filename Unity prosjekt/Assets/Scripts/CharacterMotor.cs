using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour
{

    // Does this script currently respond to input?
    public bool canControl = true;

    public bool useFixedUpdate = true;

    // For the next variables, @System.NonSerialized tells Unity to not serialize the variable or show it in the inspector view.
    // Very handy for organization!

    // The current global direction we want the character to move in.
    [System.NonSerialized]
    public Vector3 inputMoveDirection = Vector3.zero;

    // Is the jump button held down? We use this interface instead of checking
    // for the jump button directly so this script can also be used by AIs.
    [System.NonSerialized]
    public bool inputJump = false;

    class CharacterMotorMovement
    {
        // The maximum horizontal speed when moving
        float maxForwardSpeed = 10.0f;
        float maxSidewaysSpeed = 10.0f;
        float maxBackwardsSpeed = 10.0f;

        // Curve for multiplying speed based on slope (negative = downwards)
        AnimationCurve slopeSpeedMultiplier = new AnimationCurve(new Keyframe(-90, 1), new Keyframe(0, 1), new Keyframe(90, 0));

        // How fast does the character change speeds?  Higher is faster.
        float maxGroundAcceleration = 30.0f;
        float maxAirAcceleration = 20.0f;

        // The gravity for the character
        float gravity = 10.0f;
        float maxFallSpeed = 20.0f;

        // For the next variables, @System.NonSerialized tells Unity to not serialize the variable or show it in the inspector view.
        // Very handy for organization!

        // The last collision flags returned from controller.Move
        [System.NonSerialized]
        CollisionFlags collisionFlags;

        // We will keep track of the character's current velocity,
        [System.NonSerialized]
        Vector3 velocity;

        // This keeps track of our current velocity while we're not grounded
        [System.NonSerialized]
        Vector3 frameVelocity = Vector3.zero;

        [System.NonSerialized]
        Vector3 hitPoint = Vector3.zero;

        [System.NonSerialized]
        Vector3 lastHitPoint = new Vector3(Mathf.Infinity, 0, 0);
    }

    //CharacterMotorMovement movement = CharacterMotorMovement();

    enum MovementTransferOnJump
    {
        None, // The jump is not affected by velocity of floor at all.
        InitTransfer, // Jump gets its initial velocity from the floor, then gradualy comes to a stop.
        PermaTransfer, // Jump gets its initial velocity from the floor, and keeps that velocity until landing.
        PermaLocked // Jump is relative to the movement of the last touched floor and will move together with that floor.
    }

    // We will contain all the jumping related variables in one helper class for clarity.
    class CharacterMotorJumping
    {
        // Can the character jump?
        bool enabled = true;

        // How high do we jump when pressing jump and letting go immediately
        float baseHeight = 1.0f;

        // We add extraHeight units (meters) on top when holding the button down longer while jumping
        float extraHeight = 4.1f;

        // How much does the character jump out perpendicular to the surface on walkable surfaces?
        // 0 means a fully vertical jump and 1 means fully perpendicular.
        float perpAmount = 0.0f;

        // How much does the character jump out perpendicular to the surface on too steep surfaces?
        // 0 means a fully vertical jump and 1 means fully perpendicular.
        float steepPerpAmount = 0.5f;

        // For the next variables, @System.NonSerialized tells Unity to not serialize the variable or show it in the inspector view.
        // Very handy for organization!

        // Are we jumping? (Initiated with jump button and not grounded yet)
        // To see if we are just in the air (initiated by jumping OR falling) see the grounded variable.
        [System.NonSerialized]
        bool jumping = false;

        [System.NonSerialized]
        bool holdingJumpButton = false;

        // the time we jumped at (Used to determine for how long to apply extra jump power after jumping.)
        [System.NonSerialized]
        float lastStartTime = 0.0f;

        [System.NonSerialized]
        float lastButtonDownTime = -100f;

        [System.NonSerialized]
        Vector3 jumpDir = Vector3.up;
    }
}
