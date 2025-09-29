using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private TextMeshProUGUI speedometer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(rb != null || speedometer != null)
        {
            float speedMPS = rb.linearVelocity.magnitude;
            int speedMPH = Mathf.RoundToInt(speedMPS * 2.23694f);

            speedometer.text = speedMPH + " MPH";
        }
    }
}