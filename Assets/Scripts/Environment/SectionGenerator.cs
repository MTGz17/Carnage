using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] roadSections;
    [SerializeField] private GameObject specialSection;
    [SerializeField] private Transform endPoint;

    public static int sectionCount = 0;
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasSpawned || !other.CompareTag("Player")) return;
        hasSpawned = true;

        sectionCount++;
        Debug.Log("Section Count: " + sectionCount);

        GameObject selectedSection;

        if (sectionCount % 10 == 0)
        {
            selectedSection = specialSection;
        }
        else
        {
            int randomIndex = Random.Range(0, roadSections.Length);
            selectedSection = roadSections[randomIndex];
        }

        GameObject newSection = Instantiate(selectedSection);

        Transform newStart = newSection.transform.Find("StartPoint");
        Transform newEnd = newSection.transform.Find("EndPoint");

        if (newStart == null || newEnd == null)
        {
            Debug.Log("StartPoint or EndPoint missing");
            return;
        }

        Quaternion rotationOffset = Quaternion.FromToRotation(newStart.forward, endPoint.forward);
        newSection.transform.rotation = rotationOffset * newSection.transform.rotation;

        Vector3 positionOffset = endPoint.position - newStart.position;
        newSection.transform.position += positionOffset;
    }

    public static void ResetSectionCount()
    {
        sectionCount = 0;
        Debug.Log("Section Count Reset!");
    }
}