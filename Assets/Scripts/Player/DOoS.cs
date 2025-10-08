using UnityEngine;

public class DOoS : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}