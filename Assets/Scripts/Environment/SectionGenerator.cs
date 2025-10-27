using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] roadSections;
    [SerializeField] private Transform endPoint;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasSpawned || !other.CompareTag("Player")) return;
        hasSpawned = true;

        int randomIndex = Random.Range(0, roadSections.Length);
        GameObject selectedSection = roadSections[randomIndex];

        GameObject newSection = Instantiate(selectedSection);

        Transform newStart = newSection.transform.Find("StartPoint");
        Transform newEnd = newSection.transform.Find("EndPoint");

        if (newStart == null || newEnd == null)
        {
            Debug.LogError($"StartPoint or EndPoint missing on {selectedSection.name}");
            return;
        }

        Quaternion rotationOffset = Quaternion.FromToRotation(newStart.forward, endPoint.forward);
        newSection.transform.rotation = rotationOffset * newSection.transform.rotation;

        Vector3 positionOffset = endPoint.position - newStart.position;
        newSection.transform.position += positionOffset;
    }
}