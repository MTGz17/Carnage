using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] roadSections;
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            hasSpawned = true;

            Vector3 offset = new Vector3(75f, 0f, 0f);
            Vector3 spawnPosition = transform.position + transform.forward * 361.2826f + offset;

            int randomIndex = Random.Range(0, roadSections.Length);
            GameObject selectedSection = roadSections[randomIndex];

            Instantiate(selectedSection, spawnPosition, Quaternion.identity);
        }
    }
}