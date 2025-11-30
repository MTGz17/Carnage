using UnityEngine;
using System.Collections;

public class NPCarSpawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnRandomPrefab();
        }
    }

    void SpawnRandomPrefab()
    {
        if (prefabs.Length == 0) return;

        int index = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[index], transform.position, transform.rotation);
    }
}