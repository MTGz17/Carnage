using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float motorForce = 10000f;
    [SerializeField] private float maxSteerAngle = 15f;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    [SerializeField] private float boostForce = 10000f;

    [SerializeField] private float speedCap = 53.6448f;
    [SerializeField] private float boostOverrideDuration = 2f;

    private float speedCapOverrideTimer = 0f;

    private InputAction moveAction;
    private Rigidbody rb;
    private Vector2 input;
    private float currentSteerAngle;

    private void Start()
    {
        moveAction = GetComponent<PlayerInput>().actions["Move"];
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.centerOfMass = new Vector3(0f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();

        if (speedCapOverrideTimer > 0f)
            speedCapOverrideTimer -= Time.fixedDeltaTime;

        CapSpeed();
        UpdateWheels();
    }

    private void GetInput()
    {
        input = moveAction.ReadValue<Vector2>();
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = input.y * motorForce;
        frontRightWheelCollider.motorTorque = input.y * motorForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * input.x;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void CapSpeed()
    {
        if (speedCapOverrideTimer > 0f)
            return;

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float currentSpeed = horizontalVelocity.magnitude;

        if (currentSpeed > speedCap)
        {
            Vector3 clampedVelocity = horizontalVelocity.normalized * speedCap;
            rb.linearVelocity = new Vector3(clampedVelocity.x, rb.linearVelocity.y, clampedVelocity.z);
        }
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    public void BoostSpeed()
    {
        Vector3 boostDirection = transform.forward;
        rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);

        speedCapOverrideTimer = boostOverrideDuration;
    }
}