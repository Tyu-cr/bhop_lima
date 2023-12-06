using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration = 200f; // How fast the player accelerates on the ground
    [SerializeField] private float airAcceleration = 200f; // How fast the player accelerates in the air
    [SerializeField] private float maxSpeed = 6.4f; // Maximum player speed on the ground
    [SerializeField] private float maxAirSpeed = 0.6f; // "Maximum" player speed in the air
    [SerializeField] private float friction = 8f; // How fast the player decelerates on the ground
    [SerializeField] private float jumpForce = 5f; // How high the player jumps
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private GameObject cam;

    private float _lastJumpPress = -1f;
    private readonly float _jumpPressDuration = 0.1f;
    private bool _onGround;
    public Restart restart;

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            _lastJumpPress = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (restart.restart) return;

        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input != new Vector2(0f, 0f))
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        // Get player velocity
        var playerVelocity = GetComponent<Rigidbody>().velocity;
        // Slow down if on ground
        playerVelocity = CalculateFriction(playerVelocity);
        // Add player input
        playerVelocity += CalculateMovement(input, playerVelocity);
        // Assign new velocity to player object
        GetComponent<Rigidbody>().velocity = playerVelocity;
    }

    // Slows down the player if on the ground
    private Vector3 CalculateFriction(Vector3 currentVelocity)
    {
        _onGround = CheckGround();
        var speed = currentVelocity.magnitude;

        if (!_onGround || Input.GetButton("Jump") || speed == 0f)
            return currentVelocity;

        var drop = speed * friction * Time.deltaTime;
        return currentVelocity * (Mathf.Max(speed - drop, 0f) / speed);
    }

    // Moves the player according to the input. (THIS IS WHERE THE STRAFING MECHANIC HAPPENS)
    private Vector3 CalculateMovement(Vector2 input, Vector3 velocity)
    {
        _onGround = CheckGround();

        //Different acceleration values for ground and air
        var curAccel = acceleration;
        if (!_onGround)
            curAccel = airAcceleration;

        //Ground speed
        var curMaxSpeed = maxSpeed;

        //Air speed
        if (!_onGround)
            curMaxSpeed = maxAirSpeed;

        //Get rotation input and make it a vector
        var camRotation = new Vector3(0f, cam.transform.rotation.eulerAngles.y, 0f);
        var inputVelocity = Quaternion.Euler(camRotation) *
                            new Vector3(input.x * curAccel, 0f, input.y * curAccel);

        //Ignore vertical component of rotated input
        var alignedInputVelocity = new Vector3(inputVelocity.x, 0f, inputVelocity.z) * Time.deltaTime;

        //Get current velocity
        var currentVelocity = new Vector3(velocity.x, 0f, velocity.z);

        //How close the current speed to max velocity is (1 = not moving, 0 = at/over max speed)
        var max = Mathf.Max(0f, 1 - (currentVelocity.magnitude / curMaxSpeed));

        //How perpendicular the input to the current velocity is (0 = 90°)
        var velocityDot = Vector3.Dot(currentVelocity, alignedInputVelocity);

        //Scale the input to the max speed
        var modifiedVelocity = alignedInputVelocity * max;

        //The more perpendicular the input is, the more the input velocity will be applied
        var correctVelocity = Vector3.Lerp(alignedInputVelocity, modifiedVelocity, velocityDot);

        //Apply jump
        correctVelocity += GetJumpVelocity(velocity.y);

        return correctVelocity;
    }

    // Calculates the velocity with which the player is accelerated up when jumping
    private Vector3 GetJumpVelocity(float yVelocity)
    {
        var jumpVelocity = Vector3.zero;

        if (Time.time < _lastJumpPress + _jumpPressDuration && yVelocity < jumpForce && CheckGround())
        {
            _lastJumpPress = -1f;
            jumpVelocity = new Vector3(0f, jumpForce - yVelocity, 0f);
        }

        return jumpVelocity;
    }

    // Checks if the player is touching the ground.
    private bool CheckGround()
    {
        var ray = new Ray(transform.position, Vector3.down);
        var result = Physics.Raycast(ray, GetComponent<Collider>().bounds.extents.y + 0.1f, groundLayers);
        return result;
    }
}