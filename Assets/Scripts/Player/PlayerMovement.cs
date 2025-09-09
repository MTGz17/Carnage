using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float boostedSpeed = 10f;
    [SerializeField] float boostDuration = 2f;

    private float currentSpeed;
    private bool isBoosted = false;

    InputAction moveAction;
    Rigidbody rb;

    private void Start()
    {
        moveAction = GetComponent<PlayerInput>().actions["Move"];
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
    }

    private void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * input.y + right * input.x).normalized;
        Vector3 movement = direction * currentSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
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