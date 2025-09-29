using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roadSection;
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            hasSpawned = true;

            Vector3 spawnPosition = transform.position + transform.forward * 30f;

            Instantiate(roadSection, spawnPosition, Quaternion.identity);
        }
    }
}
