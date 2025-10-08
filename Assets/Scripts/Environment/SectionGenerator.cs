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

            Vector3 offset = new Vector3(75f, 0f, 0f);

            Vector3 spawnPosition = transform.position + transform.forward * 361.2826f + offset;

            Instantiate(roadSection, spawnPosition, Quaternion.identity);
        }
    }
}