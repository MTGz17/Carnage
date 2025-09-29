using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("List of Prefabs to Spawn")]
    public GameObject[] prefabs;

    void Start()
    {
        SpawnRandomPrefab();
    }

    void SpawnRandomPrefab()
    {
        int index = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[index], transform.position, Quaternion.identity);
    }
}