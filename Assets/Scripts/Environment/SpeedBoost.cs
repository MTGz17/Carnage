using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCarController playerCar = other.GetComponent<PlayerCarController>();
            if (playerCar != null)
            {
                playerCar.BoostSpeed();
                Destroy(gameObject);
            }
        }
    }
}