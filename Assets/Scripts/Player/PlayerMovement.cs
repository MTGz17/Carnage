using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float boostedSpeed = 15f;
    [SerializeField] private float boostDuration = 3f;

    private float currentSpeed;
    private bool isBoosted = false;

    private InputAction moveAction;
    private Rigidbody rb;

    [Header("Car Physics Settings")]
    [SerializeField] private float turnTorqueMultiplier = 75f; // How strongly the car turns

    private void Start()
    {
        moveAction = GetComponent<PlayerInput>().actions["Move"];
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;

        // Ensure gravity is enabled
        rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        // Convert input to world-space movement relative to camera
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * input.y + right * input.x).normalized;

        // Apply forward/backward movement using AddForce
        rb.AddForce(direction * currentSpeed, ForceMode.Acceleration);

        // Apply turning torque around Y-axis for car-like feel
        Vector3 torque = Vector3.up * input.x * turnTorqueMultiplier;
        rb.AddTorque(torque, ForceMode.Acceleration);
    }

    public void BoostSpeed()
    {
        if (!isBoosted)
        {
            StartCoroutine(SpeedBoostCoroutine());
        }
    }

    private System.Collections.IEnumerator SpeedBoostCoroutine()
    {
        isBoosted = true;
        currentSpeed = boostedSpeed;

        yield return new WaitForSeconds(boostDuration);

        currentSpeed = speed;
        isBoosted = false;
    }
}