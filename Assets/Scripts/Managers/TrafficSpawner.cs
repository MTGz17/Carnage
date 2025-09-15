using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour
{
    [Header("Car Settings")]
    public GameObject[] carPrefabs;                 // Car prefabs array
    public Vector2 speedRange = new Vector2(5f, 15f); // Initial impulse speed range
    public Vector2 massRange = new Vector2(1f, 3f);   // Random mass for chaos
    public Vector2 dragRange = new Vector2(0.3f, 1f); // Random drag for friction

    [Header("Spawn Settings")]
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;

    [Header("Spawn Position Ranges")]
    public Vector2 spawnXRange = new Vector2(10f, 15f);
    public float spawnY = 0.45f;
    public Vector2 spawnZRange = new Vector2(30f, 35f);

    [Header("Optional Rotation")]
    public bool randomRotation = true;
    public Vector2 rotationYRange = new Vector2(-10f, 10f); // small initial twist

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            // Pick a random car prefab
            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

            // Calculate spawn Y so car sits on ground
            Collider carCollider = carPrefab.GetComponent<Collider>();
            float spawnHeight = spawnY;
            if (carCollider != null)
                spawnHeight += carCollider.bounds.extents.y;

            // Random spawn position
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnXRange.x, spawnXRange.y),
                spawnHeight,
                Random.Range(spawnZRange.x, spawnZRange.y)
            );

            // Instantiate car
            Quaternion rotation = Quaternion.identity;
            if (randomRotation)
            {
                rotation = Quaternion.Euler(0f, Random.Range(rotationYRange.x, rotationYRange.y), 0f);
            }

            GameObject car = Instantiate(carPrefab, spawnPos, rotation);

            // Add TrafficCar script if missing
            TrafficCar trafficCar = car.GetComponent<TrafficCar>();
            if (trafficCar == null)
                trafficCar = car.AddComponent<TrafficCar>();

            // Randomize initial speed
            trafficCar.initialSpeed = Random.Range(speedRange.x, speedRange.y);

            // Configure Rigidbody
            Rigidbody rb = car.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = Random.Range(massRange.x, massRange.y);
                rb.linearDamping = Random.Range(dragRange.x, dragRange.y);
            }
        }
    }
}