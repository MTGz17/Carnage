using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float motorForce = 100f;
    [SerializeField] private float maxSteerAngle = 30f;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    [SerializeField] private float baseMaxSpeed = 35.7632f;
    [SerializeField] private float boostIncrement = 5f;

    private int boostCount = 0;

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

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void CapSpeed()
    {
        float currentMaxSpeed = baseMaxSpeed + boostCount * boostIncrement;
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        float currentSpeed = horizontalVelocity.magnitude;

        if (boostCount > 0 && currentSpeed < baseMaxSpeed)
        {
            boostCount = 0;
            currentMaxSpeed = baseMaxSpeed;
        }

        if (boostCount == 0 && currentSpeed > baseMaxSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * baseMaxSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    public void BoostSpeed()
    {
        boostCount++;
    }
}